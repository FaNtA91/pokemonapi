﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pokemon.Packets.Incoming
{
    public class RuleViolationRemovePacket : IncomingPacket
    {
        public string Name { get; set; }

        public RuleViolationRemovePacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.RuleViolationRemove;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.RuleViolationRemove)
                throw new Exception();

            Destination = destination;
            Type = IncomingPacketType.RuleViolationRemove;

            try
            {
                Name = msg.GetString();
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
            msg.AddString(Name);
        }
    }
}