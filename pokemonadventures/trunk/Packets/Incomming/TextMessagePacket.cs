﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pokemon.Packets.Incoming
{
    public class TextMessagePacket : IncomingPacket
    {
        public StatusMessage Color { get; set; }
        public string Message { get; set; }

        public TextMessagePacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.TextMessage;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.TextMessage)
                return false;

            Destination = destination;
            Type = IncomingPacketType.TextMessage;

            try
            {
                Color = (StatusMessage)msg.GetByte();
                Message = msg.GetString();
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
            msg.AddByte((byte)Color);
            msg.AddString(Message);
        }

        public static bool Send(Objects.Client client, StatusMessage color, string msg)
        {
            TextMessagePacket p = new TextMessagePacket(client);
            p.Color = color;
            p.Message = msg;

            return p.Send();
        }

    }
}