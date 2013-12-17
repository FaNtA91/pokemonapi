using System;
using Pokemon.Constants;
using Pokemon.Util;

namespace Pokemon.Packets
{
    public class Packet
    {
        public enum SendMethod { Proxy, HookProxy, Memory }
        public uint PacketId { get; set; }
        public bool Forward { get; set; }
        public PacketDestination Destination { get; set; }
        public Objects.Client Client { get; set; }

        private static object msgLock = new object();
        private static NetworkMessage msg;

        public Packet(Objects.Client c)
        {
            Client = c;
            Forward = true;
        }

        public virtual void ToNetworkMessage(NetworkMessage msg)
        {
            throw new Exception("ToNetworkMessage not implemented.");
        }

        public bool Send()
        {

            if (Client.IO.UsingProxy)
            {
                return Send(SendMethod.Proxy);
            }
            else if (Client.Dll.Pipe != null && Client.Dll.Pipe.Connected && Destination != PacketDestination.Client)
            {
                return Send(SendMethod.HookProxy);
            }
            else
            {
                return Send(SendMethod.Memory);
            }
        }

        public bool Send(SendMethod method)
        {
            if (msg == null)
                msg = new NetworkMessage(Client, 4048);

            switch (method)
            {

                case SendMethod.Proxy:
                    lock (msgLock)
                    {
                        msg.Reset();
                        ToNetworkMessage(msg);

                        if (msg.Length > 8)
                        {
                            msg.InsetLogicalPacketHeader();
                            msg.PrepareToSend();

                            if (Destination == PacketDestination.Client)
                                Client.IO.Proxy.SendToClient(msg.GetData());
                            else if (Destination == PacketDestination.Server)
                                Client.IO.Proxy.SendToServer(msg.GetData());

                            return true;
                        }
                    }
                    break;
                case SendMethod.HookProxy:
                    lock (msgLock)
                    {
                        msg.Reset();
                        ToNetworkMessage(msg);

                        if (msg.Length > 8)
                        {
                            msg.InsetLogicalPacketHeader();
                            msg.PrepareToSend();

                            Pipes.HookSendToServerPacket.Send(Client, msg.GetData());

                            return true;
                        }
                    }
                    break;
                case SendMethod.Memory:
                    lock (msgLock)
                    {
                        msg.Reset();
                        ToNetworkMessage(msg);
                        if (Destination == PacketDestination.Server)
                        {
                            if (msg.Length > 8)
                            {
                                msg.InsetLogicalPacketHeader();
                                msg.PrepareToSend();

                                return SendPacketToServerByMemory(Client, msg.GetData());
                            }
                        }
                        else if (Destination == PacketDestination.Client)
                        {
                            byte[] data = new byte[msg.GetData().Length - 8];
                            Array.Copy(msg.GetData(), 8, data, 0, data.Length);
                            SendPacketToClientByMemory(Client, data);
                        }
                    }
                    break;
            }

            return false;
        }

        #region Sending Packets with Stepler's Method
        // (http://www.tpforums.org/forum/showthread.php?t=2832)
        /// <summary>
        /// Send a packet through the client by writing some code in memory and running it.
        /// The packet must not contain any header(no length nor Adler checksum) and be unencrypted
        /// </summary>
        /// <param name="client"></param>
        /// <param name="packet"></param>
        /// <returns></returns>
        public static bool SendPacketToServerByMemory(Objects.Client client, byte[] packet)
        {
            if (client.LoggedIn())
            {
                if (!client.IO.IsSendToServerCodeWritten)
                    if (!client.IO.WriteSocketSendCode()) return false;

                uint bufferSize = (uint)(4 + msg.Length);
                byte[] readyPacket = new byte[bufferSize];
                Array.Copy(BitConverter.GetBytes(msg.Length), readyPacket, 4);
                Array.Copy(packet, 0, readyPacket, 4, packet.Length);

                IntPtr pRemote = WinAPI.VirtualAllocEx(client.Handle, IntPtr.Zero, /*bufferSize*/
                    bufferSize,
                    WinAPI.AllocationType.Commit | WinAPI.AllocationType.Reserve,
                    WinAPI.MemoryProtection.ExecuteReadWrite);

                if (pRemote != IntPtr.Zero)
                {
                    if (client.Memory.WriteBytes(pRemote.ToInt64(), readyPacket, bufferSize))
                    {
                        IntPtr threadHandle = Pokemon.Util.WinAPI.CreateRemoteThread(client.Handle, IntPtr.Zero, 0,
                            client.IO.SendToServerAddress, pRemote, 0, IntPtr.Zero);
                        Pokemon.Util.WinAPI.WaitForSingleObject(threadHandle, 0xFFFFFFFF);//INFINITE=0xFFFFFFFF
                        Pokemon.Util.WinAPI.CloseHandle(threadHandle);
                        return true;
                    }
                }
                return false;

            }
            else return false;
        }
        #endregion

        public bool SendPacketToClientByMemory(Objects.Client client, byte[] packet)
        {
            bool ret = false;
            if (client.LoggedIn())
            {
                if (!client.IO.IsSendToClientCodeWritten)
                    if (!client.IO.WriteOnGetNextPacketCode()) return false;

                byte[] originalStream = client.Memory.ReadBytes(Pokemon.Addresses.Client.RecvStream, 12);

                IntPtr myStreamAddress = WinAPI.VirtualAllocEx(
                    client.Handle,
                    IntPtr.Zero,
                    (uint)packet.Length,
                    WinAPI.AllocationType.Commit | WinAPI.AllocationType.Reserve,
                    WinAPI.MemoryProtection.ExecuteReadWrite);
                if (myStreamAddress != IntPtr.Zero)
                {

                    if (client.Memory.WriteBytes(
                        myStreamAddress.ToInt64(),
                        packet,
                        (uint)packet.Length))
                    {
                        byte[] myStream = new byte[12];
                        Array.Copy(BitConverter.GetBytes(myStreamAddress.ToInt32()), myStream, 4);
                        Array.Copy(BitConverter.GetBytes(packet.Length), 0, myStream, 4, 4);

                        if (client.Memory.WriteBytes(Pokemon.Addresses.Client.RecvStream,
                            myStream, 12))
                        {
                            if (client.Memory.WriteByte(client.IO.SendToClientAddress.ToInt64(), 0x1))
                            {

                                IntPtr threadHandle = WinAPI.CreateRemoteThread(
                                                            client.Handle,
                                                            IntPtr.Zero,
                                                            0,
                                                            new IntPtr(Pokemon.Addresses.Client.ParserFunc),
                                                            IntPtr.Zero,
                                                            0,
                                                            IntPtr.Zero);
                                WinAPI.WaitForSingleObject(threadHandle, 0xFFFFFFFF);//INFINITE=0xFFFFFFFF
                                WinAPI.CloseHandle(threadHandle);

                                ret = true;
                                client.Memory.WriteByte(client.IO.SendToClientAddress.ToInt64(), 0x0);

                            }

                            client.Memory.WriteBytes(Pokemon.Addresses.Client.RecvStream,
                            originalStream, 12);
                        }

                    }
                }
                if (myStreamAddress != IntPtr.Zero)
                {
                    WinAPI.VirtualFreeEx(
                        client.Handle,
                        myStreamAddress,
                        12,
                        WinAPI.AllocationType.Release);
                }
            }
            return ret;
        }



        public virtual bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            return false;
        }

        public virtual bool ParseMessage(NetworkMessage msg, PacketDestination destination, NetworkMessage outMsg)
        {
            return false;
        }
    }
}
