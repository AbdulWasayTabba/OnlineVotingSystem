using Microsoft.EntityFrameworkCore;
using practice.Data;
using practice.Repository.Interface;

namespace practice.Repository.Implementation
{
    public class VoterRepository:IVoterRepository
    {
        private readonly ApplicationDbContext _context;
        public VoterRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> GetVoterEmailByIdAsync(int voterId)
        {
            // 1. Get the data (await unwraps the Task)
            var name = await _context.Users
                .Where(u => u.Id == voterId)
                .Select(u => u.Email)
                .FirstOrDefaultAsync();

            // 2. Return name OR empty string if null
            return name ?? string.Empty;
        }

        public async Task<string> GetVoterNameByIdAsync(int voterId)
        {
            // 1. Get the data (await unwraps the Task)
            var name = await _context.Users
                .Where(u => u.Id == voterId)
                .Select(u => u.FullName)
                .FirstOrDefaultAsync();

            // 2. Return name OR empty string if null
            return name ?? string.Empty;
        }
    }
}
