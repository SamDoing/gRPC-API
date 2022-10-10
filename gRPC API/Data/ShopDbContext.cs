using gRPC_API.Model;
using Microsoft.EntityFrameworkCore;

namespace gRPC_API.Data
{
    public class ShopDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public ShopDbContext(DbContextOptions<ShopDbContext> options) 
            : base(options) => Database.EnsureCreated();

    }
}
