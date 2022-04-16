using System.ComponentModel.DataAnnotations;

namespace SoftStuGroup.Models
{
    public class Like
    {
        [Required]
        public int UserId { get; set; }
    }



    public class Comment
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public string Detail { get; set; }

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
