using System.ComponentModel.DataAnnotations;

namespace World_Api.Models
{
    public class LoginRegistration
    {
        [Key]
        public int userId { get; set; }

        [Required]
        [MaxLength(50)]
        public string userName { get; set; }

        [Required]
        [MaxLength(50)]
        public string Password { get; set; }

        [Required]
        [MaxLength(50)]
        public string Email { get; set; }

        [Required]
        [MaxLength(20)]
        public string Phone { get; set; }

        [Required]
        [MaxLength(10)]
        public bool ActiveSts { get; set; }
    }
}
