﻿using Pokemon.Constants;

namespace Pokemon.Packets.Incoming
{
    public class CreatureHealthPacket : IncomingPacket
    {
        public uint CreatureId { get; set; }
        public byte Health { get; set; }

        public CreatureHealthPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.CreatureHealth;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.CreatureHealth)
                return false;

            Destination = destination;
            Type = IncomingPacketType.CreatureHealth;

            CreatureId = msg.GetUInt32();
            Health = msg.GetByte();

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddUInt32(CreatureId);
            msg.AddByte(Health);
        }

    }
}