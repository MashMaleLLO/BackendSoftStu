#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SoftwareStudioBlog.Models;

namespace SoftwareStudioBlog.Data
{
    public class SoftwareStudioBlogContext : DbContext
    {
        public SoftwareStudioBlogContext (DbContextOptions<SoftwareStudioBlogContext> options)
            : base(options)
        {
        }

        public DbSet<User> User { get; set; }

        public DbSet<Blog> Blog { get; set; }

        public DbSet<SoftwareStudioBlog.Models.LikeBlog> LikeBlog { get; set; }

        public DbSet<SoftwareStudioBlog.Models.LikeComment> LikeComment { get; set; }

        public DbSet<SoftwareStudioBlog.Models.Comment> Comment { get; set; }

    }
}
