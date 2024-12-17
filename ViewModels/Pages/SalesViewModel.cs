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
        #region services
        private OrderService orderService;
        private ProductService productService;
        private CustomerService customerService;
        private ItemService itemService;
        #endregion

        #region collections
        [ObservableProperty] public List<Customer> customers;
        [ObservableProperty] public List<Product> products;
        //[ObservableProperty] public ObservableCollection<Item> orderItems;

        [ObservableProperty] public ObservableCollection<ItemViewModel> orderItemViewModels;
        #endregion


        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(HasOrder))]
        public Order currentOrder;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(NetTotal))]
        public double totalDiscountPercentage = 0;


        [ObservableProperty] public int? selectedCustomer;

        public decimal Total => OrderItemViewModels.Sum(item => item.NetTotal);
        public decimal NetTotal
        {
            get
            {
                decimal tot = OrderItemViewModels.Sum(item => item.NetTotal);
                return tot - (tot / (decimal)100.0 * (decimal)TotalDiscountPercentage);
            }
        }
        //TotalPrice - ((TotalPrice / 100) * item.discount
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

        private void ItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(ItemViewModel.TotalPrice):
                    OnPropertyChanged(nameof(Total));
                    OnPropertyChanged(nameof(NetTotal));
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

            var ivm = new ItemViewModel(temp, Products);
            ivm.PropertyChanged += ItemPropertyChanged;

            OrderItemViewModels.Add(ivm);
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
