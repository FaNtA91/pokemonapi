﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pokemon.Packets.Outgoing
{
    public class ItemUsePacket : OutgoingPacket
    {
        public Objects.Location FromPosition { get; set; }
        public ushort SpriteId { get; set; }
        public byte FromStackPosition { get; set; }
        public byte Index { get; set; }

        public ItemUsePacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.ItemUse;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.ItemUse)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.ItemUse;

            FromPosition = msg.GetLocation();
            SpriteId = msg.GetUInt16();
            FromStackPosition = msg.GetByte();
            Index = msg.GetByte();

            return true;
        }

        public override void ToNetworkMessage(ref NetworkMessage msg)
        {
            msg.AddByte((byte)Type);

            msg.AddLocation(FromPosition);
            msg.AddUInt16(SpriteId);
            msg.AddByte(FromStackPosition);
            msg.AddByte(Index);
        }

        public static bool Send(Objects.Client client, Objects.Location fromPosition, ushort spriteId, byte fromStack, byte index)
        {
            ItemUsePacket p = new ItemUsePacket(client);
            p.FromPosition = fromPosition;
            p.SpriteId = spriteId;
            p.FromStackPosition = fromStack;
            p.Index = index;

            return p.Send();
        }
    }
}