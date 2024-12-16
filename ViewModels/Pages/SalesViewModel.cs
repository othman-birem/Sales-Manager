using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Sales_Manager.Models;
using Sales_Manager.Services;
using Sales_Manager.ViewModels.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Forms;

namespace Sales_Manager.ViewModels.Pages
{
    public partial class SalesViewModel : BaseViewModel
    {
        private OrderService orderService;
        private ProductService productService;
        private CustomerService customerService;
        private ItemService itemService;

        [ObservableProperty] public List<Customer> customers;
        [ObservableProperty] public List<Product> products;
        //[ObservableProperty] public ObservableCollection<Item> orderItems;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Total))]
        [NotifyPropertyChangedFor(nameof(NetTotal))]
        public ObservableCollection<ItemViewModel> orderItemViewModels;


        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(HasOrder))]
        public Order currentOrder;

        [ObservableProperty] public int? selectedCustomer;

        public decimal Total => OrderItemViewModels.Sum(item => item.TotalPrice);
        public decimal NetTotal => OrderItemViewModels.Sum(item => item.NetTotal);

        public bool HasOrder { get { return CurrentOrder != null; } }

        internal SalesViewModel(OrderService orderService, ProductService productService, CustomerService customerService, ItemService itemService)
        {
            this.orderService = orderService;
            this.productService = productService;
            this.customerService = customerService;
            this.itemService = itemService;

            ResolveProperties();
        }

        private async void ResolveProperties()
        {
            IsBusy = true;
            Customers = customerService.Get();
            Products = productService.Get();
            //OrderItems ??= new();

            //OrderItemViewModels = new ObservableCollection<ItemViewModel>(
            //        orderService.GetOrderItems((int)CurrentOrder?.Id).Select(item => new ItemViewModel(item, new List<Product>()))
            //        );
            OrderItemViewModels = new ObservableCollection<ItemViewModel>();

            PropertyChanged += ResolvePropertyChange;
            IsBusy = false;
        }

        private void ResolvePropertyChange(object sender, PropertyChangedEventArgs e)
        {
            switch(e.PropertyName)
            {
                case "":
                    break;
            }
        }

        #region commands
        [RelayCommand]
        public async Task NewOrder()
        {
            if (SelectedCustomer == null) { MessageBox.Show("Please select a customer first."); return; }

            CurrentOrder = new Order()
            {
                CustomerId = (int)SelectedCustomer,
            };
            await orderService.Add(CurrentOrder);
        }

        [RelayCommand]
        public async Task SaveOrder()
        {
            var itemsToSave = OrderItemViewModels.Select(vm => vm.ToItem()).ToList();
            await orderService.SaveAsync();
            await itemService.AddRangeAsync(itemsToSave);
        }

        [RelayCommand]
        public void AddOrderItem()
        {
            if (CurrentOrder == null) { MessageBox.Show("Create an Order first."); return; }

            Item temp = new()
            {
                OrderId = CurrentOrder.Id,
            };
            OrderItemViewModels.Add(new ItemViewModel(temp, Products));
            //OrderItems.Add(temp);
        }

        [RelayCommand]
        public void RemoveOrderItem(object item)
        {
            OrderItemViewModels.Remove((ItemViewModel)item);
            //OrderItems.Remove((Item)item);
        }
        #endregion
    }
}
