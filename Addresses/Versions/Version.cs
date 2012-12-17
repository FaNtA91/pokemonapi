using System;
using System.Diagnostics;

namespace Pokemon
{
    public partial class Version
    {
        public static ushort CurrentVersion = 100;
        public static string CurrentVersionString = "1.0";

        public static string VersionToString(ushort version)
        {
            int main = version / 100;
            int secondary = version - main * 100;
            return String.Format("{0}.{1:00}", main, secondary);
        }

        public static ushort StringToVersion(string versionString)
        {
            string[] split = versionString.Split('.');
            int main = int.Parse(split[0]);
            int secondary = int.Parse(split[1]);
            return (ushort)(main * 100 + secondary);
        }

        public static void Set(string version)
            {

            CurrentVersion = StringToVersion(version);
            CurrentVersionString = version;
            switch (version)
            {
                case "1.0":
                    SetVersion100();
                    break;
                default:
                    throw new Exceptions.VersionNotSupportedException("Incorrect Pokemon client version " + CurrentVersionString + ". That API works only with PokeXGames client.");
            }

        }

    }
}
