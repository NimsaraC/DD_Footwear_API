using DD_Footwear.DTOs;
using DD_Footwear.Services;
using Microsoft.AspNetCore.Mvc;

namespace DD_Footwear.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StockController : ControllerBase
    {
        private readonly IStockService _stockService;

        public StockController(IStockService stockService)
        {
            _stockService = stockService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetStockByUserId(int userId)
        {
            var stock = await _stockService.GetStockByUserIdAsync(userId);
            if (stock == null) return NotFound();
            return Ok(stock);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStock([FromBody] StockDto createStockDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var createdStock = await _stockService.CreateStockAsync(createStockDto);
            return CreatedAtAction(nameof(GetStockByUserId), new { userId = createdStock.UserId }, createdStock);
        }

        [HttpPut("items/{ItemId}/stock")]
        public async Task<IActionResult> UpdateItemQuantity(int userId, [FromBody] int stock)
        {
            if (stock <= 0)
            {
                return BadRequest("Stock must be greater than 0,");
            }
            await _stockService.UpdateItemStockAsync(userId, stock);
            return Ok();
        }

        [HttpPost("{userId}/items")]
        public async Task<IActionResult> AddStockItems(int userId, [FromBody] AddStockItemDto addStock)
        {
            await _stockService.AddStockItemAsync(userId, addStock);
            return Ok("Item added to Stock.");
        }

        [HttpDelete("items/{itemId}")]
        public async Task<IActionResult> DeleteItem(int itemId)
        {
            await _stockService.RemoveStockItemAsync(itemId);
            return Ok();

        }

    }
}

