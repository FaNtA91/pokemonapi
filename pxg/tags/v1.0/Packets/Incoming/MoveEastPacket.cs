﻿using System;
using Pokemon.Constants;

namespace Pokemon.Packets.Incoming
{
    public class MoveEastPacket : MapPacket
    {

        public MoveEastPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.MoveEast;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination, NetworkMessage outMsg)
        {
            int msgPosition = msg.Position, outMsgPosition = outMsg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.MoveEast)
                return false;

            Destination = destination;
            Type = IncomingPacketType.MoveEast;
            outMsg.AddByte((byte)Type);

            Client.playerLocation.X++;

            return ParseMapDescription(msg, Client.playerLocation.X + 9, Client.playerLocation.Y - 6, Client.playerLocation.Z, 1, 14, outMsg);
        }
    }
}
