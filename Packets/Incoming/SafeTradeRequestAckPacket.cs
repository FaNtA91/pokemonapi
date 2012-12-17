﻿using System.Collections.Generic;
using Pokemon.Constants;

namespace Pokemon.Packets.Incoming
{
    public class SafeTradeRequestAckPacket : IncomingPacket
    {
        public string Name { get; set; }
        public byte Count { get; set; }
        public List<Objects.Item> Items { get; set; }

        public SafeTradeRequestAckPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.SafeTradeRequestAck;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.SafeTradeRequestAck)
                return false;

            Destination = destination;
            Type = IncomingPacketType.SafeTradeRequestAck;

            Name = msg.GetString();
            Count = msg.GetByte();

            Items = new List<Pokemon.Objects.Item>(Count);

            for (int i = 0; i < Count; i++)
            {
                Objects.Item item = msg.GetItem();

                Items.Add(item);
            }

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);

            msg.AddString(Name);
            msg.AddByte((byte)Items.Count);

            foreach (Objects.Item i in Items)
            {
                msg.AddItem(i);
            }
        }
    }
}