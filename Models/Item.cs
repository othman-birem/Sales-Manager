
namespace Sales_Manager.Models
{
    internal class Item : ItemBase
    {
        public decimal Total { get { return Product.Price * Quantity; } }
        public int discount { get; set; } = 0;
        public decimal NetTotal { get { return Total - ((Total / 100) * discount); } }
    }
}
