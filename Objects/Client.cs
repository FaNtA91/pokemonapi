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

        public string cachedVersion { get; private set; }
        public ushort cachedVersionNumber { get; private set; }

        public Process Process { get; private set; }
        public IntPtr Handle { get; private set; }

        private int startTime;

        internal Location playerLocation = Location.Invalid;

        // References to commonly used objects
        public BattleList BattleList { get; private set; }
        public Map Map { get; private set; }
        public Inventory Inventory { get; private set; }
        public Console Console { get; private set; }
        public Screen Screen { get; private set; }
        public Util.AStarPathFinder PathFinder { get; private set; }
        public MemoryHelper Memory { get; private set; }
        public IOHelper IO { get; private set; }
        public Window Window { get; private set; }
        public LoginHelper Login { get; private set; }
        public DllHelper Dll { get; private set; }
        public InputHelper Input { get; private set; }
        public PlayerHelper Player { get; private set; }

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
            Process = p;
            Process.Exited += new EventHandler(process_Exited);
            Process.EnableRaisingEvents = true;

            // Wait until we can really access the process
            Process.WaitForInputIdle();

            while (Process.MainWindowHandle == IntPtr.Zero)
            {
                Process.Refresh();
                System.Threading.Thread.Sleep(5);
            }

            // Save a copy of the handle so the process doesn't have to be opened
            // every read/write operation
            Handle = Util.WinAPI.OpenProcess(Util.WinAPI.PROCESS_ALL_ACCESS, 0, (uint)Process.Id);

            PathFinder = new Pokemon.Util.AStarPathFinder(this);
            Inventory = new Objects.Inventory(this);
            BattleList = new Objects.BattleList(this);
            Map = new Objects.Map(this);
            Memory = new MemoryHelper(this);
            Window = new Window(this);
            IO = new IOHelper(this);
            Login = new LoginHelper(this);
            Dll = new DllHelper(this);
            Input = new InputHelper(this);
            Player = new PlayerHelper(this);

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
            Util.WinAPI.CloseHandle(Handle);
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
                else if (LoggedIn())
                {
                    return new Location(
                        (int)Player.X,
                        (int)Player.Y,
                        (int)Player.Z);
                }

                return Location.Invalid;
            }
        }

        public bool HasExited()
        {
            return Process.HasExited;
        }

        /// <summary>
        /// Get the status of the client.
        /// </summary>
        /// <returns></returns>
        public Constants.LoginStatus GetStatus()
        {
            return (Constants.LoginStatus)Memory.ReadByte(Addresses.Client.Status);
        }

        /// <summary>
        /// Check whether or not the client is logged in
        /// </summary>
        public bool LoggedIn()
        {
            return GetStatus() == Constants.LoginStatus.LoggedIn;
        }

        /// <summary>
        /// Gets the last seen item/tile id.
        /// </summary>
        public ushort GetLastSeenId()
        {
            return BitConverter.ToUInt16(Memory.ReadBytes(Addresses.Client.SeeId, 2), 0);
        }

        /// <summary>
        /// Gets the amount of the last seen item/tile. Returns 0 if the item is not
        /// stackable. Also gets the amount of charges in a rune starting at 1.
        /// </summary>
        public ushort GetLastSeenCount()
        {
            return BitConverter.ToUInt16(Memory.ReadBytes(Addresses.Client.SeeCount, 2), 0);
        }

        /// <summary>
        /// Gets the text of the last seen item/tile.
        /// </summary>
        public string GetLastSeenText()
        {
            return Memory.ReadString(Addresses.Client.SeeText);
        }

        /// <summary>
        /// Get the client's version
        /// </summary>
        /// <returns></returns>
        public string GetVersion()
        {

            if (cachedVersion == null)
            {
                cachedVersion = Process.MainModule.FileVersionInfo.FileVersion;
            }
            return cachedVersion;
        }

        /// <summary>
        /// Get the client's version as a number
        /// </summary>
        /// <returns></returns>
        public ushort GetVersionNumber()
        {
            if (cachedVersionNumber == 0)
            {
                cachedVersionNumber = Pokemon.Version.StringToVersion(GetVersion());
            }
            return cachedVersionNumber;
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
            Util.WinAPI.PROCESS_INFORMATION pi = new Pokemon.Util.WinAPI.PROCESS_INFORMATION();
            Util.WinAPI.STARTUPINFO si = new Pokemon.Util.WinAPI.STARTUPINFO();

            if (arguments == null)
                arguments = "";

            Util.WinAPI.CreateProcess(path, " " + arguments, IntPtr.Zero, IntPtr.Zero,
                false, Util.WinAPI.CREATE_SUSPENDED, IntPtr.Zero,
                System.IO.Path.GetDirectoryName(path), ref si, out pi);

            IntPtr handle = Util.WinAPI.OpenProcess(Util.WinAPI.PROCESS_ALL_ACCESS, 0, pi.dwProcessId);
            Process p = Process.GetProcessById(Convert.ToInt32(pi.dwProcessId));
            Util.Memory.WriteByte(handle, (long)Pokemon.Addresses.Client.MultiClient, Pokemon.Addresses.Client.MultiClientJMP);
            Util.WinAPI.ResumeThread(pi.hThread);
            p.WaitForInputIdle();
            Util.Memory.WriteByte(handle, (long)Pokemon.Addresses.Client.MultiClient, Pokemon.Addresses.Client.MultiClientJNZ);
            Util.WinAPI.CloseHandle(handle);
            Util.WinAPI.CloseHandle(pi.hProcess);
            Util.WinAPI.CloseHandle(pi.hThread);

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
            Pokemon.Version.Set(GetVersion());
            string s = "[" + GetVersion() + "] ";
            if (!LoggedIn())
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
                Util.WinAPI.GetClassName(process.MainWindowHandle, classname, 12);

                if (classname.ToString().Equals("DirectX5Wnd", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (version == null)
                    {
                        client = new Client(process);
                        if (!offline || !client.LoggedIn())
                            clients.Add(client);
                    }
                    else if (process.MainModule.FileVersionInfo.FileVersion == version)
                    {
                        clients.Add(new Client(process));
                        if (!offline || !client.LoggedIn())
                            clients.Add(client);
                    }
                }
            }
            return clients;
        }

        public void Close()
        {
            if (Process != null && !Process.HasExited)
                Process.Kill();
        }

        #endregion

        #region Client's Objects

        /// <summary>
        /// Get the client's player.
        /// </summary>
        /// <returns></returns>
        public Player GetPlayer()
        {
            if (!LoggedIn())
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

        /// <summary>
        /// Get the time the client was started.
        /// </summary>
        /// <returns></returns>
        public int StartTime
        {
            get { return startTime; }
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
