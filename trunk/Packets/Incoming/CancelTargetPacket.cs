using System;
using Pokemon.Constants;

namespace Pokemon.Packets.Incoming
{
    public class CancelTargetPacket : IncomingPacket
    {
        public uint Count { get; set; }

        public CancelTargetPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.CancelTarget;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)IncomingPacketType.CancelTarget)
                throw new Exception();

            Destination = destination;
            Type = IncomingPacketType.CancelTarget;

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
        }

        public static bool Send(Objects.Client client)
        {
            CancelTargetPacket p = new CancelTargetPacket(client);
            return p.Send();
        }
    }
}