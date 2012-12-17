using Pokemon.Constants;

namespace Pokemon.Packets.Incoming
{
    public class PlayerStatusPacket : IncomingPacket
    {
        public ushort Health { get; set; }
        public ushort MaxHealth { get; set; }
        public uint PokemonsCount { get; set; }
        public ulong Experience { get; set; }
        public ushort Level { get; set; }
        public byte LevelPercent { get; set; }
        public ushort Pokemons { get; set; }
        public ushort PokemonsMax { get; set; }
        public byte MagicLevel { get; set; }
        public byte MagicLevelPercent { get; set; }
        public byte Soul { get; set; }
        public ushort Stamina { get; set; }

        public PlayerStatusPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.PlayerStatus;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.PlayerStatus)
                return false;

            Destination = destination;
            Type = IncomingPacketType.PlayerStatus;

            Health = msg.GetUInt16();
            MaxHealth = msg.GetUInt16();
            PokemonsCount = msg.GetUInt32();

            Experience = msg.GetUInt32();

            Level = msg.GetUInt16();

            LevelPercent = msg.GetByte();

            Pokemons = msg.GetUInt16();
            PokemonsMax = msg.GetUInt16();

            MagicLevel = msg.GetByte();
            MagicLevelPercent = msg.GetByte();
            Soul = msg.GetByte();

            Stamina = msg.GetUInt16();

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);

            msg.AddUInt16(Health);
            msg.AddUInt16(MaxHealth);
            msg.AddUInt32(PokemonsCount);

            msg.AddUInt32((uint)Experience);

            msg.AddUInt16(Level);

            msg.AddByte(LevelPercent);

            msg.AddUInt16(Pokemons);
            msg.AddUInt16(PokemonsMax);

            msg.AddByte(MagicLevel);
            msg.AddByte(MagicLevelPercent);
            msg.AddByte(Soul);

            msg.AddUInt16(Stamina);
        }
    }
}