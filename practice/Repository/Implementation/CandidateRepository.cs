using practice.Data;
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
    }
}
