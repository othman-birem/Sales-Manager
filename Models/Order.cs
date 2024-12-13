
namespace Sales_Manager.Models
{
    public class Order : OrderBase
    {
        public decimal Total { get; set; } = decimal.Zero;

        public decimal NetTotal { get; set; } = decimal.Zero;
    }
}
