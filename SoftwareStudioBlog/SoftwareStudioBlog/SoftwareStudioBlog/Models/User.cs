using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftwareStudioBlog.Models
{
    public class User
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [StringLength(32, ErrorMessage = "The username must have length between 4-32 characters"), MinLength(4)]
        public string? Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(32, ErrorMessage = "The password must have length between 6-32 characters"), MinLength(6)]
        [Display(Name = "Password")]
        public string? Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword")]
        [StringLength(32, ErrorMessage = "The password must have length between 6-32 characters"), MinLength(6)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string? ConfirmPassword { get; set; }

        // Unnecessary (up to frontend :D)
        [DataType(DataType.ImageUrl)]
        public string? Img { get; set; }

        [Required]
        public string? IsAdmin { get; set; }

        [Required]
        public string? IsBan { get; set; }


    }
}
