﻿using System;
using Pokemon.Constants;

namespace Pokemon.Packets.Incoming
{
    public class MoveWestPacket : MapPacket
    {
        public MoveWestPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.MoveWest;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination, NetworkMessage outMsg)
        {
            int msgPosition = msg.Position, outMsgPosition = outMsg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.MoveWest)
                return false;

            Destination = destination;
            Type = IncomingPacketType.MoveWest;
            outMsg.AddByte((byte)Type);

            Client.playerLocation.X--;

            return ParseMapDescription(msg, Client.playerLocation.X - 8, Client.playerLocation.Y - 6, Client.playerLocation.Z, 1, 14, outMsg);
        }
    }
}
