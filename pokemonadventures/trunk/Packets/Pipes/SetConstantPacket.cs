﻿using System;
using Pokemon.Objects;

namespace Pokemon.Packets.Pipes
{
    public class SetConstantPacket : PipePacket
    {
        public PipeConstantType ConstantType { get; set; }
        public uint Value { get; set; }

        public SetConstantPacket(Client client)
            : base(client)
        {
            Type = PipePacketType.SetConstant;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)PipePacketType.SetConstant)
                return false;

            Type = PipePacketType.SetConstant;
            ConstantType = (PipeConstantType)msg.GetByte();
            Value = msg.GetUInt32();

            return true;
        }

        public override byte[] ToByteArray()
        {
            NetworkMessage msg = NetworkMessage.CreateUnencrypted(Client, 6);
            msg.AddByte((byte)Type);

            msg.AddByte((byte)ConstantType);
            msg.AddUInt32((uint)Value);

            return msg.Data;
        }

        public static bool Send(Objects.Client client, PipeConstantType constantType, uint value)
        {
            SetConstantPacket p = new SetConstantPacket(client);
            p.ConstantType = constantType;
            p.Value = value;
            return p.Send();
        }

    }
}