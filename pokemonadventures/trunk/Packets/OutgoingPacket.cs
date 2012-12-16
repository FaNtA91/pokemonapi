using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pokemon.Util;
using System.Runtime.InteropServices;

namespace Pokemon.Packets
{
    public class OutgoingPacket : Packet
    {
        public OutgoingPacketType Type { get; set; }

        public OutgoingPacket(Objects.Client c)
            : base(c) {}
    }
}
