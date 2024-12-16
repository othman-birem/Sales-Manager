using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Sales_Manager.Services;

namespace Sales_Manager.ViewModels.Pages
{
    public partial class LoginPageViewModel : BaseViewModel
    {
        #region
        private bool _isError = false;
        public bool IsError
        {
            get { return _isError; }
            set
            {
                _isError = value;
                OnPropertyChanged(nameof(IsError));
            }
        }
        #endregion

        [ObservableProperty] public string name;
        [ObservableProperty] public string password;

        private UserService _userService;

        public event Action LoggedIn;

        [ObservableProperty] public int lang;

        internal LoginPageViewModel(UserService service)
        {
            _userService = service;
        }

        [RelayCommand]
        public async Task Login()
        {
            IsBusy = true;
            var result = await _userService.Login(Name, Password);
            IsBusy = false;

            IsError = !result;

            if (result)
            {
                LoggedIn?.Invoke();
            }
        }
    }
}
