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
    }
}
