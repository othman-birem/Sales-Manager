using Sales_Manager.Models.Common;
using Sales_Manager.Modules.Common;
using Sales_Manager.ViewModels.Pages;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Sales_Manager.Views.Pages.Authentication
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage(LoginPageViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }

        public void KeyPressed(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) { SubmitButton.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent)); }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }
        private void LangBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GlobalApi.GetCurrentMainWindowViewModel()?.UpdateLanguage((MetaData.Languages)LangBox.SelectedIndex);
        }
    }
}
