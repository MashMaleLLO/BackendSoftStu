using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace SoftwareStudioBlog.Models
{
    public class Comment
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        public int BlogId { get; set; }

        [DataType(DataType.ImageUrl)]
        public string Img { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Text)]
        public string Message { get; set; } = string.Empty;

        public string IsHidden { get; set; } = "False";

        public DateTime CreatedDate { get; set; } = DateTime.Now;

    }
}
