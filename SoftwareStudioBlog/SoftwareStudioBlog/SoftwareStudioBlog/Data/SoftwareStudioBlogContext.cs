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

        public DbSet<SoftwareStudioBlog.Models.User> User { get; set; }

        public DbSet<SoftwareStudioBlog.Models.Blog> Blog { get; set; }
    }
}
