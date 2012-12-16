﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pokemon.Packets.Incoming
{
    public class MagicEffectPacket : IncomingPacket
    {
        public Objects.Location Position { get; set; }
        public byte Effect { get; set; }

        public MagicEffectPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.MagicEffect;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.MagicEffect)
                return false;

            Destination = destination;
            Type = IncomingPacketType.MagicEffect;

            try
            {
                Position = msg.GetLocation();
                Effect = msg.GetByte();
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
            msg.AddLocation(Position);
            msg.AddByte(Effect);
        }
    }
}