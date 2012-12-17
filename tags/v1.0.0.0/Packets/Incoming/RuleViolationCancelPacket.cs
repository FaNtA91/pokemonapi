﻿using System;
using Pokemon.Constants;

namespace Pokemon.Packets.Incoming
{
    public class RuleViolationCancelPacket : IncomingPacket
    {
        public string Name { get; set; }

        public RuleViolationCancelPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.RuleViolationCancel;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.RuleViolationCancel)
                throw new Exception();

            Destination = destination;
            Type = IncomingPacketType.RuleViolationCancel;

            Name = msg.GetString();

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddString(Name);
        }
    }
}