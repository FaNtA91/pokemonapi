using Pokemon.Addresses;

namespace Pokemon
{
    public partial class Version
    {
        public static void SetVersion100()
        {
            BattleList.Start = 0x613BD0;
            BattleList.End = 0x619990;
            BattleList.StepCreatures = 0xA0;
            BattleList.MaxCreatures = 150;

            Client.StartTime = 0x76D90C;
            Client.XTeaKey = 0x768C7C;
            Client.SocketStruct = 0x768C50;
            Client.SendPointer = 0x597600;

            Client.FrameRatePointer = 0x76CE0C;
            Client.FrameRateCurrentOffset = 0x00;
            Client.FrameRateLimitOffset = 0x58;

            Client.MultiClient = 0xF8944;
            Client.Status = 0x76C2C8;

            Client.SafeMode = 0x76909C;
            Client.FollowMode = Client.SafeMode + 4;
            Client.AttackMode = Client.FollowMode + 4;
            Client.ActionState = 0x76C328;

            Client.CurrentWindow = 0x61E984;
            Client.LastMSGText = 0x76DB78;
            Client.LastMSGAuthor = Client.LastMSGText - 0x28;
            Client.StatusbarText = 0x76D928;
            Client.StatusbarTime = Client.StatusbarText - 4;

            Client.ClickId = 0x76C364;
            Client.ClickCount = Client.ClickId + 4;
            Client.ClickZ = Client.ClickId - 0x68;
            Client.SeeId = Client.ClickId + 12;
            Client.SeeCount = Client.SeeId + 4;
            Client.SeeZ = Client.SeeId - 0x68;
            Client.SeeText = 0x76DB50;

            Client.LoginServerStart = 0x763BB8;
            Client.StepLoginServer = 112;
            Client.DistancePort = 100;
            Client.MaxLoginServers = 10;
            Client.RSA = 0x597610;

            Client.LoginCharList = 0x76C28C;
            Client.LoginSelectedChar = 0x76C288;
            Client.LoginCharListLength = 0x76C290;

            Client.GameWindowRectPointer = 0x12D624;
            Client.DatPointer = 0x768C9C;
            Client.DialogPointer = 0x61E984;
            Client.DialogLeft = 0x14;
            Client.DialogTop = 0x18;
            Client.DialogWidth = 0x1C;
            Client.DialogHeight = 0x20;
            Client.DialogCaption = 0x50;

            Client.LoginAccountNum = 0x76C2C0;
            Client.LoginAccount = 0x76C2B4;
            Client.LoginPassword = 0x76C294;
            Client.LoginPatch = 0x47935E;
            Client.LoginPatch2 = 0x47A2B3;
            Client.LoginPatchOrig = new byte[] { 0xE8, 0x0D, 0x1D, 0x09, 0x00 };
            Client.LoginPatchOrig2 = new byte[] { 0xE8, 0xC8, 0x15, 0x09, 0x00 };

            Container.Start = 0x61C0D0;
            Container.End = 0x61DF90;
            Container.StepContainer = 492;
            Container.StepSlot = 12;
            Container.MaxContainers = 16;
            Container.MaxStack = 100;
            Container.DistanceIsOpen = 0;
            Container.DistanceId = 4;
            Container.DistanceName = 16;
            Container.DistanceVolume = 48;
            Container.DistanceAmount = 56;
            Container.DistanceItemId = 60;
            Container.DistanceItemCount = 64;

            Creature.DistanceId = 0;
            Creature.DistanceType = 3;
            Creature.DistanceName = 4;
            Creature.DistanceX = 36;
            Creature.DistanceY = 40;
            Creature.DistanceZ = 44;
            Creature.DistanceScreenOffsetHoriz = 48;
            Creature.DistanceScreenOffsetVert = 52;
            Creature.DistanceIsWalking = 76;
            Creature.DistanceWalkSpeed = 140;
            Creature.DistanceDirection = 80;
            Creature.DistanceIsVisible = 144;
            Creature.DistanceBlackSquare = 128;
            Creature.DistanceLight = 120;
            Creature.DistanceLightColor = 124;
            Creature.DistanceHPBar = 136;
            Creature.DistanceSkull = 148;
            Creature.DistanceParty = 152;
            Creature.DistanceOutfit = 96;
            Creature.DistanceColorHead = 100;
            Creature.DistanceColorBody = 104;
            Creature.DistanceColorLegs = 108;
            Creature.DistanceColorFeet = 112;
            Creature.DistanceAddon = 116;

            DatItem.StepItems = 0x4C;
            DatItem.Width = 0;
            DatItem.Height = 4;
            DatItem.MaxSizeInPixels = 8;
            DatItem.Layers = 12;
            DatItem.PatternX = 16;
            DatItem.PatternY = 20;
            DatItem.PatternDepth = 24;
            DatItem.Phase = 28;
            DatItem.Sprite = 32;
            DatItem.Flags = 36;
            DatItem.CanLookAt = 0;
            DatItem.WalkSpeed = 40;
            DatItem.TextLimit = 44;
            DatItem.LightRadius = 48;
            DatItem.LightColor = 52;
            DatItem.ShiftX = 56;
            DatItem.ShiftY = 60;
            DatItem.WalkHeight = 64;
            DatItem.Automap = 68;
            DatItem.LensHelp = 72;

            Hotkey.SendAutomaticallyStart = 0x769298;
            Hotkey.SendAutomaticallyStep = 0x01;
            Hotkey.TextStart = 0x7692C0;
            Hotkey.TextStep = 0x100;
            Hotkey.ObjectStart = 0x769208;
            Hotkey.ObjectStep = 0x04;
            Hotkey.ObjectUseTypeStart = 0x7690E8;
            Hotkey.ObjectUseTypeStep = 0x04;
            Hotkey.MaxHotkeys = 36;

            Map.MapPointer = 0x6234D8;
            Map.StepTile = 172;
            Map.StepTileObject = 12;
            Map.DistanceTileObjectCount = 0;
            Map.DistanceTileObjects = 4;
            Map.DistanceObjectId = 0;
            Map.DistanceObjectData = 4;
            Map.DistanceObjectDataEx = 8;
            Map.MaxTileObjects = 13;
            Map.MaxX = 18;
            Map.MaxY = 14;
            Map.MaxZ = 8;
            Map.MaxTiles = 2016;
            Map.ZAxisDefault = 7;
            Map.PlayerTile = 0x3E3A08;

            Map.NameSpy1 = 0x4DF469;
            Map.NameSpy2 = 0x4DF473;
            Map.NameSpy1Default = 19061;
            Map.NameSpy2Default = 16501;

            Map.LevelSpy1 = 0x4E115A;
            Map.LevelSpy2 = 0x4E125F;
            Map.LevelSpy3 = 0x4E12E0;
            Map.LevelSpyPtr = 0x61B608;
            Map.LevelSpyAdd2 = 0x25D8;

            Map.RevealInvisible1 = 0x453AF3;
            Map.RevealInvisible2 = 0x4DE734;

            Player.Flags = 0x00613AF8;
            Player.Experience = 0x00613B64;
            Player.Id = Player.Experience + 12;
            Player.Health = Player.Experience + 8;
            Player.HealthMax = Player.Experience + 4;
            Player.Level = Player.Experience - 4;
            Player.MagicLevel = Player.Experience - 8;
            Player.LevelPercent = Player.Experience - 12;
            Player.MagicLevelPercent = Player.Experience - 16;
            Player.Mana = Player.Experience - 20;
            Player.ManaMax = Player.Experience - 24;
            Player.Soul = Player.Experience - 28;
            Player.Stamina = Player.Experience - 32;
            Player.Capacity = Player.Experience - 36;
            Player.FistPercent = 0x00613AFC;
            Player.ClubPercent = Player.FistPercent + 4;
            Player.SwordPercent = Player.FistPercent + 8;
            Player.AxePercent = Player.FistPercent + 12;
            Player.DistancePercent = Player.FistPercent + 16;
            Player.ShieldingPercent = Player.FistPercent + 20;
            Player.FishingPercent = Player.FistPercent + 24;
            Player.Fist = Player.FistPercent + 28;
            Player.Club = Player.FistPercent + 32;
            Player.Sword = Player.FistPercent + 36;
            Player.Axe = Player.FistPercent + 40;
            Player.Distance = Player.FistPercent + 44;
            Player.Shielding = Player.FistPercent + 48;
            Player.Fishing = Player.FistPercent + 52;
            Player.SlotHead = 0x61C058;
            Player.SlotNeck = Player.SlotHead + 12;
            Player.SlotBackpack = Player.SlotHead + 24;
            Player.SlotArmor = Player.SlotHead + 36;
            Player.SlotRight = Player.SlotHead + 48;
            Player.SlotLeft = Player.SlotHead + 60;
            Player.SlotLegs = Player.SlotHead + 72;
            Player.SlotFeet = Player.SlotHead + 84;
            Player.SlotRing = Player.SlotHead + 96;
            Player.SlotAmmo = Player.SlotHead + 108;
            Player.MaxSlots = 10;
            Player.DistanceSlotCount = 4;
            Player.CurrentTileToGo = 0x613B78;
            Player.TilesToGo = 0x613B7C;
            Player.GoToX = 0x613BB4;
            Player.GoToY = Player.GoToX - 4;
            Player.GoToZ = Player.GoToX - 8;
            Player.RedSquare = 0x613B3C;
            Player.GreenSquare = Player.RedSquare - 4;
            Player.WhiteSquare = Player.GreenSquare - 8;
            Player.AccessN = 0x766DF4;
            Player.AccessS = 0x766DC4;
            Player.TargetId = Player.RedSquare;
            Player.TargetBattlelistId = Player.TargetId - 8;
            Player.TargetBattlelistType = Player.TargetId - 5;
            Player.TargetType = Player.TargetId + 3;
            Player.Z = 0x61E9C0;

            TextDisplay.PrintName = 0x4E228A;
            TextDisplay.PrintFPS = 0x44E753;
            TextDisplay.ShowFPS = 0x611874;
            TextDisplay.PrintTextFunc = 0x4A3C00;
            TextDisplay.NopFPS = 0x44E68F;

            Vip.Start = 0x611890;
            Vip.End = 0x612128;
            Vip.StepPlayers = 0x2C;
            Vip.MaxPlayers = 100;
            Vip.DistanceId = 0;
            Vip.DistanceName = 4;
            Vip.DistanceStatus = 34;
            Vip.DistanceIcon = 40;
        }
    }
}
