
namespace Sales_Manager.ViewModels.Pages
{
    public partial class LoginPageViewModel : BaseViewModel
    {
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
    }
}
