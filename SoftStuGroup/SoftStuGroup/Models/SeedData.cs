using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SoftStuGroup.Data;
using System;
using System.Linq;


namespace SoftStuGroup.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new SoftStuGroupContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<SoftStuGroupContext>>()))
            {
                if (context.User.Any())
                {
                    return;
                }

                context.User.AddRange(
                    new User
                    {
                        Username = "Mash",
                        Password = "123456",
                        ConfirmPassword = "123456",
                        Img = "Hi1",
                        Role = false
                    },

                    new User
                    {
                        Username = "Male",
                        Password = "123456",
                        ConfirmPassword = "123456",
                        Img = "Hi2",
                        Role = false
                    },

                    new User
                    {
                        Username = "LLO",
                        Password = "123456",
                        ConfirmPassword = "123456",
                        Img = "Hi3",
                        Role = false
                    }
                    );
                context.SaveChanges();
            }
        }
    }
}
