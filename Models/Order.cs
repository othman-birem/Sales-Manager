using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Sales_Manager.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } = 0;

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerId { get; set; } = 0;

        [ForeignKey(nameof(CustomerId))]
        public Customer Customer { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Precision(10, 2)]
        public decimal Total { get; set; } = decimal.Zero;

        [Precision(10, 2)]
        public decimal NetTotal { get; set; } = decimal.Zero;
    }
}
