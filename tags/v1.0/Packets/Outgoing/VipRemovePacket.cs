﻿using Pokemon.Constants;

namespace Pokemon.Packets.Outgoing
{
    public class VipRemovePacket : OutgoingPacket
    {
        public uint Id { get; set; }

        public VipRemovePacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.VipRemove;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.VipRemove)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.VipRemove;

            Id = msg.GetUInt32();

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);

            msg.AddUInt32(Id);
        }

        public static bool Send(Objects.Client client, uint id)
        {
            VipRemovePacket p = new VipRemovePacket(client);
            p.Id = id;
            return p.Send();
        }
    }
}