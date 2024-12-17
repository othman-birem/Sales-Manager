using Sales_Manager.EntitiesManagement;
using Sales_Manager.Models;
using Sales_Manager.ViewModels;
using System.Globalization;
using System.Net.Sockets;
using System.Windows;
using System.Windows.Threading;

namespace Sales_Manager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        internal MainWindowViewModel MainVM { get; private set; }
        internal AppConfiguration config { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            CultureInfo culture = new("en-US");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            config = new AppConfiguration();
            config.Load();

            DesignTimeDbContextFactory factory = new();
            MainVM = new MainWindowViewModel(factory);

            MainVM.Favorites = new System.Collections.ObjectModel.ObservableCollection<FavoriteShortcut>(config.favShortcuts);

            MainWindow win = new(MainVM);
            config.SetState(win);
            MainWindow = win;
            win.Show();
        }
        protected override void OnExit(ExitEventArgs e)
        {
            config?.Save();

            base.OnExit(e);
        }

        private async void DispatcherOnUnhandledException(DispatcherUnhandledExceptionEventArgs args)
        {
            args.Handled = true;
            string errorType = args.Exception.InnerException
                is TimeoutException
                or SocketException
                or NotImplementedException
                || (args.Exception
                is TimeoutException
                or SocketException
                or NotImplementedException)
                ? "Network error" : "Unknown error";
            MessageBox.Show($"{errorType}: {args.Exception.GetType().Name}");
        }
        internal void AddShortcut(FavoriteShortcut shortcut)
        {
            var identical = MainVM.Favorites.FirstOrDefault(x => x.Equals(shortcut));
            if (identical != null)
            {
                MainVM.Favorites.Remove(identical);
                config.favShortcuts.Remove(identical);
                return;
            }
            MainVM.Favorites.Add(shortcut);
            config.favShortcuts.Add(shortcut);
        }
    }

}
