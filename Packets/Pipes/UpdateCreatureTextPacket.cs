﻿using System;
using System.Drawing;
using Pokemon.Constants;
using Pokemon.Objects;

namespace Pokemon.Packets.Pipes
{
    public class UpdateCreatureTextPacket : PipePacket
    {
        public uint CreatureId{get;set;}
        public string CreatureName{get;set;}
        public Location Location{get;set;}
        public string Text{get;set;}

        public UpdateCreatureTextPacket(Client client)
            : base(client)
        {
            Type = PipePacketType.UpdateCreatureText;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)PipePacketType.UpdateCreatureText)
                return false;

            Type = PipePacketType.UpdateCreatureText;
            
            CreatureId = msg.GetUInt32();
            CreatureName = msg.GetString();
            Location = new Location((int)msg.GetUInt32(), (int)msg.GetUInt32(), 0);
            Text = msg.GetString();

            return true;
        }

        public override byte[] ToByteArray()
        {
            NetworkMessage msg = NetworkMessage.CreateUnencrypted(Client, 13 + CreatureName.Length + Text.Length);
            msg.AddByte((byte)Type);

            msg.AddUInt32(CreatureId);
            msg.AddString(CreatureName);
            msg.AddUInt16((ushort)Location.X);
            msg.AddUInt16((ushort)Location.Y);
            msg.AddString(Text);

            return msg.Data;
        }

        public static bool Send(Objects.Client client, uint creatureId , string creatureName, Location location, string text)
        {
            UpdateCreatureTextPacket p = new UpdateCreatureTextPacket(client);

            p.CreatureId = creatureId;
            p.CreatureName = creatureName;
            p.Location = location;
            p.Text = text;

            return p.Send();
        }

    }
}




