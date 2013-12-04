using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pokemon.Addresses;

namespace Pokemon.Objects
{
    /// <summary>
    /// A class that offers interaction with the client's status bar.
    /// </summary>
    public class StatusBar
    {
        /// <summary>
        /// Gets the client associated with this object.
        /// </summary>
        public Client Client { get; private set; }

        /// <summary>
        /// Constructor for this class.
        /// </summary>
        /// <param name="c">The client to be associated with this object.</param>
        public StatusBar(Client c)
        {
            this.Client = c;
        }

        /// <summary>
        /// Gets the currently shown text in the status bar. Returns string.Empty if no text is shown.
        /// </summary>
        /// <returns></returns>
        public string GetText()
        {
            return this.Client.Memory.ReadByte(Addresses.Client.StatusbarTime) > 0 ?
                this.Client.Memory.ReadString(Addresses.Client.StatusbarText, 64) : string.Empty;
        }
        /// <summary>
        /// Sets the status bar's text for 4 seconds.
        /// </summary>
        /// <param name="text">The text to set.</param>
        public void SetText(string text)
        {
            this.SetText(text, 4);
        }
        /// <summary>
        /// Sets the status bar's text for a given amount of time.
        /// </summary>
        /// <param name="text">The text to set.</param>
        /// <param name="seconds">The amount of seconds to display the text for.</param>
        public void SetText(string text, byte seconds)
        {
            this.Client.Memory.WriteString(Addresses.Client.StatusbarText, text);
            this.Client.Memory.WriteByte(Addresses.Client.StatusbarTime, !string.IsNullOrEmpty(text) ? (byte)(seconds * 10) : (byte)0);
        }
    }
}
