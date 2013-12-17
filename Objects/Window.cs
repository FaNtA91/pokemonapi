using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using Pokemon.Util;

namespace Pokemon.Objects
{
    public class Window
    {
        public Client client { get; private set; }
        public StatusBar statusBar { get; private set; }
        private bool isVisible;
        private int defBarY, defRectX, defRectY, defRectW, defRectH;

        public Window(Client c)
        {
            this.client = c;
            this.statusBar = new Objects.StatusBar(c);
        }

        public IntPtr Handle
        {
            get
            {
                if (client.Process.MainWindowHandle == IntPtr.Zero)
                    client.Process.Refresh();

                return this.client.Process.MainWindowHandle;
            }
        }

        public string GetCurrentDialogTitle()
        {
            if (this.client.Memory.ReadByte(Pokemon.Addresses.Client.ActionState) != (byte)Pokemon.Constants.ActionState.OpenDialogWindow) return string.Empty;
            return this.client.Memory.ReadString(this.client.Memory.ReadInt32(Pokemon.Addresses.Client.DialogPointer) +
                0x50/*this.Client.Addresses.Dialog.Distances.Title*/);
        }

        /// <summary>
        /// Sets the Tibia client's window text.
        /// </summary>
        /// <param name="text"></param>
        public void SetTitleText(string text)
        {
            // create new thread because it's a synchronous call
            // and if the fps is low, it'll lock up the calling thread
            new Thread(delegate()
            {
                WinAPI.SetWindowText(this.client.Process.MainWindowHandle, text);
            }).Start();
        }

        /// <summary>
        /// Checks whether the Tibia client's window is minimized.
        /// </summary>
        /// <returns></returns>
        public bool IsMinimized()
        {
            return WinAPI.IsIconic(this.Handle);
        }

        /// <summary>
        /// Checks whether the Tibia client's window is maximized.
        /// </summary>
        /// <returns></returns>
        public bool IsMaximized()
        {
            return WinAPI.IsZoomed(this.Handle);
        }

        /// <summary>
        /// Checks whether the Tibia client's window is in focus.
        /// </summary>
        /// <returns></returns>
        public bool IsFocused()
        {
            return WinAPI.GetForegroundWindow() == this.Handle;
        }

        /// <summary>
        /// Bring the window to the front.
        /// </summary>
        public void SetFocused()
        {
            Util.WinAPI.SetForegroundWindow(Handle);
        }

        public bool GetActionStateFreezer()
        {
            return client.Memory.ReadByte(Addresses.Client.ActionStateFreezer) == Addresses.Client.ActionStateFreezed[0];
        }

        public void SetActionStateFreezer(bool state)
        {
            Array.Copy(BitConverter.GetBytes((int)Pokemon.Addresses.Client.ActionState), 0, Addresses.Client.ActionStateOriginal, 1, 4);
            Array.Copy(BitConverter.GetBytes((int)Pokemon.Addresses.Client.ActionState), 0, Addresses.Client.ActionStateFreezed, 2, 4);

            if (state)
            {
                client.Memory.WriteByte((long)Pokemon.Addresses.Client.ActionState, (byte)Pokemon.Constants.ActionState.Using);
                client.Memory.WriteBytes(Addresses.Client.ActionStateFreezer, Addresses.Client.ActionStateFreezed, (uint)Addresses.Client.ActionStateFreezed.Length);
            }
            else
                client.Memory.WriteBytes(Addresses.Client.ActionStateFreezer, Addresses.Client.ActionStateOriginal, (uint)Addresses.Client.ActionStateOriginal.Length);
        }

        /// <summary>
        /// Get the current FPS of the client.
        /// </summary>
        public double GetCurrentFPS()
        {
            int frameRateBegin = client.Memory.ReadInt32(Addresses.Client.FrameRatePointer);
            return client.Memory.ReadDouble(frameRateBegin + Addresses.Client.FrameRateCurrentOffset);
        }

        /// <summary>
        /// Get the FPS limit for the client.
        /// </summary>
        /// <returns></returns>
        public double GetFPSLimit()
        {
            int frameRateBegin = client.Memory.ReadInt32(Addresses.Client.FrameRatePointer);
            return Calculate.ConvertFPS(client.Memory.ReadDouble(frameRateBegin + Addresses.Client.FrameRateLimitOffset));
        }

        /// <summary>
        /// Set the FPS limit for the client.
        /// </summary>
        /// <returns></returns>
        public void SetFPSLimit(double value)
        {
            if (value <= 0)
                value = 1;

            int frameRateBegin = client.Memory.ReadInt32(Addresses.Client.FrameRatePointer);
            client.Memory.WriteDouble(frameRateBegin + Addresses.Client.FrameRateLimitOffset, Calculate.ConvertFPS(value));
        }

        /// <summary>
        /// Flashes the client's window and taskbar.
        /// </summary>
        public void Flash()
        {
            // create new thread because it's a synchronous call
            // and if the fps is low, it'll lock up the calling thread
            new Thread(delegate()
            {
                Util.WinAPI.FlashWindow(Handle, false);
            }).Start();
        }

        /// <summary>
        /// Get and Set window visible setate
        /// </summary>
        /// <returns></returns>
        public void SetVisible(bool value)
        {
            Util.WinAPI.ShowWindow(Handle, value ? Util.WinAPI.SW_SHOW : Util.WinAPI.SW_HIDE);
            isVisible = value;
        }

        /// <summary>
        /// Sets the Tibia client as the topmost application or not.
        /// </summary>
        public void SetTopMost(bool value)
        {
            Util.WinAPI.SetWindowPos(Handle, (value) ? Util.WinAPI.HWND_TOPMOST :
                Util.WinAPI.HWND_NOTOPMOST, 0, 0, 0, 0, Util.WinAPI.SWP_NOMOVE | Util.WinAPI.SWP_NOSIZE);
        }

        /// <summary>
        /// Gets the position of the client, and its outer boundaries
        /// </summary>
        public Rect Size()
        {
            Util.WinAPI.RECT r = new Pokemon.Util.WinAPI.RECT();
            Util.WinAPI.GetWindowRect(Handle, ref r);
            return new Rect(r);
        }

        #region World Only View
        /// <summary>
        /// Gets world only view.
        /// </summary>
        /// <returns></returns>
        public bool GetWorldOnlyView()
        {
            int screenBar;
            screenBar = client.Memory.ReadInt32(Addresses.Client.GameWindowBar);
            return client.Memory.ReadInt32(screenBar + 0x70) == Size().Height;
        }

        /// <summary>
        /// Sets world only view.
        /// </summary>
        /// <returns></returns>
        public void SetWorldOnlyView()
        {
            int screenRect, screenBar;
            screenRect = client.Memory.ReadInt32(Addresses.Client.GameWindowRectPointer);
            screenRect = client.Memory.ReadInt32(screenRect + 0x18 + 0x04);
            screenBar = client.Memory.ReadInt32(Addresses.Client.GameWindowBar);

            if (client.Memory.ReadInt32(screenBar + 0x70) != Size().Height)
            {
                defBarY = client.Memory.ReadInt32(screenBar + 0x70);
                defRectX = client.Memory.ReadInt32(screenRect + 0x14);
                defRectY = client.Memory.ReadInt32(screenRect + 0x18);
                defRectW = client.Memory.ReadInt32(screenRect + 0x1C);
                defRectH = client.Memory.ReadInt32(screenRect + 0x20);
                client.Memory.WriteInt32(screenBar + 0x70, Size().Height);
                client.Memory.WriteInt32(screenRect + 0x14, 0);
                client.Memory.WriteInt32(screenRect + 0x18, 0);
                client.Memory.WriteInt32(screenRect + 0x1C, Size().Width);
                client.Memory.WriteInt32(screenRect + 0x20, Size().Height);
            }
            else if (defBarY != 0 && defRectX != 0 &&
                defRectY != 0 && defRectW != 0 && defRectH != 0)
            {
                client.Memory.WriteInt32(screenBar + 0x70, defBarY);
                client.Memory.WriteInt32(screenRect + 0x14, defRectX);
                client.Memory.WriteInt32(screenRect + 0x18, defRectY);
                client.Memory.WriteInt32(screenRect + 0x1C, defRectW);
                client.Memory.WriteInt32(screenRect + 0x20, defRectH);
            }
        }
        #endregion

        #region Wide Screen
        /// <sumary>
        /// Gets wide screen view
        /// </sumary>
        public bool GetWideScreenView()
        {
            int screenRect, screenBar;
            screenRect = client.Memory.ReadInt32(Addresses.Client.GameWindowRectPointer);
            screenRect = client.Memory.ReadInt32(screenRect + 0x18 + 0x04);
            screenBar = client.Memory.ReadInt32(Addresses.Client.GameWindowBar);
            return client.Memory.ReadInt32(screenRect + 0x14) == 5;
        }

        /// <sumary>
        /// Sets wide screen view
        /// </sumary>
        public void SetWideScreenView()
        {

            int screenRect, screenBar;
            screenRect = client.Memory.ReadInt32(Pokemon.Addresses.Client.GameWindowRectPointer);
            screenRect = client.Memory.ReadInt32(screenRect + 0x18 + 0x4);
            screenBar = client.Memory.ReadInt32(Pokemon.Addresses.Client.GameWindowBar);

            if (!GetWideScreenView())
            {
                defRectX = client.Memory.ReadInt32(screenRect + 0x14);
                defRectW = client.Memory.ReadInt32(screenRect + 0x1C);
                client.Memory.WriteInt32(screenRect + 0x14, 5);
                client.Memory.WriteInt32(screenRect + 0x1C, client.Memory.ReadInt32(screenBar + 0x74) - 10);
            }
            else if (defRectX != 0 && defRectW != 0)
            {
                client.Memory.WriteInt32(screenRect + 0x14, defRectX);
                client.Memory.WriteInt32(screenRect + 0x1C, defRectW);
            }
        }
        #endregion

        /// <summary>
        /// Gets the last seen item/tile id.
        /// </summary>
        public ushort GetLastSeenId()
        {
            return BitConverter.ToUInt16(client.Memory.ReadBytes(Addresses.Client.SeeId, 2), 0);
        }

        /// <summary>
        /// Gets the amount of the last seen item/tile. Returns 0 if the item is not
        /// stackable. Also gets the amount of charges in a rune starting at 1.
        /// </summary>
        public ushort GetLastSeenCount()
        {
            return BitConverter.ToUInt16(client.Memory.ReadBytes(Addresses.Client.SeeCount, 2), 0);
        }

        /// <summary>
        /// Gets the text of the last seen item/tile.
        /// </summary>
        public string GetLastSeenText()
        {
            return client.Memory.ReadString(Addresses.Client.SeeText);
        }


        /// <summary>
        /// Gets the dialog pointer
        /// </summary>
        public uint GetDialogPointer()
        {
            return client.Memory.ReadUInt32(Addresses.Client.DialogPointer);
        }

        /// <summary>
        /// Gets a value indicating if a dialog is opened.
        /// </summary>
        public bool IsDialogOpen()
        {
            return GetDialogPointer() != 0;
        }

        /// <summary>
        /// Gets the position of the current opened dialog. Returns null if dialog is not opened.
        /// </summary>
        public Rectangle GetDialogPosition()
        {
            if (!IsDialogOpen())
                return new Rectangle();

            return new Rectangle(client.Memory.ReadInt32(GetDialogPointer() + Addresses.Client.DialogLeft),
                client.Memory.ReadInt32(GetDialogPointer() + Addresses.Client.DialogTop),
                client.Memory.ReadInt32(GetDialogPointer() + Addresses.Client.DialogWidth),
                client.Memory.ReadInt32(GetDialogPointer() + Addresses.Client.DialogHeight));
        }

        /// <summary>
        /// Gets the caption text of the current opened dialog. Returns null if dialog is not opened.
        /// </summary>
        public string GetDialogCaption()
        {
            if (!IsDialogOpen())
                return "";

            return client.Memory.ReadString(GetDialogPointer() + Addresses.Client.DialogCaption);
        }

        #region Attack Mode, Follow Mode && Safe Mode
        /// <summary>
        /// Gets the attack mode.
        /// </summary>
        public Constants.AttackMode GetAttackMode()
        {
            return (Constants.AttackMode)client.Memory.ReadByte(Addresses.Client.AttackMode);
        }

        /// <summary>
        /// Sets the attack mode.
        /// </summary>
        public bool SetAttackMode(Constants.AttackMode mode)
        {
            return client.Memory.WriteByte(Addresses.Client.AttackMode, (byte)mode);
        }

        /// <summary>
        /// Gets or sets the follow mode.
        /// </summary>
        public Constants.FollowMode GetFollowMode()
        {
            return (Constants.FollowMode)client.Memory.ReadByte(Addresses.Client.FollowMode);
        }

        /// <summary>
        /// Sets the follow mode.
        /// </summary>
        public bool SetFollowMode(Constants.FollowMode mode)
        {
            return client.Memory.WriteByte(Addresses.Client.FollowMode, (byte)mode);
        }

        /// <summary>
        /// Gets the follow mode.
        /// </summary>
        public byte GetSafeMode()
        {
            return client.Memory.ReadByte(Addresses.Client.SafeMode);
        }

        /// <summary>
        /// Sets the follow mode.
        /// </summary>
        public bool SetSafeMode(byte mode)
        {
            return client.Memory.WriteByte(Addresses.Client.SafeMode, (byte)mode);
        }
        #endregion
    }
}
