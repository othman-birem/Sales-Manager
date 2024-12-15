using CommunityToolkit.Mvvm.ComponentModel;
using Sales_Manager.Models;
using Sales_Manager.Services;

namespace Sales_Manager.ViewModels.Pages
{
    public partial class OrdersViewModel : BaseViewModel
    {
        private OrderService orderService;

        [ObservableProperty] public List<Order> orders;

        public OrdersViewModel(OrderService service)
        {
            this.orderService = service;
        }

        public void ResolveProperties()
        {
            Orders = orderService.Get();
        }
    }
}
