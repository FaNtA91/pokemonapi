﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pokemon.Packets.Incoming
{
    public class PlayerFlagsPacket : IncomingPacket
    {

        public ushort Flag { get; set; }

        public PlayerFlagsPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.PlayerFlags;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.PlayerFlags)
                return false;

            Destination = destination;
            Type = IncomingPacketType.PlayerFlags;

            try
            {
                Flag = msg.GetUInt16();
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
            msg.AddUInt16(Flag);
        }
    }
}