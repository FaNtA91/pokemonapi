﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pokemon.Packets.Incoming
{
    public class ContainerUpdateItemPacket : IncomingPacket
    {
        public byte Container { get; set; }
        public byte Slot { get; set; }
        public Objects.Item Item { get; set; }

        public ContainerUpdateItemPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.ContainerUpdateItem;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.ContainerUpdateItem)
                return false;

            Destination = destination;
            Type = IncomingPacketType.ContainerUpdateItem;

            try
            {
                Container = msg.GetByte();
                Slot = msg.GetByte();
                Item = new Pokemon.Objects.Item(Client, msg.GetUInt16(), 0);

                if (Item.HasExtraByte)
                    Item.Count = msg.GetByte();
            }
            catch (Exception)
            {
                msg.Position = position;
                return false;
            }

            return true;
        }

        public override void ToNetworkMessage(ref NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddByte(Container);
            msg.AddByte(Slot);
            msg.AddUInt16((ushort)Item.Id);

            if (Item.HasExtraByte)
                msg.AddByte(Item.Count);
        }
    }
}