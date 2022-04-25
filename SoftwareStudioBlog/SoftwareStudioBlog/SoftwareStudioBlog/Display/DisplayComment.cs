using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftwareStudioBlog.Display
{
    public class DisplayComment
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public int BlogId { get; set; }
        public string Message { get; set; }
        public string IsHidden { get; set; }
        public DateTime Date { get; set; }
        public string Img { get; set; }
        public int Likes { get; set; }
    }
}