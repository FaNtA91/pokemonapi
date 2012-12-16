﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pokemon.Packets.Incoming
{
    public class PlayerWalkCancelPacket : IncomingPacket
    {
        public byte Direction { get; set; }

        public PlayerWalkCancelPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.PlayerWalkCancel;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.PlayerWalkCancel)
                return false;

            Destination = destination;
            Type = IncomingPacketType.PlayerWalkCancel;

            try
            {
                Direction = msg.GetByte();
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
            msg.AddByte(Direction);
        }
    }
}