using Microsoft.EntityFrameworkCore;
using practice.Data;
using practice.Models;
using practice.Repository.Interface;

namespace practice.Repository.Implementation
{
    public class CandidateRepository:ICandidateRepository
    {
        private readonly ApplicationDbContext _context;
        public CandidateRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> RegisterCandidateAsync(User user, Candidate candidate)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // 1. Save the User first (to generate the Id)
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();

                // 2. IMPORTANT: Link the new User Id to the Candidate
                candidate.UserId = user.Id;

                // 3. Save the Candidate
                await _context.Candidates.AddAsync(candidate);
                await _context.SaveChangesAsync();

                // 4. Commit
                await transaction.CommitAsync();
                return true;
            }
            catch (Exception)
            {
                // 5. Rollback on any error
                await transaction.RollbackAsync();
                return false;
            }
        }

        //Admin Methods

        // Implement the methods similar to UserRepository, using _context.Candidates
        public async Task<int> GetTotalCandidatesCountAsync() => await _context.Candidates.CountAsync();

        public async Task<int> GetPendingApprovalsCountAsync() => await _context.Candidates.CountAsync(c => !c.IsApproved);

        public async Task<List<Candidate>> GetRecentPendingCandidatesAsync()
        {
            return await _context.Candidates.Include(c => c.User)
                .Where(c => !c.IsApproved).OrderByDescending(c => c.RegisteredAt).Take(5).ToListAsync();
        }

        public async Task<List<Candidate>> GetAllCandidatesWithUsersAsync()
        {
            return await _context.Candidates.Include(c => c.User).OrderByDescending(c => c.RegisteredAt).ToListAsync();
        }

        public async Task<Candidate?> GetCandidateByIdAsync(int id)
        {
            return await _context.Candidates.Include(c => c.User).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<bool> UpdateCandidateAsync(Candidate candidate)
        {
            _context.Candidates.Update(candidate);
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<Candidate?> GetCandidateByUserIdAsync(int userId)
        {
            return await _context.Candidates
                .Include(c => c.User)
                .Include(c => c.Votes) // Important: Dashboard needs to count votes
                .FirstOrDefaultAsync(c => c.UserId == userId);
        }
        public async Task<List<Candidate>> GetApprovedCandidatesAsync()
        {
            return await _context.Candidates
                .Include(c => c.User)
                .Where(c => c.IsApproved)
                .ToListAsync();
        }
    }
}
