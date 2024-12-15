using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sales_Manager.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } = 0;

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Precision(8, 2)]
        public decimal Price { get; set; } = decimal.Zero;

        [Required]
        public int StockQuantity { get; set; } = 0;

        [Required]
        [EnumDataType(typeof(UnitMetrics))]
        public UnitMetrics UnitMetric { get; set; }
    }
}
