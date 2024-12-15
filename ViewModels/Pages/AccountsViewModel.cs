using Sales_Manager.Services;

namespace Sales_Manager.ViewModels.Pages
{
    public partial class AccountsViewModel : BaseViewModel
    {
        private UserService userService;

        public AccountsViewModel(UserService service)
        {
            userService = service;
        }
    }
}
