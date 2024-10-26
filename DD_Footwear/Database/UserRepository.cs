using DD_Footwear.Models;
using Microsoft.EntityFrameworkCore;

namespace DD_Footwear.Database
{
    public class UserRepository : IUserRepo
    {
        private readonly DDShopDbContext _context;

        public UserRepository(DDShopDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> LoginAsync(string email, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(e => e.Email == email && e.Password == password);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }

    public interface IUserRepo
    {
        Task<User> GetUserByIdAsync(int id);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task<User> LoginAsync(string email, string password);
    }
}
