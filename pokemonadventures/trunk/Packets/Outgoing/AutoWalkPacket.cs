using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pokemon.Packets.Outgoing
{
    public class AutoWalkPacket : OutgoingPacket
    {
        public List<Constants.Direction> Directions { get; set; }

        public AutoWalkPacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.AutoWalk;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.AutoWalk)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.AutoWalk;

            Directions = new List<Pokemon.Constants.Direction> { };
            byte count = msg.GetByte();

            for (int i = 0; i < count; i++)
            {
                Constants.Direction direction;
                byte dir = msg.GetByte();

                switch (dir)
                {
                    case 1: direction = Pokemon.Constants.Direction.Right; break;
                    case 2: direction = Pokemon.Constants.Direction.UpRight; break;
                    case 3: direction = Pokemon.Constants.Direction.Up; break;
                    case 4: direction = Pokemon.Constants.Direction.UpLeft; break;
                    case 5: direction = Pokemon.Constants.Direction.Left; break;
                    case 6: direction = Pokemon.Constants.Direction.DownLeft; break;
                    case 7: direction = Pokemon.Constants.Direction.Down; break;
                    case 8: direction = Pokemon.Constants.Direction.DownRight; break;
                    default: continue;
                }

                Directions.Add(direction);
            }

            return true;
        }

        public override void ToNetworkMessage(ref NetworkMessage msg)
        {
            msg.AddByte((byte)Type);

            msg.AddByte((byte)Directions.Count);

            foreach (var dir in Directions)
            {
                switch (dir)
                {
                    case Pokemon.Constants.Direction.Right: msg.AddByte(1); break;
                    case Pokemon.Constants.Direction.UpRight: msg.AddByte(2); break;
                    case Pokemon.Constants.Direction.Up: msg.AddByte(3); break;
                    case Pokemon.Constants.Direction.UpLeft: msg.AddByte(4); break;
                    case Pokemon.Constants.Direction.Left: msg.AddByte(5); break;
                    case Pokemon.Constants.Direction.DownLeft: msg.AddByte(6); break;
                    case Pokemon.Constants.Direction.Down: msg.AddByte(7); break;
                    case Pokemon.Constants.Direction.DownRight: msg.AddByte(8); break;
                    default: continue;
                }
            }
        }

        public static bool Send(Objects.Client client, List<Constants.Direction> directions)
        {
            AutoWalkPacket p = new AutoWalkPacket(client);
            p.Directions = directions;
            return p.Send();
        }
    }
}