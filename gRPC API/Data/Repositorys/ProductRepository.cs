using gRPC_API.Model;
using Microsoft.EntityFrameworkCore;

namespace gRPC_API.Data.Repositorys
{
    public class ProductRepository
    {
        readonly ShopDbContext _dbContext;
        public ProductRepository(ShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Product>> GetProductsAsync(int n = 10) =>
            await _dbContext.Products.Take(n).ToListAsync();

        public async Task<Product?> GetProductByIdAsync(int id) =>
            await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);

        public async Task<Product> CreateProductAsync(Product product)
        {
            _dbContext.Add(product);

            await _dbContext.SaveChangesAsync();

            return product;
        }

        public async Task<Product> UpdateProductAsync(Product product)
        {
            _dbContext.Update(product);

            await _dbContext.SaveChangesAsync();

            return product;
        }

        public async Task DeleteProductAsync(Product product)
        {
            _dbContext.Remove(product);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<Product?> GetProductByNameAsync(string name) => 
            await _dbContext.Products.FirstOrDefaultAsync(p => p.Name == name);

        internal async Task<List<Product>> GetProductsWhereNameAsync(int n, string? name) =>
            name != null ? await _dbContext.Products.Where(
                                        p => p.Name.Contains(name))
                                    .Take(n).ToListAsync()
                         : await _dbContext.Products.Take(n).ToListAsync();

        public async Task DeleteProductBydIdAsync(int id)
        {
            _dbContext.Remove(await GetProductByIdAsync(id));

            await _dbContext.SaveChangesAsync();
        }
    }
}
