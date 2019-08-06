using System.Diagnostics;

namespace MyDiscordBot.Utility
{
    internal static class VersionHelper
    {
        public static string AssemblyVersion
        {
            get
            {
                return _assemblyVersion;
            }
        }

        public static string FileVersion {
            get
            {
                return _fileVersion;
            }
        }


        private static string _assemblyVersion = typeof(VersionHelper).Assembly.GetName().Version.ToString();
        private static string _fileVersion = FileVersionInfo.GetVersionInfo(typeof(VersionHelper).Assembly.Location).FileVersion;
    }
}
