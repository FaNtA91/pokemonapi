﻿using System;
using Pokemon.Constants;

namespace Pokemon.Packets.Outgoing
{
    public class TurnPacket : OutgoingPacket
    {
        public Constants.Direction Direction { get; set; }

        public TurnPacket(Objects.Client c, Constants.Direction direction)
            : base(c)
        {
            Direction = direction;

            switch (direction)
            {
                case Constants.Direction.Down:
                    Type = OutgoingPacketType.TurnDown;
                    break;
                case Constants.Direction.Up:
                    Type = OutgoingPacketType.TurnUp;
                    break;
                case Constants.Direction.Right:
                    Type = OutgoingPacketType.TurnRight;
                    break;
                case Constants.Direction.Left:
                    Type = OutgoingPacketType.TurnLeft;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(
                        "direction",
                        "Valid directions for turning are Up, Right, Down, and Left.");
            }

            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
        }

        public static bool Send(Objects.Client client, Constants.Direction direction)
        {
            TurnPacket p = new TurnPacket(client, direction);
            return p.Send();
        }
    }
}