﻿using Pokemon.Constants;

namespace Pokemon.Packets.Incoming
{
    public class TileRemoveThingPacket : IncomingPacket
    {
        public byte StackPosition { get; set; }
        public Objects.Location Position { get; set; }

        public TileRemoveThingPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.TileRemoveThing;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.TileRemoveThing)
                return false;

            Destination = destination;
            Type = IncomingPacketType.TileRemoveThing;

            Position = msg.GetLocation();
            StackPosition = msg.GetByte();

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddLocation(Position);
            msg.AddByte(StackPosition);
        }
    }
}