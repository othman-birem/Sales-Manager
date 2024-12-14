using Sales_Manager.ViewModels.Pages;
using System.Windows.Controls;

namespace Sales_Manager.Views.Pages.Workspace
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Page
    {
        public Settings(SettingsViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
