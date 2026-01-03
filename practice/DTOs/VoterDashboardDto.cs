using practice.Models;

namespace practice.DTOs
{
    public class VoterDashboardDto
    {
        public User? User { get; set; }
        public List<Election>? ActiveElections { get; set; }
        public List<int>? VotedElectionIds { get; set; }
    }
}
