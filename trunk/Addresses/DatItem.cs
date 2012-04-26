using System;
using System.Collections.Generic;
using System.Text;

namespace Pokemon.Addresses
{
    public static class DatItem
    {
        public static uint StepItems;
        public static uint Width;
        public static uint Height;
        public static uint MaxSizeInPixels;
        public static uint Layers;
        public static uint PatternX;
        public static uint PatternY;
        public static uint PatternDepth;
        public static uint Phase;
        public static uint Sprite;
        public static uint Flags;
        public static uint CanEquip;
        public static uint CanLookAt;
        public static uint WalkSpeed;
        public static uint TextLimit;
        public static uint LightRadius;
        public static uint LightColor;
        public static uint ShiftX;
        public static uint ShiftY;
        public static uint WalkHeight;
        public static uint Automap; // Minimap color
        public static uint LensHelp;

        public enum Flag : uint
        {
            IsGround,
            TopOrder1,
            TopOrder2,
            TopOrder3,
            IsContainer,
            IsStackable,
            IsCorpse,
            IsUsable,
            IsRune,
            IsWritable,
            IsReadable,
            IsFluidContainer,
            IsSplash,
            Blocking,
            IsImmovable,
            BlocksMissiles,
            BlocksPath,
            IsPickupable,
            IsHangable,
            IsHangableHorizontal,
            IsHangableVertical,
            IsRotatable,
            IsLightSource,
            Floorchange,
            IsShifted,
            HasHeight,
            IsLayer,
            IsIdleAnimation,
            HasAutoMapColor,
            HasHelpLens,
            Unknown,
            IgnoreStackpos
        }

        public enum Help
        {
            IsLadder = 0x44C,
            IsSewer = 0x44D,
            IsDoor = 0x450,
            IsDoorWithLock = 0x451,
            IsRopeSpot = 0x44E,
            IsSwitch = 0x44F,
            IsStairs = 0x452,
            IsMailbox = 0x453,
            IsDepot = 0x454,
            IsTrash = 0x455,
            IsHole = 0x456,
            HasSpecialDescription = 0x457,
            IsReadOnly = 0x458
        }
    }
}
