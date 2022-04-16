using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SoftStuGroup.Models
{
    [Keyless]
    public class Like
    {
        public int UserId { get; set; }
    }

    public class Comment
    { 
        [Key]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Detail { get; set; }

        [NotMapped]
        public List<Like> Like { get; set; }

    }

    public class Blog
    {   
        [Key]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string TagName { get; set; }

        [Required]
        public string Detail { get; set; }

        public List<Comment> Comments { get; set; }

        

    }
}
