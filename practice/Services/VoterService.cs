using practice.DTOs;
using practice.Models;
using practice.Repository.Interface;

namespace practice.Services
{
    public class VoterService : IVoterService
    {
        private readonly IUserRepository _userRepo;
        private readonly IElectionRepository _electionRepo;
        private readonly ICandidateRepository _candidateRepo;

        public VoterService(IUserRepository userRepo, IElectionRepository electionRepo, ICandidateRepository candidateRepo)
        {
            _userRepo = userRepo;
            _electionRepo = electionRepo;
            _candidateRepo = candidateRepo;
        }

        public async Task<VoterDashboardDto?> GetDashboardAsync(int userId)
        {
            var user = await _userRepo.FindUserViaId(userId);
            if (user == null) return null;

            var activeElections = await _electionRepo.GetOngoingElectionsAsync();
            var votedIds = await _electionRepo.GetVotedElectionIdsAsync(userId);

            return new VoterDashboardDto
            {
                User = user,
                ActiveElections = activeElections,
                VotedElectionIds = votedIds
            };
        }

        public async Task<(Election? Election, List<CandidateDto> Candidates)> GetVotingPageAsync(int userId, int electionId)
        {
            // 1. Validate Election
            var election = await _electionRepo.GetElectionByIdAsync(electionId);
            var now = DateTime.Now;

            if (election == null || !election.IsActive || now < election.StartDate || now > election.EndDate)
                return (null, new List<CandidateDto>());

            // 2. Validate Double Voting
            if (await _electionRepo.HasUserVotedAsync(electionId, userId))
                return (null, new List<CandidateDto>()); // Or handle differently if you want specific error

            // 3. Get Candidates
            var candidates = await _candidateRepo.GetApprovedCandidatesAsync();

            var candidateDtos = candidates.Select(c => new CandidateDto
            {
                Id = c.Id,
                FullName = c.User.FullName,
                PartyName = c.PartyName,
                PartySymbol = c.PartySymbol,
                Manifesto = c.Manifesto,
                Biography = c.Biography,
                ProfileImageUrl = c.ProfileImageUrl
            }).ToList();

            return (election, candidateDtos);
        }

        public async Task<string> CastVoteAsync(int userId, VoteDto voteDto)
        {
            // 1. Check Election Validity
            var election = await _electionRepo.GetElectionByIdAsync(voteDto.ElectionId);
            var now = DateTime.Now;
            if (election == null || !election.IsActive || now < election.StartDate || now > election.EndDate)
                return "Invalid election or voting period.";

            // 2. Check Double Vote
            if (await _electionRepo.HasUserVotedAsync(voteDto.ElectionId, userId))
                return "You have already voted.";

            // 3. Check Candidate Validity
            var candidate = await _candidateRepo.GetCandidateByIdAsync(voteDto.CandidateId);
            if (candidate == null || !candidate.IsApproved)
                return "Invalid candidate.";

            // 4. Create Vote Object
            var vote = new Vote
            {
                ElectionId = voteDto.ElectionId,
                VoterId = userId,
                CandidateId = voteDto.CandidateId,
                VotedAt = DateTime.Now,
                IsVerified = true
            };

            // 5. Save (Transactional Save via Repo)
            var success = await _electionRepo.CastVoteAsync(vote, candidate);

            return success ? "Success" : "Failed to record vote.";
        }
    }
}