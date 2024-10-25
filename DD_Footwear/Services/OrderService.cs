using AutoMapper;
using DD_Footwear.Database;
using DD_Footwear.DTOs;
using DD_Footwear.Models;

namespace DD_Footwear.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository,ICartRepository cartRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _cartRepository = cartRepository;
            _mapper = mapper;
        }

        public async Task<OrderDto> GetOrderByIdAsync(int orderId)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);
            return _mapper.Map<OrderDto>(order);
        }

        public async Task<OrderDto> CreateOrderAsync(CreateOrderDto createOrderDto)
        {
            var cartItems = await _cartRepository.GetCartItemsByUserIdAsync(createOrderDto.UserID);
            if (!cartItems.Any())
            {
                throw new InvalidOperationException("No items in cart.");
            }
            double Total = 0;
            foreach (var item in cartItems)
            {
                Total += (item.Quantity * item.UnitPrice);
            }

            var order = new Order
            {
                UserID = createOrderDto.UserID,
                CreateTime = DateTime.UtcNow,
                OrderStatus = "Pending",
                AddressLine1 = createOrderDto.AddressLine1,
                AddressLine2 = createOrderDto.AddressLine2,
                City = createOrderDto.City,
                Region = createOrderDto.Region,
                TotalAmount = Total,
                items = cartItems.Select(ci => new OrderItem
                {
                    ProductId = ci.ProductId,
                    Quantity = ci.Quantity,
                    UnitPrice = ci.UnitPrice,
                    ImagePath = ci.ImagePath,

                }).ToList()
            };

            await _cartRepository.ClearCartAsync(createOrderDto.UserID);
            var createdOrder = await _orderRepository.AddAsync(order);

            return _mapper.Map<OrderDto>(createdOrder);
        }

        public async Task UpdateOrderStatusAsync(int orderId, string newStatus)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order == null)
            {
                throw new Exception("Order not found.");
            }

            await _orderRepository.UpdateOrderStatusAsync(orderId, newStatus);
        }

        public async Task<List<OrderDto>> GetAllOrdersAsync()
        {
            var orders = await _orderRepository.GetAllOrdersAsync();
            return orders.Select(o => new OrderDto
            {
                Id = o.Id,
                AddressLine1 = o.AddressLine1,
                AddressLine2 = o.AddressLine2,
                City = o.City,
                CreateTime = o.CreateTime,
                OrderStatus = o.OrderStatus,
                Region = o.Region,
                TotalAmount = o.TotalAmount,
                UserID = o.UserID,
                items = o.items.Select(
                    i => new OrderItemDto
                    {
                        Id=i.Id,
                        OrderId = i.Id,
                        ProductId = i.ProductId,
                        Quantity = i.Quantity,
                         UnitPrice = i.UnitPrice,
                    }).ToList()
            }).ToList();
        }

        public async Task<List<OrderDto>> GetOrdersByUserIdAsync(int userId)
        {
            var order = await _orderRepository.GetOrdersByUserIdAsync(userId);
            return order.Select(o => new OrderDto
            {
                Id = o.Id,
                AddressLine1 = o.AddressLine1,
                AddressLine2 = o.AddressLine2,
                City = o.City,
                CreateTime = o.CreateTime,
                OrderStatus = o.OrderStatus,
                Region = o.Region,
                TotalAmount = o.TotalAmount,
                UserID = o.UserID,
                items = o.items.Select(
                    i => new OrderItemDto
                    {
                        Id = i.Id,
                        OrderId = i.Id,
                        ProductId = i.ProductId,
                        Quantity = i.Quantity,
                        UnitPrice = i.UnitPrice,
                    }).ToList()
            }).ToList();
        }
    }

    public interface IOrderService
    {
        Task<List<OrderDto>> GetAllOrdersAsync();
        Task<OrderDto> GetOrderByIdAsync(int orderId);
        Task<OrderDto> CreateOrderAsync(CreateOrderDto createOrderDto);
        Task<List<OrderDto>> GetOrdersByUserIdAsync(int userId);
        Task UpdateOrderStatusAsync(int orderId, string newStatus);
    }

}
