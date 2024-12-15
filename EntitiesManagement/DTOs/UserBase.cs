using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sales_Manager.EntitiesManagement.DTOs
{
    [Table("Users")]
    public class UserBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } = 0;

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        [PasswordPropertyText]
        [MinLength(6, ErrorMessage = "The password should be at least 6 characters long.")]
        public string Password { get; set; } = string.Empty;
    }
}
