using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pokemon.Constants
{
    public static class TAConstants
    {
        public static string CurrentTibiaVersion = "1.0";
        public static string AppDataPath = System.IO.Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "PokemonApi");
    }
}
