using DD_Footwear.DTOs;
using DD_Footwear.Models;
using Microsoft.EntityFrameworkCore;

namespace DD_Footwear.Database
{
    public class StockRepository : IStockRepository
    {
        private readonly DDShopDbContext _context;

        public StockRepository(DDShopDbContext context)
        {
            _context = context;
        }

        public async Task<Stock> GetByIdAsync(int stockId)
        {
            return await _context.Stocks.Include(s => s.StockItems)
                .FirstOrDefaultAsync(s => s.Id == stockId);
        }

        public async Task<Stock> GetByUserIdAsync(int userId)
        {
            return await _context.Stocks.Include(s => s.StockItems)
                .FirstOrDefaultAsync(s => s.UserId == userId);
        }

        public async Task<Stock> AddAsync(Stock stock)
        {
            await _context.Stocks.AddAsync(stock);
            await _context.SaveChangesAsync();
            return stock;
        }

        public async Task AddStockItemsAsync(StockItems stockItem)
        {
            await _context.StockItems.AddAsync(stockItem);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveStockItemsAsync(int itemId)
        {
            var item = await _context.StockItems.FindAsync(itemId);
            if(item != null)
            {
                _context.StockItems.Remove(item);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Stock> UpdateAsync(Stock stock)
        {
            _context.Stocks.Update(stock);
            await _context.SaveChangesAsync();
            return stock;
        }

        public async Task UpdateItemStockAsync(int ItemId, int Stock)
        {
            var Item = await _context.StockItems.FindAsync(ItemId);
            if(Item != null)
            {
                Item.Stock = Stock;
                _context.StockItems.Update(Item);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(Stock stock)
        {
            _context.Stocks.Remove(stock);
            await _context.SaveChangesAsync();
        }
    }

    public interface IStockRepository
    {
        Task<Stock> GetByIdAsync(int stockId);
        Task<Stock> GetByUserIdAsync(int userId);
        Task<Stock> AddAsync(Stock stock);
        Task<Stock> UpdateAsync(Stock stock);
        Task DeleteAsync(Stock stock);
        Task AddStockItemsAsync(StockItems stockItem);
        Task RemoveStockItemsAsync(int itemId);
        Task UpdateItemStockAsync(int ItemId, int Stock);
    }


}
