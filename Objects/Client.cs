using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Pokemon.Objects
{
    /// <summary>
    /// Represents a single Client. Contains wrapper methods 
    /// for memory, packet sending, battlelist, and slots. Also contains
    /// any "helper methods" that automate tasks, such as making a rune.
    /// </summary>
    public partial class Client
    {
        #region Variables

        private string cachedVersion = null;
        private ushort cachedVersionNumber = 0;

        private Process process;
        private IntPtr processHandle;

        private int startTime;

        internal Location playerLocation = Location.Invalid;

        // References to commonly used objects
        private BattleList battleList;
        private Map map;
        private Inventory inventory;
        private Console console;
        private Screen screen;
        private Util.AStarPathFinder pathFinder;
        private MemoryHelper memory;
        private WindowHelper window;
        private IOHelper io;
        private LoginHelper login;
        private DllHelper dll;
        private InputHelper input;
        private PlayerHelper player;

        #endregion

        #region Events

        /// <summary>
        /// Event raised when the client is exited.
        /// </summary>
        public event EventHandler Exited;

        private void process_Exited(object sender, EventArgs e)
        {
            if (Exited != null)
                Exited.Invoke(this, e);
        }

        #endregion

        #region Constructor/Destructor

        /// <summary>
        /// "Support" constructor
        /// </summary>
        /// <param name="p">used when necessary to use classes such as packet builder when working clientless</param>
        public Client() { }

        /// <summary>
        /// Main constructor
        /// </summary>
        /// <param name="p">the client's process object</param>
        public Client(Process p)
        {
            process = p;
            process.Exited += new EventHandler(process_Exited);
            process.EnableRaisingEvents = true;

            // Wait until we can really access the process
            process.WaitForInputIdle();

            while (process.MainWindowHandle == IntPtr.Zero)
            {
                process.Refresh();
                System.Threading.Thread.Sleep(5);
            }

            // Save a copy of the handle so the process doesn't have to be opened
            // every read/write operation
            processHandle = Util.WinApi.OpenProcess(Util.WinApi.PROCESS_ALL_ACCESS, 0, (uint)process.Id);

            pathFinder = new Pokemon.Util.AStarPathFinder(this);

            memory = new MemoryHelper(this);
            window = new WindowHelper(this);
            io = new IOHelper(this);
            login = new LoginHelper(this);
            dll = new DllHelper(this);
            input = new InputHelper(this);
            player = new PlayerHelper(this);

            // Save the start time (it isn't changing)
            startTime = Memory.ReadInt32(Addresses.Client.StartTime);
        }

        /// <summary>
        /// Finalize this client, closing the handle.
        /// Called before the object is garbage collected.
        /// </summary>
        ~Client()
        {
            // Close the process handle
            Util.WinApi.CloseHandle(ProcessHandle);
        }
        #endregion

        #region Properties

        public Location PlayerLocation
        {
            get
            {
                if (IO.UsingProxy)
                {
                    return playerLocation;
                }
                else if (LoggedIn)
                {
                    return new Location(
                        (int)Player.X,
                        (int)Player.Y,
                        (int)Player.Z);
                }

                return Location.Invalid;
            }
        }

        public bool HasExited
        {
            get { return process.HasExited; }
        }

        /// <summary>
        /// Get the status of the client.
        /// </summary>
        /// <returns></returns>
        public Constants.LoginStatus Status
        {
            get { return (Constants.LoginStatus)Memory.ReadByte(Addresses.Client.Status); }
        }

        /// <summary>
        /// Check whether or not the client is logged in
        /// </summary>
        public bool LoggedIn
        {
            get { return Status == Constants.LoginStatus.LoggedIn; }
        }

        /// <summary>
        /// Get and set the Statusbar text (the white text above the console).
        /// </summary>
        public string Statusbar
        {
            get { return Memory.ReadString(Addresses.Client.StatusbarText); }
            set
            {
                Memory.WriteByte(Addresses.Client.StatusbarTime, 50);
                Memory.WriteString(Addresses.Client.StatusbarText, value);
                Memory.WriteByte(Addresses.Client.StatusbarText + value.Length, 0x00);
            }
        }

        /// <summary>
        /// Gets the last seen item/tile id.
        /// </summary>
        public ushort LastSeenId
        {
            get { return BitConverter.ToUInt16(Memory.ReadBytes(Addresses.Client.SeeId, 2), 0); }
        }

        /// <summary>
        /// Gets the amount of the last seen item/tile. Returns 0 if the item is not
        /// stackable. Also gets the amount of charges in a rune starting at 1.
        /// </summary>
        public ushort LastSeenCount
        {
            get { return BitConverter.ToUInt16(Memory.ReadBytes(Addresses.Client.SeeCount, 2), 0); }
        }

        /// <summary>
        /// Gets the text of the last seen item/tile.
        /// </summary>
        public string LastSeenText
        {
            get { return Memory.ReadString(Addresses.Client.SeeText); }
        }

        /// <summary>
        /// Get the client's version
        /// </summary>
        /// <returns></returns>
        public string Version
        {
            get
            {
                if (cachedVersion == null)
                {
                    cachedVersion = process.MainModule.FileVersionInfo.FileVersion;
                }
                return cachedVersion;
            }
        }

        /// <summary>
        /// Get the client's version as a number
        /// </summary>
        /// <returns></returns>
        public ushort VersionNumber
        {
            get
            {
                if (cachedVersionNumber == 0)
                {
                    cachedVersionNumber = Pokemon.Version.StringToVersion(Version);
                }
                return cachedVersionNumber;
            }
        }

        /// <summary>
        /// Gets the dialog pointer
        /// </summary>
        public uint DialogPointer
        {
            get { return Memory.ReadUInt32(Addresses.Client.DialogPointer); }
        }

        /// <summary>
        /// Gets a value indicating if a dialog is opened.
        /// </summary>
        public bool IsDialogOpen
        {
            get { return DialogPointer != 0; }
        }

        /// <summary>
        /// Gets the position of the current opened dialog. Returns null if dialog is not opened.
        /// </summary>
        public Rectangle DialogPosition
        {
            get
            {
                if (!IsDialogOpen)
                    return new Rectangle();

                return new Rectangle(Memory.ReadInt32(DialogPointer + Addresses.Client.DialogLeft),
                    Memory.ReadInt32(DialogPointer + Addresses.Client.DialogTop),
                    Memory.ReadInt32(DialogPointer + Addresses.Client.DialogWidth),
                    Memory.ReadInt32(DialogPointer + Addresses.Client.DialogHeight));
            }
        }

        /// <summary>
        /// Gets the caption text of the current opened dialog. Returns null if dialog is not opened.
        /// </summary>
        public string DialogCaption
        {
            get
            {
                if (!IsDialogOpen)
                    return "";

                return Memory.ReadString(DialogPointer + Addresses.Client.DialogCaption);
            }
        }

        /// <summary>
        /// Gets or sets the attack mode.
        /// </summary>
        public Constants.Attack AttackMode
        {
            get { return (Constants.Attack)Memory.ReadByte(Addresses.Client.AttackMode); }
            set { Memory.WriteByte(Addresses.Client.AttackMode, (byte)value); }
        }

        /// <summary>
        /// Gets or sets the follow mode.
        /// </summary>
        public Constants.Follow FollowMode
        {
            get { return (Constants.Follow)Memory.ReadByte(Addresses.Client.FollowMode); }
            set { Memory.WriteByte(Addresses.Client.FollowMode, (byte)value); }
        }

        /// <summary>
        /// Gets or sets the follow mode.
        /// </summary>
        public byte SafeMode
        {
            get { return Memory.ReadByte(Addresses.Client.SafeMode); }
            set { Memory.WriteByte(Addresses.Client.FollowMode, value); }
        }

        /// <summary>
        /// Gets or sets the action state.
        /// </summary>
        public Constants.ActionState ActionState
        {
            get { return (Constants.ActionState)Memory.ReadByte(Addresses.Client.ActionState); }
            set { Memory.WriteByte(Addresses.Client.ActionState, (byte)value); }
        }

        #endregion

        #region Open Client
        /// <summary>
        /// Open a client at the default path
        /// </summary>
        /// <returns></returns>
        public static Client Open()
        {
            return Open(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), @"PokeXGames\Pokemon.exe"));
        }

        /// <summary>
        /// Open a client from the specified path
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static Client Open(string path)
        {
            ProcessStartInfo psi = new ProcessStartInfo(path);
            psi.UseShellExecute = true; // to avoid opening currently running.
            psi.WorkingDirectory = System.IO.Path.GetDirectoryName(path);
            return Open(psi);
        }

        /// <summary>
        /// Open a client from the specified path with arguments
        /// </summary>
        /// <param name="path"></param>
        /// <param name="arguments"></param>
        /// <returns></returns>
        public static Client Open(string path, string arguments)
        {
            ProcessStartInfo psi = new ProcessStartInfo(path, arguments);
            psi.UseShellExecute = true; // to avoid opening currently running.
            psi.WorkingDirectory = System.IO.Path.GetDirectoryName(path);
            return Open(psi);
        }

        /// <summary>
        /// Opens a client given a process start info object.
        /// </summary>
        public static Client Open(ProcessStartInfo psi)
        {
            Process p = Process.Start(psi);
            return new Client(p);
        }


        public static Client OpenMC()
        {
            return OpenMC(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), @"PokeXGames\Pokemon.exe"), "");
        }

        /// <summary>
        /// Open a client with path.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static Client OpenMC(string path)
        {
            return OpenMC(path, null);
        }

        /// <summary>
        /// Opens a client with dynamic multi-clienting support
        /// </summary>
        public static Client OpenMC(string path, string arguments)
        {
            Util.WinApi.PROCESS_INFORMATION pi = new Pokemon.Util.WinApi.PROCESS_INFORMATION();
            Util.WinApi.STARTUPINFO si = new Pokemon.Util.WinApi.STARTUPINFO();

            if (arguments == null)
                arguments = "";

            Util.WinApi.CreateProcess(path, " " + arguments, IntPtr.Zero, IntPtr.Zero,
                false, Util.WinApi.CREATE_SUSPENDED, IntPtr.Zero,
                System.IO.Path.GetDirectoryName(path), ref si, out pi);

            IntPtr handle = Util.WinApi.OpenProcess(Util.WinApi.PROCESS_ALL_ACCESS, 0, pi.dwProcessId);
            Process p = Process.GetProcessById(Convert.ToInt32(pi.dwProcessId));
            Util.Memory.WriteByte(handle, (long)Pokemon.Addresses.Client.MultiClient, Pokemon.Addresses.Client.MultiClientJMP);
            Util.WinApi.ResumeThread(pi.hThread);
            p.WaitForInputIdle();
            Util.Memory.WriteByte(handle, (long)Pokemon.Addresses.Client.MultiClient, Pokemon.Addresses.Client.MultiClientJNZ);
            Util.WinApi.CloseHandle(handle);
            Util.WinApi.CloseHandle(pi.hProcess);
            Util.WinApi.CloseHandle(pi.hThread);

            return new Client(p);
        }

        #endregion

        #region Override Functions
        /// <summary>
        /// String identifier for this client.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            Pokemon.Version.Set(Version);
            string s = "[" + Version + "] ";
            if (!LoggedIn)
                s += "Not logged in.";
            else
                s += GetPlayer().Name;

            return s;
        }
        #endregion

        #region Client Processes
        /// <summary>
        /// Get a list of all the open clients. Class method.
        /// </summary>
        /// <returns></returns>
        public static List<Client> GetClients()
        {

            return GetClients(null);
        }

        /// <summary>
        /// Get a list of all the open clients of certain version. Class method.
        /// </summary>
        /// <returns></returns>
        public static List<Client> GetClients(string version)
        {
            return GetClients(version, false);
        }

        /// <summary>
        /// Get a list of all the open clients of certain version. Class method.
        /// </summary>
        /// <returns></returns>
        public static List<Client> GetClients(string version, bool offline)
        {
            List<Client> clients = new List<Client>();
            Client client = null;

            foreach (Process process in Process.GetProcesses())
            {
                StringBuilder classname = new StringBuilder();
                Util.WinApi.GetClassName(process.MainWindowHandle, classname, 12);

                if (classname.ToString().Equals("DirectX5Wnd", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (version == null)
                    {
                        client = new Client(process);
                        if (!offline || !client.LoggedIn)
                            clients.Add(client);
                    }
                    else if (process.MainModule.FileVersionInfo.FileVersion == version)
                    {
                        clients.Add(new Client(process));
                        if (!offline || !client.LoggedIn)
                            clients.Add(client);
                    }
                }
            }
            return clients;
        }

        public void Close()
        {
            if (process != null && !process.HasExited)
                process.Kill();
        }

        #endregion

        #region Client's Objects

        /// <summary>
        /// Get the client's player.
        /// </summary>
        /// <returns></returns>
        public Player GetPlayer()
        {
            if (!LoggedIn)
                throw new Exceptions.NotLoggedInException();

            int playerId = Memory.ReadInt32(Addresses.Player.Id);

            return new Player(this, BattleList.GetCreatures().
                First(c => c.Id == playerId).Address);
        }

        public Hotkey GetHotkey(byte number)
        {
            if (number < 0 || number > Addresses.Hotkey.MaxHotkeys)
                return null;
            else
                return new Hotkey(this, number);
        }

        public MemoryHelper Memory
        {
            get { return memory; }
        }

        public WindowHelper Window
        {
            get { return window; }
        }

        public IOHelper IO
        {
            get { return io; }
        }

        public LoginHelper Login
        {
            get { return login; }
        }

        public DllHelper Dll
        {
            get { return dll; }
        }

        public InputHelper Input
        {
            get { return input; }
        }

        public PlayerHelper Player
        {
            get { return player; }
        }

        /// <summary>
        /// Get the client's battlelist.
        /// </summary>
        /// <returns></returns>
        public BattleList BattleList
        {
            get
            {
                if (battleList == null) battleList = new BattleList(this);
                return battleList;
            }
        }

        /// <summary>
        /// Get the client's map.
        /// </summary>
        /// <returns></returns>
        public Map Map
        {
            get
            {
                if (map == null) map = new Map(this);
                return map;
            }
        }

        /// <summary>
        /// Get the client's inventory.
        /// </summary>
        /// <returns></returns>
        public Inventory Inventory
        {
            get
            {
                if (inventory == null) inventory = new Inventory(this);
                return inventory;
            }
        }

        /// <summary>
        /// Get the client's console.
        /// </summary>
        /// <returns></returns>
        public Console Console
        {
            get
            {
                if (console == null) console = new Console(this);
                return console;
            }
        }

        /// <summary>
        /// Get the client's screen (for displaying text)
        /// </summary>
        public Screen Screen
        {
            get
            {
                if (screen == null) screen = new Screen(this);
                return screen;
            }
        }

        /// <summary>
        /// Get the time the client was started.
        /// </summary>
        /// <returns></returns>
        public int StartTime
        {
            get { return startTime; }
        }

        /// <summary>
        /// Get the client's process.
        /// </summary>
        public Process Process
        {
            get { return process; }
        }

        /// <summary>
        /// Get the client's process handle
        /// </summary>
        public IntPtr ProcessHandle
        {
            get { return processHandle; }
        }

        public Util.AStarPathFinder PathFinder
        {
            get { return pathFinder; }
        }

        #endregion

        #region Client Actions

        /// <summary>
        /// Logout.
        /// </summary>
        /// <returns></returns>
        public bool Logout()
        {
            return Packets.Outgoing.LogoutPacket.Send(this);
        }

        public bool Send(byte[] packet)
        {
            return MyPacket.SendPacket(this, packet);
        }

        #endregion
    }
}
