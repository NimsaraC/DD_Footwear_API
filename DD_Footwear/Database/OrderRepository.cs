using DD_Footwear.Models;
using Microsoft.EntityFrameworkCore;

namespace DD_Footwear.Database
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DDShopDbContext _context;

        public OrderRepository(DDShopDbContext context)
        {
            _context = context;
        }

        public async Task<Order> GetByIdAsync(int orderId)
        {
            return await _context.Orders
                .Include(o => o.items)
                .FirstOrDefaultAsync(o => o.Id == orderId);
        }

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders
                .Include(o => o.items)
                .ToListAsync();
        }

        public async Task<List<Order>> GetOrdersByUserIdAsync(int userId)
        {
            return await _context.Orders
                .Include(o => o.items)
                .Where(o => o.UserID == userId)
                .ToListAsync();
        }

        public async Task<Order> AddAsync(Order order)
        {

            foreach (var item in order.items)
            {
                var stock = await _context.Products.FindAsync(item.ProductId);
                if (stock != null)
                {
                    if (stock.Unlock < item.Quantity)
                    {
                        order.OrderStatus = "PreOrderd";
                        stock.Lock += item.Quantity;
                    }
                    else
                    {
                        stock.Lock += item.Quantity;
                        stock.Unlock = (stock.StockLevel - stock.Lock);
                    }
                }
            }
            order.CreateTime = DateTime.Now;
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task UpdateOrderStatusAsync(int orderId, string status)
        {
            
            var order = await _context.Orders.FindAsync(orderId);
            if (order != null)
            {
                order.OrderStatus = status;
                if(status == "shipped")
                {
                    foreach (var item in order.items)
                    {
                        var stock = await _context.Products.FindAsync(item.ProductId);
                        if(stock != null)
                        {
                            stock.StockLevel -= item.Quantity;
                            stock.Lock -= item.Quantity;
                            _context.Products.Update(stock);
                        }
                    }
                }

                
                _context.Orders.Update(order);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Order> UpdateAsync(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
            return order;
        }
    }
    public interface IOrderRepository
    {
        Task<Order> GetByIdAsync(int orderId);
        Task<Order> AddAsync(Order order);
        Task<Order> UpdateAsync(Order order);
        Task<List<Order>> GetOrdersByUserIdAsync(int userId);
        Task<List<Order>> GetAllOrdersAsync();
        Task UpdateOrderStatusAsync(int orderId, string status);

    }


}
