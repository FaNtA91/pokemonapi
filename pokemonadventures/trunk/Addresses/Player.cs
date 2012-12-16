namespace Pokemon.Addresses
{
    /// <summary>
    /// Player memory addresses.
    /// </summary>
    public static class Player
    {
        public static uint Exp = 0x632EC4; // 8.50

        public static uint GoToX = Exp + 80;
        public static uint GoToY = Exp + 76;
        public static uint GoToZ = Exp + 72;

        public static uint Id = Exp + 12;
        public static uint HP = Exp + 8;
        public static uint HPMax = Exp + 4;

        public static uint Level = Exp - 4;
        public static uint MagicLevel = Exp - 8;
        public static uint LevelPercent = Exp - 12;
        public static uint MagicLevelPercent = Exp - 16;

        public static uint Mana = Exp - 20;
        public static uint ManaMax = Exp - 24;

        public static uint Soul = Exp - 28;
        public static uint Stamina = Exp - 32;
        public static uint Cap = Exp - 36;

        public static uint Fishing = Exp - 52;
        public static uint Shielding = Exp - 56;
        public static uint Distance = Exp - 60;
        public static uint Axe = Exp - 64;
        public static uint Sword = Exp - 68;
        public static uint Club = Exp - 72;
        public static uint Fist = Exp - 76;

        public static uint FishingPercent = Exp - 80;
        public static uint ShieldingPercent = Exp - 84;
        public static uint DistancePercent = Exp - 88;
        public static uint AxePercent = Exp - 92;
        public static uint SwordPercent = Exp - 96;
        public static uint ClubPercent = Exp - 100;
        public static uint FistPercent = Exp - 104;
        public static uint Flags = Exp - 108;

        /// <summary>
        /// Total number of equipment slots (accessed 0-10)
        /// </summary>
        public static int MaxSlots = 11;
        public static uint SlotHead = 0x63F310; // 8.50
        public static uint SlotNeck = SlotHead + 12;
        public static uint SlotBackpack = SlotHead + 24;
        public static uint SlotArmor = SlotHead + 36;
        public static uint SlotRight = SlotHead + 48;
        public static uint SlotLeft = SlotHead + 60;
        public static uint SlotLegs = SlotHead + 72;
        public static uint SlotFeet = SlotHead + 84;
        public static uint SlotRing = SlotHead + 96;
        public static uint SlotAmmo = SlotHead + 108;

        public static uint DistanceSlotCount = 4;


        public static uint CurrentTileToGo = 0x632ED8; // 8.50
        public static uint TilesToGo = 0x632EDC; // 8.50


        public static uint RedSquare = 0x632E9C; // 8.50
        public static uint GreenSquare = RedSquare - 4;
        public static uint WhiteSquare = GreenSquare - 8;

        public static uint AccessN = 0; // 8.0
        public static uint AccessS = 0; // 8.0

        public static uint TargetID = RedSquare;    
        public static uint TargetBListID = TargetID - 8; 
        public static uint TargetBListType = TargetID - 5;
        public static uint TargetType = TargetID + 3;

        /// <summary>
        /// Static address for player Z, used for level spy
        /// </summary>
        public static uint Z = 0x641C78; // 8.50

        public static uint Y = Z + 4; // 8.50
        public static uint X = Z + 8; // 8.50
    }
}
