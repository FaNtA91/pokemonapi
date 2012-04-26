﻿using Pokemon.Constants;

namespace Pokemon.Packets.Incoming
{
    public class TileTransformThingPacket : IncomingPacket
    {
        public ushort ThingId { get; set; }
        public byte StackPosition { get; set; }
        public Objects.Location Position { get; set; }
        public Objects.Item Item { get; set; }
        public uint CreatureId { get; set; }
        public byte CreatureDirection { get; set; }

        public TileTransformThingPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.TileTransformThing;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.TileTransformThing)
                return false;

            Destination = destination;
            Type = IncomingPacketType.TileTransformThing;

            Position = msg.GetLocation();
            StackPosition = msg.GetByte();
            ThingId = msg.GetUInt16();

            if (ThingId == 0x0061 || ThingId == 0x0062 || ThingId == 0x0063)
            {
                CreatureId = msg.GetUInt32();
                CreatureDirection = msg.GetByte();
            }
            else
            {
                Item = new Pokemon.Objects.Item(Client, ThingId, 0);
                Item.Location = Pokemon.Objects.ItemLocation.FromLocation(Position);

                if (Item.HasExtraByte)
                    Item.Count = msg.GetByte();
            }

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);

            msg.AddLocation(Position);
            msg.AddByte(StackPosition);
            msg.AddUInt16(ThingId);

            if (ThingId == 0x0061 || ThingId == 0x0062 || ThingId == 0x0063)
            {
                msg.AddUInt32(CreatureId);
                msg.AddByte(CreatureDirection);
            }
            else
            {
                if (Item.HasExtraByte)
                    msg.AddByte(Item.Count);
            }
        }
    }
}