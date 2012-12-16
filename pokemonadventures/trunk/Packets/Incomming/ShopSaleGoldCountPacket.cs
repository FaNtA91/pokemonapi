﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pokemon.Packets.Incoming
{
    public class ShopSaleGoldCountPacket : IncomingPacket
    {
        public uint Cash { get; set; }
        public List<ShopInfo> ItemList { get; set; }

        public ShopSaleGoldCountPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.ShopSaleGoldCount;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.ShopSaleGoldCount)
                return false;

            Destination = destination;
            Type = IncomingPacketType.ShopSaleGoldCount;

            try
            {
                Cash = msg.GetUInt32();
                byte count = msg.GetByte();

                ItemList = new List<ShopInfo> { };

                for (int i = 0; i < count; i++)
                {
                    ShopInfo item = new ShopInfo();

                    item.ItemId = msg.GetUInt16();
                    item.SubType = msg.GetByte();

                    ItemList.Add(item);
                }
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
            msg.AddUInt32(Cash);

            msg.AddByte((byte)ItemList.Count);

            foreach (ShopInfo i in ItemList)
            {
                msg.AddUInt16(i.ItemId);
                msg.AddByte(i.SubType);
            }
        }
    }
}