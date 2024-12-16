using CommunityToolkit.Mvvm.ComponentModel;
using Sales_Manager.Models;
using Sales_Manager.Services;

namespace Sales_Manager.ViewModels.Pages
{
    public partial class OrdersViewModel : BaseViewModel
    {
        private OrderService orderService;

        [ObservableProperty] public List<Order> orders;

        #region
        internal OrdersViewModel(OrderService service)
        {
            this.orderService = service;
        }
        #endregion

        public async Task ResolveProperties()
        {
            IsBusy = true;
            Orders = await orderService.GetAsync();
            IsBusy = false;
        }
    }
}
