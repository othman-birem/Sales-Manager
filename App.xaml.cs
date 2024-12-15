using Sales_Manager.EntitiesManagement;
using Sales_Manager.ViewModels;
using System.Windows;

namespace Sales_Manager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        internal MainWindowViewModel MainVM { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            DesignTimeDbContextFactory factory = new();
            MainVM = new MainWindowViewModel(factory);
            MainWindow win = new(MainVM);
            MainWindow = win;
            win.Show();
        }
    }

}
