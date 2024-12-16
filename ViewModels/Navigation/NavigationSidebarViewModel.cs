using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Sales_Manager.ViewModels.Navigation
{
    public partial class NavigationSidebarViewModel : BaseViewModel
    {
        public event Action<int> Navigated;

        [ObservableProperty] public int selectedIndex = 0;

        public NavigationSidebarViewModel()
        {
        }

        [RelayCommand]
        public void NavigateToPage(object index)
        {
            Navigated?.Invoke(int.Parse(index.ToString()));
        }
    }
}
