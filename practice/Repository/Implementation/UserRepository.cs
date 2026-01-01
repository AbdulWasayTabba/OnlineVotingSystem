using Microsoft.EntityFrameworkCore;
using practice.Data;
using practice.Models;
using practice.Repository.Interface;

namespace practice.Repository.Implementation
{
    public class UserRepository: IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> checkEmailPreExisting(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<User?> FindUser(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email && u.IsActive);
        }
    }
}
