using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Sales_Manager.Models;
using Sales_Manager.Services;
using Sales_Manager.Services.Printing;
using System.Collections.ObjectModel;

namespace Sales_Manager.ViewModels.Pages
{
    public partial class OrdersViewModel : BaseViewModel
    {
        private readonly OrderService orderService;
        private PrintService printService;

        public ObservableCollection<Order> FilteredOrders { get; private set; } = new();

        [ObservableProperty] private string searchInput;

        [ObservableProperty] private int sortValue = -1;

        [ObservableProperty] private DateTime? fromDate;

        [ObservableProperty] private DateTime? toDate;

        [ObservableProperty] private bool groupByCustomer;

        private List<Order> orders;

        internal OrdersViewModel(OrderService service)
        {
            this.orderService = service;
            ResolveProperties();
        }

        public async Task ResolveProperties()
        {
            IsBusy = true;
            orders = await orderService.GetAsync();
            ApplyFiltersAndSorting();
            IsBusy = false;
        }

        private void ApplyFiltersAndSorting()
        {
            IEnumerable<Order> query = orders;

            if (!string.IsNullOrWhiteSpace(SearchInput))
            {
                query = query.Where(o =>
                    o.Id.ToString().Contains(SearchInput, StringComparison.OrdinalIgnoreCase) ||
                    (o.Customer?.Name?.Contains(SearchInput, StringComparison.OrdinalIgnoreCase) ?? false));
            }

            if (FromDate.HasValue)
            {
                query = query.Where(o => o.CreatedAt >= FromDate.Value);
            }

            if (ToDate.HasValue)
            {
                query = query.Where(o => o.CreatedAt <= ToDate.Value);
            }

            query = SortValue switch
            {
                -1 => query,
                0 => query.OrderBy(o => o.Customer.Name),
                1 => query.OrderByDescending(o => o.Customer.Name),
                2 => query.OrderByDescending(o => o.CreatedAt),
                3 => query.OrderBy(o => o.CreatedAt),
                _ => query
            };

            if (GroupByCustomer)
            {
                query = query.GroupBy(o => o.Customer?.Name)
                             .SelectMany(group => group);
            }

            FilteredOrders.Clear();
            foreach (var order in query)
            {
                FilteredOrders.Add(order);
            }
        }

        [RelayCommand]
        private void RefreshFilters()
        {
            ApplyFiltersAndSorting();
        }
        
        [RelayCommand]
        public void Print(object obj)
        {
            if (obj is Order order)
            {
                var orderItems = orderService.GetOrderItems(order.Id);
                printService = new PrintService(orderItems, order);
                printService.Print();
            }
        }

        [RelayCommand]
        public void Edit(object obj)
        {
            
        }

        [RelayCommand]
        public async Task Delete(object obj)
        {
            IsBusy = true;
            await orderService.Delete((Order)obj);
            await ResolveProperties();
            IsBusy = false;
        }
    }

}
