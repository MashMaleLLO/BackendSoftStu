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

        public string GetLike()
        {
            return this.Like.ToString();
        }

        public Like AddLike(Like l)
        {
            this.Like.Add(l);
            return l;
        }

        public string PopLike(Like l)
        {
            foreach (var item in this.Like)
            {
                if (l.UserId == item.UserId)
                {
                    Like temp = item;
                    temp.UserId = item.UserId;
                    this.Like.Remove(item);
                    return temp.ToString();
                }
            }
            return "Not in list";
        }

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

        public string GetComment()
        {
            return this.Comments.ToString();
        }

        public Comment AddComment(Comment c)
        {
            this.Comments.Add(c);
            return c;
        }

        public string PopCommnent(Comment c)
        {
            foreach(var comment in this.Comments)
            {
                if(comment.Id == c.Id)
                {
                    Comment temp = new Comment();
                    temp.Id = comment.Id;
                    temp.Username = comment.Username;
                    temp.Detail = comment.Detail;
                    temp.Like = comment.Like;
                    this.Comments.Remove(comment);
                    return temp.ToString();
                }
            }
            return "Cant Find that comment.";
        }

    }
}
