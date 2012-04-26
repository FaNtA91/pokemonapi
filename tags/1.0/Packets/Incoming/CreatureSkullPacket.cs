﻿using Pokemon.Constants;

namespace Pokemon.Packets.Incoming
{
    public class CreatureSkullPacket : IncomingPacket
    {
        public uint CreatureId { get; set; }
        public byte CreatureSkull { get; set; }

        public CreatureSkullPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.CreatureSkull;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.CreatureSkull)
                return false;

            Destination = destination;
            Type = IncomingPacketType.CreatureSkull;

            CreatureId = msg.GetUInt32();
            CreatureSkull = msg.GetByte();

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddUInt32(CreatureId);
            msg.AddByte(CreatureSkull);
        }
    }
}