using practice.Models;

namespace practice.DTOs
{
    public class CandidateDashboardDto
    {
        public User User { get; set; }
        public Candidate Candidate { get; set; }
        public List<Election>? ActiveElections { get; set; }
        public List<dynamic>? VotesByElection { get; set; }
    }
}
