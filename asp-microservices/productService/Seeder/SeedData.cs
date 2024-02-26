using Microsoft.EntityFrameworkCore; 
using productService.Context;

namespace productService.Seeder
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new AppDbContext(serviceProvider.GetRequiredService<
                    DbContextOptions<AppDbContext>>()))
            {
               /* if (context == null || context.Users == null) {
                    throw new ArgumentNullException("Null AppDbContext");
                }

                // Look for any movies.
                if (context.Users.Any())
                {
                    return;   // DB has been seeded
                }
                context.Users.AddRange( 
                    new User
                    {
                        Username = "pierre",
                        Password = "test",
                        Email = "p.pierre@localhost.com",
                        CreatedAt = DateTime.Now
                    },
                    new User
                    {
                        Username = "paul",
                        Password = "test",
                        Email = "p.paul@localhost.com",
                        CreatedAt = DateTime.Now
                    }
                );

                context.SaveChanges();*/
            }
        }

    }
}