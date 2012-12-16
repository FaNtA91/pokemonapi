﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pokemon.Packets.Outgoing
{
    public class TileUpdatePacket : OutgoingPacket
    {
        public Objects.Location Position { get; set; }

        public TileUpdatePacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.TileUpdate;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.TileUpdate)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.TileUpdate;

            Position = msg.GetLocation();

            return true;
        }

        public override void ToNetworkMessage(ref NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddLocation(Position);
        }

        public static bool Send(Objects.Client client, Objects.Location position)
        {
            TileUpdatePacket p = new TileUpdatePacket(client);
            p.Position = position;
            return p.Send();
        }
    }
}
