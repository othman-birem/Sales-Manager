using Newtonsoft.Json;
using Sales_Manager.Modules.Helpers;
using System.IO;
using System.Windows;

namespace Sales_Manager
{
    internal class AppConfiguration
    {
        private static string PathToCollection = DirectorySurfer.GetConfigPath();


        #region State Appliance
        public double windowHeight = double.NaN;
        public double windowWidth = double.NaN;
        public double windowTop = double.NaN;
        public double windowLeft = double.NaN;
        public WindowState windowState = WindowState.Maximized;


        public void SetState(Window window)
        {
            window.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            window.MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth;
            window.WindowStartupLocation = WindowStartupLocation.Manual;
            if (window.Height != double.NaN) { window.Height = SystemParameters.FullPrimaryScreenHeight * 0.70; }
            else { window.Height = windowHeight; }
            if (window.Width != double.NaN) { window.Width = SystemParameters.FullPrimaryScreenWidth * 0.80; }
            else { window.Width = windowWidth; }
            if (window.Top != double.NaN) { window.Top = SystemParameters.FullPrimaryScreenHeight * 0.10; }
            if (window.Left != double.NaN) { window.Left = SystemParameters.FullPrimaryScreenWidth * 0.10; }
            window.WindowState = windowState;
        }
        public void getState(Window window)
        {
            if (window == null) { return; }
            windowTop = window.Top;
            windowLeft = window.Left;
            windowHeight = window.Height;
            windowWidth = window.Width;
            windowState = window.WindowState;
        }
        #endregion

        private static AppConfiguration GetDB()
        {
            using (StreamReader reader = new(PathToCollection))
            {
                string CollectionText = reader.ReadToEnd();

#pragma warning disable CS8603 // Possible null reference return.
                return string.IsNullOrEmpty(CollectionText) ?
                    new AppConfiguration() :
                    JsonConvert.DeserializeObject<AppConfiguration>(CollectionText);
#pragma warning restore CS8603 // Possible null reference return.
            }
        }

        public void Load()
        {
            AppConfiguration temp = GetDB();
            
            windowHeight = temp.windowHeight;
            windowWidth = temp.windowWidth;
            windowState = temp.windowState;
            windowTop = temp.windowTop;
            windowLeft = temp.windowLeft;
        }

        public void Save()
        {
            _save(this);
        }
        private void _save(AppConfiguration list)
        {
            using (StreamWriter writer = new(PathToCollection))
            {
                string text = JsonConvert.SerializeObject(list, Formatting.Indented);
                writer.Write(text);
            }
        }
    }
}
