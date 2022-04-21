using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SoftwareStudioBlog.Display;

namespace SoftwareStudioBlog.Display
{
    public class DisplayBlog
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Tag { get; set; }
        public string Img { get; set; }
        public string Detail { get; set; }
        public string IsHidden { get; set; }
        public DateTime Date { get; set; }
        public List<DisplayComment> Comments { get; set; } = new List<DisplayComment>();
        public int Likes { get; set; }
    }
}