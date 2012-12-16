﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pokemon.Packets.Outgoing
{
    public class ItemRotatePacket : OutgoingPacket
    {
        public Objects.Location Location { get; set; }
        public ushort ItemId { get; set; }
        public byte StackPosition { get; set; }

        public ItemRotatePacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.ItemRotate;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.ItemRotate)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.ItemRotate;

            Location = msg.GetLocation();
            ItemId = msg.GetUInt16();
            StackPosition = msg.GetByte();

            return true;
        }

        public override void ToNetworkMessage(ref NetworkMessage msg)
        {

            msg.AddByte((byte)Type);

            msg.AddLocation(Location);
            msg.AddUInt16(ItemId);
            msg.AddByte(StackPosition);
        }

        public static bool Send(Objects.Client client, Objects.Location location, ushort itemId, byte stackPosition)
        {
            ItemRotatePacket p = new ItemRotatePacket(client);

            p.Location = location;
            p.ItemId = itemId;
            p.StackPosition = stackPosition;

            return p.Send();
        }
    }
}