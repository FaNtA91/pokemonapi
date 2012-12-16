﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pokemon.Packets.Incoming
{
    public class ContainerRemoveItemPacket : IncomingPacket
    {
        public byte Container { get; set; }
        public byte Slot { get; set; }

        public ContainerRemoveItemPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.ContainerRemoveItem;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.ContainerRemoveItem)
                return false;

            Destination = destination;
            Type = IncomingPacketType.ContainerRemoveItem;

            try
            {
                Container = msg.GetByte();
                Slot = msg.GetByte();
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
        }

    }
}