using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Sales_Manager.Models
{
    public class User
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
