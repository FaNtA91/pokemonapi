using System;
using System.Text;
using System.Collections.Generic;
using Pokemon.Constants;

namespace Pokemon.Objects
{
    /// <summary>
    /// Represents a Login Server
    /// </summary>
    public class LoginServer
    {
        public string Server { get; set; }
        public short Port { get; set; }
        public string Version { get; set; }

        public LoginServer()
            : this("") { }

        public LoginServer(string server)
            : this(server, 7009) { }

        public LoginServer(string server, short port)
            : this(server, port, Pokemon.Version.CurrentVersionString) { }

        public LoginServer(string server, short port, string version)
        {
            Server = server;
            Port = port;
            Version = version;
        }

        public override string ToString()
        {
            return Server + ":" + Port;
        }
    }

    public struct Channel
    {
        public ChatChannel Id;
        public string Name;

        public Channel(ChatChannel id, string name)
        {
            Id = id;
            Name = name;
        }
    }

    public struct Rect
    {
        private int top;
        private int bottom;
        private int left;
        private int right;
        private int width;
        private int height;

        public int Top
        {
            get { return top; }
        }

        public int Bottom
        {
            get { return bottom; }
        }

        public int Left
        {
            get { return left; }
        }

        public int Rigth
        {
            get { return right; }
        }

        public int Height
        {
            get { return height; }
        }

        public int Width
        {
            get { return width; }
        }

        public Rect(Util.WinAPI.RECT r)
        {
            top = r.top;
            bottom = r.bottom;
            left = r.left;
            right = r.right;
            width = r.right - r.left;
            height = r.bottom - r.top;
        }
    }

    public struct ClientPathInfo
    {
        public string Path;
        public string Version;

        public ClientPathInfo(string path, string version)
        {
            Path = path;
            Version = version;
        }

        public override string ToString()
        {
            return string.Format("{0} [{1}]", Path, Version);
        }
    }

    public struct CharacterLoginInfo
    {
        public string CharName { get; set; }
        public string WorldName { get; set; }
        public uint WorldIP { get; set; }
        public string WorldIPString { get; set; }
        public ushort WorldPort { get; set; }
    }
}
