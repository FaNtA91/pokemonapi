using Pokemon.Constants;

namespace Pokemon.Packets
{
    public class OutgoingPacket : Packet
    {
        public OutgoingPacketType Type { get; set; }

        public OutgoingPacket(Objects.Client c)
            : base(c) {}
    }
}
