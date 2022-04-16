using System.ComponentModel.DataAnnotations;


namespace SoftStuGroup.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(32, ErrorMessage = "Username must have length between 6 - 32", MinimumLength = 6)]
        public string Username { get; set; }

        [Required]
        [StringLength(32, ErrorMessage = "Username must have length between 6 - 32", MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        [StringLength(32, ErrorMessage = "Username must have length between 6 - 32", MinimumLength = 6)]
        public string ConfirmPassword { get; set; }

        public string Img { get; set; }
        public bool Role { get; set; }
    }
}
