using practice.DTOs;
using practice.Models;

namespace practice.Services
{
    public interface ICandidateService
    {
        Task<CandidateDashboardDto?> GetDashboardAsync(int userId);

        // Edit Profile: Gets data for the form
        Task<CandidateDto?> GetProfileForEditAsync(int userId);

        // Edit Profile: Saves the form
        Task<bool> UpdateProfileAsync(int userId, CandidateDto dto);

        // Results: Returns Election, Results List, and MyVotes count
        Task<(Election? Election, List<VoteResultDto> Results, int MyVotes)> GetResultsAsync(int userId, int? electionId);
        Task<bool> ParticipateInElectionAsync(int candidateId, int electionId);
        Task<int> GetCandidateByUserIdServiceAsync(int userId);
    }
}
