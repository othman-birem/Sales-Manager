using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Sales_Manager.Models
{
    public class Item : IOrderItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } = 0;

        [Required]
        public int OrderId { get; set; } = 0;

        [ForeignKey(nameof(OrderId))]
        public Order Order { get; set; }

        [Required]
        public int ProductId { get; set; } = 0;

        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }

        [Required]
        public uint Quantity { get; set; } = 1;
        [Required]
        public int discount { get; set; } = 0;
        internal decimal Total { get { return Product.Price * Quantity; } }
        internal decimal NetTotal { get { return Total - ((Total / 100) * discount); } }
    }
}
