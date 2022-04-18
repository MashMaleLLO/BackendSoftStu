using System.ComponentModel.DataAnnotations;

namespace SoftStudio_Blog.Models
{
    public class User
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string? Username { get; set; }

        [Required]
        public string? Password { get; set; }

        [Required]
        public string? WriterName { get; set; }

        [Required]
        public string? Is_Admin { get; set; }
 
        [Required]
        public string? Is_Ban { get; set; }
    }
}
