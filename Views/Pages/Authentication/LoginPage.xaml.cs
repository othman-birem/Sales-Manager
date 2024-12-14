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

        private void viewPassword_Click(object sender, RoutedEventArgs e)
        {
            if (passwordField.PasswordChar == '*')
            {
                SwitchIcon("/Assets/view_checked.png");
                passwordTxtBox.Text = passwordField.Password;
                passwordField.Visibility = Visibility.Collapsed;
                passwordTxtBox.Visibility = Visibility.Visible;
                passwordField.PasswordChar = '\0';
            }
            else
            {
                SwitchIcon("/Assets/view.png");
                passwordField.Password = passwordTxtBox.Text;
                passwordTxtBox.Visibility = Visibility.Collapsed;
                passwordField.Visibility = Visibility.Visible;
                passwordField.PasswordChar = '*';
            }
        }
        public void KeyPressed(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) { SubmitButton.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent)); }
        }
        private void SwitchIcon(string path)
        {
            BitmapImage image = new(new Uri(path, UriKind.Relative));
            viewImage.Source = image;
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
