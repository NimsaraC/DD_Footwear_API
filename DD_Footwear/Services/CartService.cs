using AutoMapper;
using DD_Footwear.Database;
using DD_Footwear.DTOs;
using DD_Footwear.Models;
using static DD_Footwear.Services.CartService;

namespace DD_Footwear.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepo;
        private readonly IProductRepository _productRepo;
        private readonly IMapper _mapper;

        public CartService(ICartRepository cartRepo, IMapper mapper, IProductRepository productRepo)
        {
            _cartRepo = cartRepo;
            _mapper = mapper;
            _productRepo = productRepo;
        }

        public async Task<CartDto> GetCartByUserIdAsync(int userId)
        {
            var cart = await _cartRepo.GetCartByUserIdAsync(userId);
            return cart == null ? null : _mapper.Map<CartDto>(cart);
        }

        public async Task AddCartItemAsync(int userId, AddCartItemDto cartItemDto)
        {
            var cart = await _cartRepo.GetCartByUserIdAsync(userId);
            if (cart == null)
            {
                cart = new Cart { UserId = userId, Items = new List<CartItem>() };
                await _cartRepo.AddCartAsync(cart);
            }
            double Price = 0;
            var product = await _productRepo.GetByIdAsync(cartItemDto.ProductId);
            if (product != null)
            {
                Price = (double)product.Price;
            }

            var cartItem = new CartItem
            {
                CartId = cart.Id,
                ProductId = cartItemDto.ProductId,
                Quantity = cartItemDto.Quantity,
                UnitPrice = Price,
                ImagePath = cartItemDto.ImagePath,
            };

            await _cartRepo.AddCartItemAsync(cartItem);
        }

        public async Task RemoveCartItemAsync(int cartItemId)
        {
            await _cartRepo.RemoveCartItemAsync(cartItemId);
        }

        public async Task ClearCartAsync(int userId)
        {
            await _cartRepo.ClearCartAsync(userId);
        }
        public async Task UpdateCartItemQuantityAsync(int cartItemId, int quantity)
        {
            await _cartRepo.UpdateCartItemQuantityAsync(cartItemId, quantity);
        }
    }

        public interface ICartService
    {
        Task<CartDto> GetCartByUserIdAsync(int userId);
        Task AddCartItemAsync(int userId, AddCartItemDto cartItemDto);
        Task RemoveCartItemAsync(int cartItemId);
        Task ClearCartAsync(int userId);
        Task UpdateCartItemQuantityAsync(int cartItemId, int quantity);
    }
}
