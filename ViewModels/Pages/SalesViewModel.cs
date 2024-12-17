using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Sales_Manager.Models;
using Sales_Manager.Services;
using Sales_Manager.Services.Printing;
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
        private PrintService printService;
        #endregion

        #region collections
        [ObservableProperty] public List<Customer> customers;
        [ObservableProperty] public List<Product> products;
        //[ObservableProperty] public ObservableCollection<Item> orderItems;

        [ObservableProperty]
        public ObservableCollection<ItemViewModel> orderItemViewModels;
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
        public void NewOrder()
        {
            if (SelectedCustomer == null) { MessageBox.Show("Please select a customer first."); return; }

            CurrentOrder = new Order()
            {
                CustomerId = (int)SelectedCustomer,
            };
        }

        [RelayCommand]
        public async Task SaveOrder()
        {
            IsBusy = true;
            CurrentOrder.Total = Total;
            CurrentOrder.NetTotal = NetTotal;

            CurrentOrder = await orderService.Add(CurrentOrder);
            await orderService.SaveAsync();

            var itemsToSave = OrderItemViewModels.Select(vm => vm.ToItem()).ToList();
            itemsToSave.ForEach(a => a.OrderId = CurrentOrder.Id);
            itemsToSave.ForEach(a => a.Product = null);

            await itemService.AddRangeAsync(itemsToSave);
            await itemService.SaveAsync();
            IsBusy = false;
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
        }

        [RelayCommand]
        public void RemoveOrderItem(object item)
        {
            OrderItemViewModels.Remove((ItemViewModel)item);
            OnPropertyChanged(nameof(Total));
            OnPropertyChanged(nameof(NetTotal));
        }
        #endregion
    }
}
