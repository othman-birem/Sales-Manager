using CommunityToolkit.Mvvm.ComponentModel;

namespace Sales_Manager.ViewModels
{
    public abstract partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsFree))]
        private bool isBusy;

        public bool IsFree{ get { return !IsBusy; } }
    }
}
