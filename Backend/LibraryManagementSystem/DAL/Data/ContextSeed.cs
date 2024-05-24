using BCrypt.Net;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Data
{
    public static class ContextSeed
    {
        public static async Task SeedData(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();

                var user1 = await dbContext.Users.FirstOrDefaultAsync(u => u.Username == "user1");
                var user2 = await dbContext.Users.FirstOrDefaultAsync(u => u.Username == "user2");
                var user3 = await dbContext.Users.FirstOrDefaultAsync(u => u.Username == "user3");
                var user4 = await dbContext.Users.FirstOrDefaultAsync(u => u.Username == "user4");
                if (user1 == null)
                {
                    var regularUser1 = new User
                    {
                        Name = "User1",
                        Username = "user1",
                        TokensAvailable = 1,
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword("User123#"),
                        
                    };

                    await dbContext.Users.AddAsync(regularUser1);
                }

                if (user2 == null)
                {
                    var regularUser2 = new User
                    {
                        Name = "User2",
                        Username = "user2",
                        TokensAvailable = 1,
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword("User123#"),
                    };

                    await dbContext.Users.AddAsync(regularUser2);
                }

                if (user3 == null)
                {
                    var regularUser3 = new User
                    {
                        Name = "User3",
                        Username = "user3",
                        TokensAvailable = 1,
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword("User123#"),
                    };

                    await dbContext.Users.AddAsync(regularUser3);
                }

                if (user4 == null)
                {
                    var regularUser4 = new User
                    {
                        Name = "User4",
                        Username = "user4",
                        TokensAvailable = 1,
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword("User123#"),
                    };

                    await dbContext.Users.AddAsync(regularUser4);
                }
                    await dbContext.SaveChangesAsync();
                }
            }
        }
    }

