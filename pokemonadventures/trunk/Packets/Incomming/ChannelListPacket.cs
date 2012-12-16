﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pokemon.Packets.Incoming
{
    public class ChannelListPacket : IncomingPacket
    {
        public List<Objects.Channel> Channels { get; set; }

        public ChannelListPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.ChannelList;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.ChannelList)
                throw new Exception();

            Destination = destination;
            Type = IncomingPacketType.ChannelList;

            try
            {
                byte count = msg.GetByte();
                Channels = new List<Pokemon.Objects.Channel> { };

                for (int i = 0; i < count; i++)
                {
                    ushort id = msg.GetUInt16();
                    Channels.Add(new Pokemon.Objects.Channel((ChatChannel)id, msg.GetString()));
                }
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
            msg.AddByte((byte)Channels.Count);

            foreach (Objects.Channel c in Channels)
            {
                msg.AddUInt16((ushort)c.Id);
                msg.AddString(c.Name);
            }
        }

        public static bool Send(Objects.Client client, List<Objects.Channel> channels)
        {
            ChannelListPacket p = new ChannelListPacket(client);
            p.Channels = channels;
            return p.Send();
        }
    }
}