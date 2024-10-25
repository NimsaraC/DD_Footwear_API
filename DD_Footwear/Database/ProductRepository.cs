using DD_Footwear.DTOs;
using DD_Footwear.Models;
using Microsoft.EntityFrameworkCore;

namespace DD_Footwear.Database
{
    public class ProductRepository : IProductRepository
    {
        private readonly DDShopDbContext _context;

        public ProductRepository(DDShopDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int productId)
        {
            return await _context.Products.FindAsync(productId);
        }

        public async Task<Product> AddAsync(Product product)
        {
            product.Unlock = product.StockLevel;
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task UpdateAsync(int productId, ProductCreateDto product)
        {
            var existingProduct = await _context.Products.FindAsync(productId);
            if(existingProduct != null)
            {
                existingProduct.ProductId = productId;
                existingProduct.Name = product.Name;
                existingProduct.Price = product.Price;
                existingProduct.Description = product.Description;
                existingProduct.Categorie = product.Categorie;
                existingProduct.StockLevel = product.StockLevel;
                existingProduct.Lock = existingProduct.Lock;
                existingProduct.Unlock = existingProduct.Unlock;
            }
            _context.Products.Update(existingProduct);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Product product)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }

    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(int productId);
        Task<Product> AddAsync(Product product);
        Task UpdateAsync(int productId, ProductCreateDto product);
        Task DeleteAsync(Product product);
    }

}
