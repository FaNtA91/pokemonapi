using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pokemon.Constants
{
    public static class TAConstants
    {
        public static string AppDataPath = System.IO.Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "PokemonAPI");
        public static string CurrentTibiaVersion = "8.50";
    }
}
