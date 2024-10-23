using AutoMapper;
using DD_Footwear.Database;
using DD_Footwear.DTOs;
using DD_Footwear.Models;

namespace DD_Footwear.Services
{
    public class StockService : IStockService
    {
        private readonly IStockRepository _stockRepository;
        private readonly IMapper _mapper;

        public StockService(IStockRepository stockRepository, IMapper mapper)
        {
            _stockRepository = stockRepository;
            _mapper = mapper;
        }

        public async Task<StockDto> GetStockByUserIdAsync(int userId)
        {
            var stock = await _stockRepository.GetByUserIdAsync(userId);
            return _mapper.Map<StockDto>(stock);
        }

        public async Task<StockDto> CreateStockAsync(StockDto createStockDto)
        {
            var stockEntity = _mapper.Map<Stock>(createStockDto);
            var createdStock = await _stockRepository.AddAsync(stockEntity);
            return _mapper.Map<StockDto>(createdStock);
        }

        public async Task AddStockItemAsync(int userId, AddStockItemDto addStock)
        {
            var stock = await _stockRepository.GetByUserIdAsync(userId);
            if (stock == null)
            {
                stock = new Stock { UserId = userId, StockItems = new List<StockItems>() };
                await _stockRepository.AddAsync(stock);
            }
            
            var stockItem = new StockItems
            {
                StockId = addStock.StockId,
                ProductId = addStock.ProductId,
                Stock = addStock.Stock,
                StockPrice = addStock.StockPrice,

            };
            await _stockRepository.AddStockItemsAsync(stockItem);

        }

        public async Task UpdateItemStockAsync(int itemId, int stock)
        {
            await _stockRepository.UpdateItemStockAsync(itemId, stock);
        }

        public async Task RemoveStockItemAsync(int itemId)
        {
            await _stockRepository.RemoveStockItemsAsync(itemId);
        }

        public async Task<StockDto> UpdateStockAsync(int stockId, StockDto stockDto)
        {
            var existingStock = await _stockRepository.GetByIdAsync(stockId);
            if (existingStock == null) return null;

            _mapper.Map(stockDto, existingStock);
            var updatedStock = await _stockRepository.UpdateAsync(existingStock);
            return _mapper.Map<StockDto>(updatedStock);
        }

        public async Task<bool> DeleteStockAsync(int stockId)
        {
            var stock = await _stockRepository.GetByIdAsync(stockId);
            if (stock == null) return false;

            await _stockRepository.DeleteAsync(stock);
            return true;
        }
    }

    public interface IStockService
    {
        Task<StockDto> GetStockByUserIdAsync(int userId);
        Task<StockDto> CreateStockAsync(StockDto createStockDto);
        Task<StockDto> UpdateStockAsync(int stockId, StockDto stockDto);
        Task<bool> DeleteStockAsync(int stockId);
        Task AddStockItemAsync(int userId, AddStockItemDto addStock);
        Task UpdateItemStockAsync(int itemId, int stock);
        Task RemoveStockItemAsync(int itemId);
    }


}
