using CommunityToolkit.Mvvm.ComponentModel;
using Sales_Manager.Services;

namespace Sales_Manager.ViewModels.Pages
{
    public partial class AccountsViewModel : BaseViewModel
    {
        private UserService userService;

        [ObservableProperty] public List<User> users;

        public AccountsViewModel(UserService service)
        {
            userService = service;
            ResolveProperties();
        }

        private void ResolveProperties()
        {
            Users = userService.Get();
        }
    }
}
