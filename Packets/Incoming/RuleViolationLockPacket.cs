using System;
using Pokemon.Constants;

namespace Pokemon.Packets.Incoming
{
    public class RuleViolationLockPacket : IncomingPacket
    {

        public RuleViolationLockPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.RuleViolationLock;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)IncomingPacketType.RuleViolationLock)
                throw new Exception();

            Destination = destination;
            Type = IncomingPacketType.RuleViolationLock;

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
        }
    }
}