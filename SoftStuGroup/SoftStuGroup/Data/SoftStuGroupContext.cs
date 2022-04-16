#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SoftStuGroup.Models;

namespace SoftStuGroup.Data
{
    public class SoftStuGroupContext : DbContext
    {
        public SoftStuGroupContext (DbContextOptions<SoftStuGroupContext> options)
            : base(options)
        {
        }

        public DbSet<SoftStuGroup.Models.User> User { get; set; }

        public DbSet<SoftStuGroup.Models.Blog> Blog { get; set; }
    }
}
