﻿using Pokemon.Constants;

namespace Pokemon.Packets.Incoming
{
    public class ContainerAddItemPacket : IncomingPacket
    {
        public byte Container { get; set; }
        public Objects.Item Item { get; set; }

        public ContainerAddItemPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.ContainerAddItem;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.ContainerAddItem)
                return false;

            Destination = destination;
            Type = IncomingPacketType.ContainerAddItem;

            Container = msg.GetByte();
            Item = msg.GetItem();

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddByte(Container);
            msg.AddItem(Item);
        }

    }
}