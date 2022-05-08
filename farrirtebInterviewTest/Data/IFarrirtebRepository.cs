using farrirtebInterviewTest.Models;

namespace farrirtebInterviewTest.Data
{
    public interface IFarrirtebRepository
    {
        IEnumerable<CandidateViewModel> GetCandidates(bool reset);
        IEnumerable<CandidateViewModel> GetApprovedCandidates(bool reset);
        IEnumerable<TechnologyViewModel> GetTechnologies();
        void UpdateCandidate(CandidateViewModel candidate);
        CandidateViewModel GetById(string candidateId);
        CandidateViewModel GetCandidateDetails(string id);
    }
}
