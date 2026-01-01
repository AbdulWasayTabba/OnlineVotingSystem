using practice.Data;
using practice.Repository.Interface;

namespace practice.Repository.Implementation
{
    public class ElectionRepository:IElectionRepository
    {
        private readonly ApplicationDbContext _context;
        public ElectionRepository(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}
