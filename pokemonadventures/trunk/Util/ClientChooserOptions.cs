﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Pokemon.Util
{
    /// <summary>
    /// Options for the ClientChooser class
    /// </summary>
    public class ClientChooserOptions
    {
        /// <summary>
        /// If true, will not open a box if there is only one 
        /// client; just returns that client.
        /// </summary>
        public bool Smart = true;

        /// <summary>
        /// Use a custom title for the client chooser.
        /// </summary>
        public string Title = string.Empty;

        /// <summary>
        /// Show the open tibia server section
        /// </summary>
        public bool ShowOTOption = true;

        /// <summary>
        /// Default value for the Use OT checkbox
        /// </summary>
        public bool UseOT = false;

        /// <summary>
        /// Default value for the server box
        /// </summary>
        public string Server = null;

        /// <summary>
        /// Default value for the port box
        /// </summary>
        public short Port = 7171;

        /// <summary>
        /// Get already running clients and in default locations.
        /// </summary>
        public bool LookUpClients = true;

        /// <summary>
        /// Saves the selected client's path
        /// </summary>
        public bool SaveClientPath = true;

        /// <summary>
        /// Command-line arguments for client
        /// </summary>
        public string Arguments = "";

        /// <summary>
        /// Location of where to read/save the selected client's path. Default: %APPDATA%\PokemonAPI\clientPaths.xml.
        /// </summary>
        public string SavedClientPathsLocation = System.IO.Path.Combine(Pokemon.Constants.TAConstants.AppDataPath, @"clientPaths.xml");

        public List<string> Addresses;

        public ClientChooserOptions()
        {
            clientPaths = new List<string>();
            Addresses = new List<string>(new string[]{
            "login01.Pokemon.com:7171",
            "login02.Pokemon.com:7171",
            "login03.Pokemon.com:7171",
            "login04.Pokemon.com:7171",
            "login05.Pokemon.com:7171",
            "tibia01.cipsoft.com:7171",
            "tibia02.cipsoft.com:7171",
            "tibia03.cipsoft.com:7171",
            "tibia04.cipsoft.com:7171",
            "tibia05.cipsoft.com:7171"
            });

        }

        /// <summary>
        /// 
        /// </summary>
        public List<string> clientPaths;
    }
}
