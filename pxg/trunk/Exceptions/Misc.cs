using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Pokemon.Exceptions
{
    /// <summary>
    /// Exception used mainly in Objects.Client when a player is not logged in.
    /// </summary>
    public class NotLoggedInException : InvalidOperationException { }

    /// <summary>
    /// Thrown when the proxy is not enabled, packet.dll is not found and a method tries to use packet.dll to send a packet.
    /// </summary>
    public class PacketDllNotFoundException : InvalidOperationException { }

    /// <summary>
    /// Thrown when a method requires the client to be using a proxy, ie. sending a packet to the client.
    /// </summary>
    public class ProxyRequiredException : InvalidOperationException { }

    /// <summary>
    /// Thrown when a method expects the proxy to be connected, but it is not.
    /// </summary>
    public class ProxyDisconnectedException : InvalidOperationException { }

    /// <summary>
    /// Thrown when client tries to inject DLL in addition to use Pipe but cannot found it
    /// </summary>
    public class InjectDLLNotFoundException : InvalidOperationException { }

    public static class Handler
    {
        public static void Handle(Exception e)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("PokemonApi has encountered an error. Please report the text of this error to the developer of this program. You can copy the text by pressing ctrl+c.");
            sb.AppendLine();
            sb.AppendLine(e.ToString());

            MessageBox.Show(sb.ToString(), "PokemonApi Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
