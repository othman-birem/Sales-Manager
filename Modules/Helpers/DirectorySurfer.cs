using System.IO;

namespace Sales_Manager.Modules.Helpers
{
    internal static class DirectorySurfer
    {
        internal static string GetDirPath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Temp";
        }
        internal static string GetConfigPath()
        {
            string path = GetDirPath() + "\\config.json";
            if (!File.Exists(path))
            {
                var stream = File.Create(path);
                stream.Close();
            }
            return path;
        }
    }
}
