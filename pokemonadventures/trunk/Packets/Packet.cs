﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
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

        public virtual void ToNetworkMessage(ref NetworkMessage msg)
        {
            throw new Exception("ToNetworkMessage not implemented.");
        }

        public bool Send()
        {
            
            if (Client.IO.UsingProxy)
            {
                return Send(SendMethod.Proxy);
            }
            else if (Client.Dll.Pipe != null && Client.Dll.Pipe.Connected)
            {
                return Send(SendMethod.HookProxy);
            }
            else if (Destination == PacketDestination.Server)
            {
                return Send(SendMethod.Memory);
            }
            return false;
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
                        ToNetworkMessage(ref msg);

                        if (msg.Length > 8)
                        {
                            msg.InsetLogicalPacketHeader();
                            msg.PrepareToSend();

                            if (Destination == PacketDestination.Client)
                                Client.IO.Proxy.SendToClient(msg.Data);
                            else if (Destination == PacketDestination.Server)
                                Client.IO.Proxy.SendToServer(msg.Data);

                            return true;
                        }
                    }
                    break;
                case SendMethod.HookProxy:
                    lock (msgLock)
                    {
                        msg.Reset();
                        ToNetworkMessage(ref msg);

                        if (msg.Length > 8)
                        {
                            msg.InsetLogicalPacketHeader();
                            msg.PrepareToSend();

                            Pipes.HookSendToServerPacket.Send(Client, msg.Data);

                            return true;
                        }
                    }
                    break;
                case SendMethod.Memory:
                    if (Destination == PacketDestination.Server)
                    {
                        lock (msgLock)
                        {
                            msg.Reset();
                            ToNetworkMessage(ref msg);

                            if (msg.Length > 8)
                            {
                                msg.InsetLogicalPacketHeader();
                                msg.PrepareToSend();

                                return SendPacketByMemory(Client, msg.Data);
                            }
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
        public static bool SendPacketByMemory(Objects.Client client, byte[] packet)
        {
            if (client.LoggedIn)
            {
                if (!client.IO.IsSendCodeWritten)
                    if (!client.IO.WriteSocketSendCode()) return false;

                uint bufferSize = (uint)(4 + msg.Length);
                byte[] readyPacket = new byte[bufferSize];
                Array.Copy(BitConverter.GetBytes(msg.Length), readyPacket, 4);
                Array.Copy(packet, 0, readyPacket, 4, packet.Length);

                IntPtr pRemote = Pokemon.Util.WinApi.VirtualAllocEx(client.ProcessHandle, IntPtr.Zero, /*bufferSize*/
                                            bufferSize,
                                            Pokemon.Util.WinApi.MEM_COMMIT | Pokemon.Util.WinApi.MEM_RESERVE,
                                            Pokemon.Util.WinApi.PAGE_EXECUTE_READWRITE);

                if (pRemote != IntPtr.Zero)
                {
                    if (client.Memory.WriteBytes(pRemote.ToInt64(), readyPacket, bufferSize))
                    {
                        IntPtr threadHandle = Pokemon.Util.WinApi.CreateRemoteThread(client.ProcessHandle, IntPtr.Zero, 0,
                            client.IO.SenderAddress, pRemote, 0, IntPtr.Zero);
                        Pokemon.Util.WinApi.WaitForSingleObject(threadHandle, 0xFFFFFFFF);//INFINITE=0xFFFFFFFF
                        Pokemon.Util.WinApi.CloseHandle(threadHandle);
                        return true;
                    }
                }
                return false;

            }
            else return false;
        }


        #endregion


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
