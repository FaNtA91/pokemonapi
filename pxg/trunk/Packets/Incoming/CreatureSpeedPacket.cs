﻿using Pokemon.Constants;

namespace Pokemon.Packets.Incoming
{
    public class CreatureSpeedPacket : IncomingPacket
    {
        public uint CreatureId { get; set; }
        public ushort Speed { get; set; }

        public CreatureSpeedPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.CreatureSpeed;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.CreatureSpeed)
                return false;

            Destination = destination;
            Type = IncomingPacketType.CreatureSpeed;

            CreatureId = msg.GetUInt32();
            Speed = msg.GetUInt16();

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddUInt32(CreatureId);
            msg.AddUInt16(Speed);
        }
    }
}