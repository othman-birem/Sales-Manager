using Sales_Manager.EntitiesManagement;
using Sales_Manager.ViewModels;
using System.Globalization;
using System.Windows;

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
    }

}
