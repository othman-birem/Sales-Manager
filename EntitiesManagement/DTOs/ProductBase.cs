using Sales_Manager.Models.Common;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Sales_Manager.EntitiesManagement.DTOs
{
    [Table("Products")]
    public class ProductBase
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
        [EnumDataType(typeof(MetaData.UnitMetrics))]
        public MetaData.UnitMetrics UnitMetric { get; set; }
    }
}
