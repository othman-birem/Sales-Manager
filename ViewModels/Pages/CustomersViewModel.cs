using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Sales_Manager.Models;
using Sales_Manager.Services;
using Sales_Manager.Views.Dialogs.Customers;
using System.Windows;

namespace Sales_Manager.ViewModels.Pages
{
    public partial class CustomersViewModel : BaseViewModel
    {
        private CustomerService customerService;

        [ObservableProperty] public List<Customer> customers;
        public CustomersViewModel(CustomerService service)
        {
            customerService = service;
            ResolveProperties();
        }

        [RelayCommand]
        public void Add()
        {
            CustomerDialog dialog = new();
            dialog.ShowDialog();

            if (!dialog.IsClosed)
            {
                IsBusy = true;
                customerService.Add(new Customer() { Name = dialog.NameBox.Text, Phone = dialog.PhoneBox.Text });
                ResolveProperties();
                IsBusy = false;
            }
        }
        [RelayCommand]
        public async void Delete(object id)
        {
            MessageBoxResult result = MessageBox.Show(
                             "Are you sure you want to delete this item?",
                             "Confirmation",
                             MessageBoxButton.YesNo,
                             MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                IsBusy = true;
                await customerService.Delete(int.Parse(((Customer)id).Id.ToString()));
                ResolveProperties();
                IsBusy = false;
            }
        }
        public void ResolveProperties()
        {
            Customers = customerService.Get();
        }
    }
}
