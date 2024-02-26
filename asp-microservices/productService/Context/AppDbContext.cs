using Microsoft.EntityFrameworkCore;

namespace productService.Context
{
    public class AppDbContext: DbContext, IAppDbContext
    {
        
        public DbSet<Product> Products { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public async Task<int> SaveChanges()
        {
            return await base.SaveChangesAsync();
        }

    }
}