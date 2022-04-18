#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SoftStudio_Blog.Models;

namespace SoftStudio_Blog.Data
{
    public class SoftStudio_BlogContext : DbContext
    {
        public SoftStudio_BlogContext (DbContextOptions<SoftStudio_BlogContext> options)
            : base(options)
        {
        }

        public DbSet<SoftStudio_Blog.Models.User> User { get; set; }
    }
}
