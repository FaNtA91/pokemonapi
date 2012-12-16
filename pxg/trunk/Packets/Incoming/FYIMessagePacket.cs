﻿using Pokemon.Constants;

namespace Pokemon.Packets.Incoming
{
    public class FyiMessagePacket : IncomingPacket
    {

        public string Message { get; set; }

        public FyiMessagePacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.FyiMessage;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.FyiMessage)
                return false;

            Destination = destination;
            Type = IncomingPacketType.FyiMessage;

            Message = msg.GetString();

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddString(Message);
        }

        public static bool Send(Objects.Client client, string message)
        {
            FyiMessagePacket p = new FyiMessagePacket(client);
            p.Message = message;
            return p.Send();
        }
    }
}