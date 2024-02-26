using Microsoft.EntityFrameworkCore;

namespace productService.Context
{
    public interface IAppDbContext
    {
        public DbSet<Product> Products { get; set; }
   
        public Task<int> SaveChanges();

    }
}