﻿using Pokemon.Constants;

namespace Pokemon.Packets.Outgoing
{
    public class ContainerClosePacket : OutgoingPacket
    {
        public byte Id { get; set; }

        public ContainerClosePacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.ContainerClose;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.ContainerClose)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.ContainerClose;

            Id = msg.GetByte();

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddByte(Id);
        }

        public static bool Send(Objects.Client client, byte id)
        {
            ContainerClosePacket p = new ContainerClosePacket(client);
            p.Id = id;
            return p.Send();
        }
    }
}