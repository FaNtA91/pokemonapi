﻿using Pokemon.Constants;

namespace Pokemon.Packets.Pipes
{
    public class RemoveAllSkinsPacket : PipePacket
    {
        public RemoveAllSkinsPacket(Objects.Client client)
            : base(client)
        {
            Type = PipePacketType.RemoveAllSkins;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)PipePacketType.RemoveAllSkins)
                return false;

            Type = PipePacketType.RemoveAllSkins;

            return true;
        }

        public override byte[] ToByteArray()
        {
            NetworkMessage msg = NetworkMessage.CreateUnencrypted(Client, 1);
            msg.AddByte((byte)Type);
            return msg.GetData();
        }

        public static bool Send(Objects.Client client)
        {
            RemoveAllSkinsPacket p = new RemoveAllSkinsPacket(client);
            return p.Send();
        }
    }
}
