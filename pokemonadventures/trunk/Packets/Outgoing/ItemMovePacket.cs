﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pokemon.Packets.Outgoing
{
    public class ItemMovePacket : OutgoingPacket
    {
        public Objects.Location FromPosition { get; set; }
        public ushort SpriteId { get; set; }
        public byte FromStackPosition { get; set; }
        public Objects.Location ToPosition { get; set; }
        public byte Count { get; set; }

        public ItemMovePacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.ItemMove;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.ItemMove)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.ItemMove;

            FromPosition = msg.GetLocation();
            SpriteId = msg.GetUInt16();
            FromStackPosition = msg.GetByte();
            ToPosition = msg.GetLocation();
            Count = msg.GetByte();

            return true;
        }

        public override void ToNetworkMessage(ref NetworkMessage msg)
        {
            msg.AddByte((byte)Type);

            msg.AddLocation(FromPosition);
            msg.AddUInt16(SpriteId);
            msg.AddByte(FromStackPosition);
            msg.AddLocation(ToPosition);
            msg.AddByte(Count);
        }

        public static bool Send(Objects.Client client, Objects.Location fromPosition, ushort spriteId, byte fromStackPostion, Objects.Location toPosition, byte count)
        {
            ItemMovePacket p = new ItemMovePacket(client);

            p.FromPosition = fromPosition;
            p.SpriteId = spriteId;
            p.FromStackPosition = fromStackPostion;
            p.ToPosition = toPosition;
            p.Count = count;

            return p.Send();
        }
    }
}