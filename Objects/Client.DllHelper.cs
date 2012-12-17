using System;
using System.IO;
using System.Threading;
using Pokemon.Constants;
using Pokemon.Packets;
using Pokemon.Util;

namespace Pokemon.Objects
{
    public partial class Client
    {
        public class DllHelper
        {
            private Client client;
            private AutoResetEvent pipeIsReady;
            private Pipe pipe = null;
            public event EventHandler PipeInitialized;

            internal DllHelper(Client client)
            {
                this.client = client;
                pipeIsReady = new AutoResetEvent(false);
            }

            /// <summary>
            /// Inject a DLL into the process
            /// </summary>
            /// <param name="filename"></param>
            /// <returns></returns>
            public bool Inject(string filename)
            {
                Extract();

                // Get a block of memory to store the filename in the client
                IntPtr remoteAddress = WinApi.VirtualAllocEx(
                    client.ProcessHandle,
                    IntPtr.Zero,
                    (uint)filename.Length,
                    WinApi.AllocationType.Commit | WinApi.AllocationType.Reserve,
                    WinApi.MemoryProtection.ExecuteReadWrite);

                // Write the filename to the client's memory
                client.Memory.WriteStringNoEncoding(remoteAddress.ToInt32(), filename);

                // Start the remote thread, first loading our library
                IntPtr thread = WinApi.CreateRemoteThread(
                    client.ProcessHandle, IntPtr.Zero, 0,
                    WinApi.GetProcAddress(WinApi.GetModuleHandle("Kernel32"), "LoadLibraryA"),
                    remoteAddress, 0, IntPtr.Zero);

                WinApi.WaitForSingleObject(thread, 0xFFFFFFFF); // Infinite

                // Free the memory used for the filename
                WinApi.VirtualFreeEx(
                    client.ProcessHandle, 
                    remoteAddress, 
                    (uint)filename.Length, 
                    WinApi.AllocationType.Release);

                return thread.ToInt32() > 0 && remoteAddress.ToInt32() > 0;
            }

            /// <summary>
            /// Gets a value indicating if a dialog is opened.
            /// </summary>
            public AutoResetEvent PipeIsReady
            {
                get { return pipeIsReady; }
            }

            /// <summary>
            /// Get the pipe that connects client client to it's injected dll
            /// </summary>
            public Pipe Pipe
            {
                get { return pipe; }
            }

            public void InitializePipe()
            {
                if (pipe != null)
                    return;

                pipe = new Pipe(client, "PokemonApi" + client.Process.Id.ToString());
                pipe.OnConnected += new Pipe.PipeNotification(OnPipeConnect);
                client.ContextMenu.AddInternalEvents();
                client.Icon.AddInternalEvents();

                if (!Inject(System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath.ToString(), "Pokemon_Inject.dll")))
                    throw new Pokemon.Exceptions.InjectDLLNotFoundException();
            }

            public void DisconnectPipe()
            {
                byte[] uninjectByte = { 0x2, 0x0, 0xD, 0x0 };
                pipe.Send(new NetworkMessage(uninjectByte));
                pipe = null;
            }

            private void OnPipeConnect()
            {
                //Set constants for displaying
                Packets.Pipes.SetConstantPacket.Send(client, PipeConstantType.PrintName, Pokemon.Addresses.TextDisplay.PrintName);
                Packets.Pipes.SetConstantPacket.Send(client, PipeConstantType.PrintFPS, Pokemon.Addresses.TextDisplay.PrintFPS);
                Packets.Pipes.SetConstantPacket.Send(client, PipeConstantType.ShowFPS, Pokemon.Addresses.TextDisplay.ShowFPS);
                Packets.Pipes.SetConstantPacket.Send(client, PipeConstantType.PrintTextFunc, Pokemon.Addresses.TextDisplay.PrintTextFunc);
                Packets.Pipes.SetConstantPacket.Send(client, PipeConstantType.NopFPS, Pokemon.Addresses.TextDisplay.NopFPS);

                //Context Menus
                Packets.Pipes.SetConstantPacket.Send(client, PipeConstantType.AddContextMenuFunc, Pokemon.Addresses.ContextMenus.AddContextMenuPtr);
                Packets.Pipes.SetConstantPacket.Send(client, PipeConstantType.OnClickContextMenu, Pokemon.Addresses.ContextMenus.OnClickContextMenuPtr);
                Packets.Pipes.SetConstantPacket.Send(client, PipeConstantType.SetOutfitContextMenu, Pokemon.Addresses.ContextMenus.AddSetOutfitContextMenu);
                Packets.Pipes.SetConstantPacket.Send(client, PipeConstantType.PartyActionContextMenu, Pokemon.Addresses.ContextMenus.AddPartyActionContextMenu);
                Packets.Pipes.SetConstantPacket.Send(client, PipeConstantType.CopyNameContextMenu, Pokemon.Addresses.ContextMenus.AddCopyNameContextMenu);
                Packets.Pipes.SetConstantPacket.Send(client, PipeConstantType.TradeWithContextMenu, Pokemon.Addresses.ContextMenus.AddTradeWithContextMenu);
                Packets.Pipes.SetConstantPacket.Send(client, PipeConstantType.LookContextMenu, Pokemon.Addresses.ContextMenus.AddLookContextMenu);
                Packets.Pipes.SetConstantPacket.Send(client, PipeConstantType.OnClickContextMenuVf, Pokemon.Addresses.ContextMenus.OnClickContextMenuVf);

                //winsock recv/send
                Packets.Pipes.SetConstantPacket.Send(client, PipeConstantType.Recv, Pokemon.Addresses.Client.RecvPointer);
                Packets.Pipes.SetConstantPacket.Send(client, PipeConstantType.Send, Pokemon.Addresses.Client.SendPointer);

                //event triggering
                Packets.Pipes.SetConstantPacket.Send(client, PipeConstantType.EventTrigger, Pokemon.Addresses.Client.EventTriggerPointer);

                //image drawing
                Packets.Pipes.SetConstantPacket.Send(client, PipeConstantType.DrawItemFunc, Pokemon.Addresses.DrawItem.DrawItemFunc);

                //skin drawing
                Packets.Pipes.SetConstantPacket.Send(client, PipeConstantType.DrawSkinFunc, Pokemon.Addresses.DrawSkin.DrawSkinFunc);

                //Hook Display functions
                Packets.Pipes.HooksEnableDisablePacket.Send(client, true);
                pipeIsReady.Set();

                if (PipeInitialized != null)
                    PipeInitialized.BeginInvoke(client, new EventArgs(), null, null);

            }

            public void Extract()
            {
                bool doExtract = false;

                if (File.Exists("Pokemon_Inject.dll"))
                {
                    byte[] embeddedBytes = Pokemon.Properties.Resources.Pokemon_Inject;
                    byte[] existingBytes = File.ReadAllBytes("Pokemon_Inject.dll");

                    if (embeddedBytes.Length == existingBytes.Length)
                        doExtract = true;
                    else
                        doExtract = true;
                }
                else
                    doExtract = true;

                if (doExtract)
                {
                    FileStream fileStream = new FileStream("Pokemon_Inject.dll", FileMode.Create);
                    fileStream.Write(Pokemon.Properties.Resources.Pokemon_Inject, 0, (int)Pokemon.Properties.Resources.Pokemon_Inject.Length);
                    fileStream.Close();
                }
            }
        }
    }
}
