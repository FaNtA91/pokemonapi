﻿using Pokemon.Constants;

namespace Pokemon.Packets.Outgoing
{
    public class PingPacket : OutgoingPacket
    {
        public PingPacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.Ping;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.Ping)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.Ping;

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
        }

        public static bool Send(Objects.Client client)
        {
            PingPacket p = new PingPacket(client);
            return p.Send();
        }
    }
}