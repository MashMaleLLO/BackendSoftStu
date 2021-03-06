using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftwareStudioBlog.Models
{
    public class Blog
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; } = string.Empty;

        public string Tag { get; set; } = string.Empty;

        [DataType(DataType.ImageUrl)]
        public string Image { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Text)]
        public string Detail { get; set; } = string.Empty;

        public string IsHidden { get; set; } = "False";

        public DateTime CreatedDate { get; set; } = DateTime.Now;

    }
}
