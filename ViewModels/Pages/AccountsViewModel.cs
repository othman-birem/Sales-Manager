using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Sales_Manager.Services;
using System.Windows;

namespace Sales_Manager.ViewModels.Pages
{
    public partial class AccountsViewModel : BaseViewModel
    {
        private UserService userService;

        [ObservableProperty] public List<User> users;

        internal AccountsViewModel(UserService service)
        {
            userService = service;
            ResolveProperties();
        }

        private void ResolveProperties()
        {
            Users = userService.Get();
        }

        [RelayCommand]
        public async Task Delete(object id)
        {
            MessageBoxResult result = MessageBox.Show(
                 "Are you sure you want to delete this item?",
                 "Confirmation",
                 MessageBoxButton.YesNo,
                 MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                IsBusy = true;
                await userService.Delete(int.Parse(((User)id).Id.ToString()));
                ResolveProperties();
                IsBusy = false;
            }
        }
    }
}
