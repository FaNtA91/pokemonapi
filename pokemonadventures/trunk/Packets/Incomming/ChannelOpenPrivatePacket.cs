﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pokemon.Packets.Incoming
{
    public class ChannelOpenPrivatePacket : IncomingPacket
    {
        public string Name { get; set; }

        public ChannelOpenPrivatePacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.ChannelOpenPrivate;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.ChannelOpenPrivate)
                return false;

            Destination = destination;
            Type = IncomingPacketType.ChannelOpenPrivate;

            try
            {
                Name = msg.GetString();
            }
            catch (Exception)
            {
                msg.Position = position;
                return false;
            }

            return true;
        }

        public override void ToNetworkMessage(ref NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddString(Name);
        }

        public static bool Send(Objects.Client client, string name)
        {
            ChannelOpenPrivatePacket p = new ChannelOpenPrivatePacket(client);
            p.Name = name;
            return p.Send();
        }
    }
}