using CommunityToolkit.Mvvm.ComponentModel;
using Sales_Manager.Models;
using Sales_Manager.Services;

namespace Sales_Manager.ViewModels.Pages
{
    public partial class SalesViewModel : BaseViewModel
    {
        private OrderService orderService;
        private ProductService productService;
        private ItemService itemService;

        [ObservableProperty] public List<Customer> customers;
        [ObservableProperty] public List<Product> products;
        [ObservableProperty] public List<Item> orderItems;

        internal SalesViewModel(OrderService orderService, ProductService productService, ItemService itemService)
        {
            this.orderService = orderService;
            this.productService = productService;
            this.itemService = itemService;
        }
    }
}
