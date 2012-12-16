using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pokemon.Util;
using System.Runtime.InteropServices;

namespace Pokemon.Packets
{
    public class IncomingPacket : Packet
    {
        public IncomingPacketType Type { get; set; }

        public IncomingPacket(Objects.Client c)
            : base(c) {}
    }
}
