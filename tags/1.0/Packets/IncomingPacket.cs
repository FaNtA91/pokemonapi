using Pokemon.Constants;

namespace Pokemon.Packets
{
    public class IncomingPacket : Packet
    {
        public IncomingPacketType Type { get; set; }

        public IncomingPacket(Objects.Client c)
            : base(c) {}
    }
}