using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SoftStudio_Blog.Data;
using System;
using System.Linq;

namespace SoftStudio_Blog.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new SoftStudio_BlogContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<SoftStudio_BlogContext>>()))
            {
                // Look for any movies.
                if (context.User.Any())
                {
                    return;   // DB has been seeded
                }
/*
                context.User.AddRange(
                    new User
                    {
                        Id = 1,
                        Username = "Male",
                        Password = "44332211",
                        WriterName = "Mashmallow",
                        Is_Admin = "True",
                        Is_Ban = "1"
                    },

                    new User
                    {
                        Id = 2,
                        Username = "fsaas",
                        Password = "625622",
                        WriterName = "fasfsadf",
                        Is_Admin = "True",
                        Is_Ban = "0"
                    },

                    new User
                    {
                        Id = 3,
                        Username = "ggggggg",
                        Password = "11111111",
                        WriterName = "gggesres",
                        Is_Admin = "True",
                        Is_Ban = "0"
                    },

                    new User
                    {
                        Id = 4,
                        Username = "qqqqqqq",
                        Password = "41345135",
                        WriterName = "rqrqrqrq",
                        Is_Admin = "True",
                        Is_Ban = "0"
                    }
                );*/
                context.SaveChanges();
            }
        }
    }
}