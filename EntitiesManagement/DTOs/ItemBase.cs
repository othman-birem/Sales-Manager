using Sales_Manager.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Sales_Manager.EntitiesManagement.DTOs
{
    [Table("Items")]
    public class ItemBase : IOrderItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } = 0;

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; } = 0;

        [ForeignKey(nameof(OrderId))]
        public OrderBase Order { get; set; }

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; } = 0;

        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }

        [Required]
        public uint Quantity { get; set; } = 0;
    }
}
