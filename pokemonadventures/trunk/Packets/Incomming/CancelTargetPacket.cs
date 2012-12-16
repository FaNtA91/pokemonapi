﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pokemon.Packets.Incoming
{
    public class CancelTargetPacket : IncomingPacket
    {

        public CancelTargetPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.CancelTarget;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)IncomingPacketType.CancelTarget)
                throw new Exception();

            Destination = destination;
            Type = IncomingPacketType.CancelTarget;

            return true;
        }

        public override void ToNetworkMessage(ref NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
        }
    }
}