using practice.Models;

namespace practice.DTOs
{
    public class DashboardStatsDto
    {
        public int TotalVoters { get; set; }
        public int TotalCandidates { get; set; }
        public int PendingVerifications { get; set; }
        public int PendingApprovals { get; set; }
        public int ActiveElections { get; set; }
        public int TotalVotesCast { get; set; }
        public  List<User>? RecentUsers { get; set; }
        public List<Candidate>? RecentCandidates { get; set; }
    }
}
