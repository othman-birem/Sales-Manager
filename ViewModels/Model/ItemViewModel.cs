using Sales_Manager.Models;

namespace Sales_Manager.ViewModels.Model
{
    public class ItemViewModel : BaseViewModel
    {
        public Item item { get; private set; }
        public readonly List<Product> _products;

        public ItemViewModel(Item item, List<Product> products)
        {
            this.item = item;
            _products = products;
        }

        public int? ProductId
        {
            get => item.ProductId;
            set
            {
                if (item.ProductId != value)
                {
                    item.ProductId = (int)value;
                    item.Product = _products.FirstOrDefault(p => p.Id == (int)value);

                    OnPropertyChanged(nameof(ProductId));
                    OnPropertyChanged(nameof(SelectedProduct));
                    OnPropertyChanged(nameof(UnitPrice));
                    OnPropertyChanged(nameof(TotalPrice));
                    OnPropertyChanged(nameof(NetTotal));
                }
            }
        }

        public Product SelectedProduct => _products.FirstOrDefault(p => p.Id == ProductId);

        public uint Quantity
        {
            get => item.Quantity;
            set
            {
                if (item.Quantity != value)
                {
                    item.Quantity = value;
                    OnPropertyChanged(nameof(Quantity));
                    OnPropertyChanged(nameof(TotalPrice));
                    OnPropertyChanged(nameof(NetTotal));
                }
            }
        }
        public int Discount
        {
            get => item.discount;
            set
            {
                if (item.discount != value)
                {
                    item.discount = value;
                    OnPropertyChanged(nameof(TotalPrice));
                    OnPropertyChanged(nameof(NetTotal));
                }
            }
        }

        public decimal UnitPrice
        {
            get
            {
                return SelectedProduct?.Price ?? 0;
            }
            set
            {
                if (SelectedProduct?.Price != value)
                {
                    SelectedProduct.Price = value;
                    OnPropertyChanged(nameof(TotalPrice));
                    OnPropertyChanged(nameof(NetTotal));
                }
            }
        }
        public decimal TotalPrice => Quantity * UnitPrice;
        public decimal NetTotal => TotalPrice - ((TotalPrice / 100) * item.discount);

        public Item ToItem() => item;
    }
}
