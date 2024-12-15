using CommunityToolkit.Mvvm.ComponentModel;
using Sales_Manager.EntitiesManagement;
using Sales_Manager.Services;
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
        #region View/Models
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
        #endregion

        #region services
        UserService userService;
        OrderService orderService;
        CustomerService customerService;
        ItemService itemService;
        ProductService productService;
        #endregion

        internal DesignTimeDbContextFactory designTimeDbContextFactory { get; }

        #region
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
        #endregion

        internal MainWindowViewModel(DesignTimeDbContextFactory factory)
        {
            designTimeDbContextFactory = factory;
            ResolveServices();
            ResolveProperties();

            GOTO_login();
        }

        #region resolvers
        void ResolveProperties()
        {
            LoginPageViewModel = new(userService);
            LoginPageViewModel.LoggedIn += Login;


            NavigationSidebarViewModel = new();
            NavigationSidebarViewModel.Navigated += (e) => NavigateTo(e);

        }
        private void ResolveServices()
        {
            userService = new(designTimeDbContextFactory);
            orderService = new(designTimeDbContextFactory);
            customerService = new(designTimeDbContextFactory);
            itemService = new(designTimeDbContextFactory);
            productService = new(designTimeDbContextFactory);
        }
        #endregion

        #region navigators
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
            ActiveView = LoginPage = new LoginPage(LoginPageViewModel);
        }
        internal void GOTO_Orders()
        {
            ActiveView = Orders = new Orders(OrdersViewModel = new OrdersViewModel(orderService));
        }
        internal void GOTO_customers()
        {
            ActiveView = Customers = new Customers(CustomersViewModel = new CustomersViewModel(customerService));
        }
        internal void GOTO_settings()
        {
            ActiveView = Settings = new Settings(SettingsViewModel = new SettingsViewModel());
        }
        internal void GOTO_accounts()
        {
            ActiveView = Accounts = new Accounts(AccountsViewModel = new AccountsViewModel(userService));
        }
        #endregion

        internal void UpdateLanguage(Languages lang = Languages.English)
        {
            ResourceDictionary? langResource = LoadLanguageResourceDictionary(lang) ?? LoadLanguageResourceDictionary();

            Application.Current.Resources.MergedDictionaries.RemoveAt(0);
            Application.Current.Resources.MergedDictionaries.Insert(0, langResource);
        }

        internal void Login() 
        {
            IsLoggedIn = true;
            NavigationSidebarViewModel.NavigateToPage(1);
        }
    }
}
