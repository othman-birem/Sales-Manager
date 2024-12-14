using CommunityToolkit.Mvvm.ComponentModel;
using Sales_Manager.Models.Common;
using Sales_Manager.Modules.Common;
using Sales_Manager.ViewModels.Navigation;
using Sales_Manager.ViewModels.Pages;
using Sales_Manager.Views.Pages.Authentication;
using Sales_Manager.Views.Pages.Workspace;
using System.Windows;
using System.Windows.Controls;

namespace Sales_Manager.ViewModels
{
    public partial class MainWindowViewModel : BaseViewModel
    {
        [ObservableProperty] public Page activeView;

        [ObservableProperty] public LoginPage loginPage;
        [ObservableProperty] public LoginPageViewModel loginPageViewModel;

        [ObservableProperty] public Orders orders;
        [ObservableProperty] public OrdersViewModel ordersViewModel;

        [ObservableProperty] public NavigationSidebarViewModel navigationSidebarViewModel;

        [ObservableProperty] public Settings settings;
        [ObservableProperty] public SettingsViewModel settingsViewModel;

        [ObservableProperty] public Customers customers;
        [ObservableProperty] public CustomersViewModel customersViewModel;

        [ObservableProperty] public Accounts accounts;
        [ObservableProperty] public AccountsViewModel accountsViewModel;

        private bool _isLoggedIn = false;

        public bool IsLoggedIn
        {
            get { return _isLoggedIn; }
            set
            {
                if (_isLoggedIn != value)
                {
                    _isLoggedIn = value;
                    OnPropertyChanged(nameof(IsLoggedIn));
                }
            }
        }

        public MainWindowViewModel()
        {
            GOTO_login();
            ResolveProperties();
        }

        private void ResolveProperties()
        {
            LoginPage.SubmitButton.Click += Login;
            NavigationSidebarViewModel = new();
            NavigationSidebarViewModel.Navigated += (e) => NavigateTo(e);

        }
        private void NavigateTo(int idx)
        {
            switch(idx)
            {
                case 1:
                    GOTO_Orders();
                    break;
                case 2:
                    GOTO_customers();
                    break;
                case 3:
                    GOTO_accounts();
                    break;
                case 4:
                    GOTO_settings();
                    break;
            }
        }
        internal void GOTO_login()
        {
            ActiveView = LoginPage = new LoginPage(LoginPageViewModel = new LoginPageViewModel());
        }
        internal void GOTO_Orders()
        {
            ActiveView = Orders = new Orders(OrdersViewModel = new OrdersViewModel());
        }
        internal void GOTO_customers()
        {
            ActiveView = Customers = new Customers(CustomersViewModel = new CustomersViewModel());
        }
        internal void GOTO_settings()
        {
            ActiveView = Settings = new Settings(SettingsViewModel = new SettingsViewModel());
        }
        internal void GOTO_accounts()
        {
            ActiveView = Accounts = new Accounts(AccountsViewModel = new AccountsViewModel());
        }


        internal void UpdateLanguage(MetaData.Languages lang = MetaData.Languages.English)
        {
            ResourceDictionary? langResource = GlobalApi.LoadLanguageResourceDictionary(lang) ?? GlobalApi.LoadLanguageResourceDictionary();

            Application.Current.Resources.MergedDictionaries.RemoveAt(0);
            Application.Current.Resources.MergedDictionaries.Insert(0, langResource);
        }

        internal void Login(object sender, RoutedEventArgs e) 
        {
            if (LoginPage.usernameField.Text.Equals("admin") && LoginPage.passwordField.Password.Equals("admin"))
            {
                IsLoggedIn = true;
                NavigationSidebarViewModel.NavigateToPage(1);
            }
            else
            {
                LoginPageViewModel.IsError = true;
            }
        }
    }
}
