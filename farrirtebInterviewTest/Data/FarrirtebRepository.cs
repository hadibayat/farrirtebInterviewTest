using farrirtebInterviewTest.Models;
using System.Net.Http.Headers;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace farrirtebInterviewTest.Data
{
    public class FarrirtebRepository : IFarrirtebRepository
    {
        public static IEnumerable<CandidateViewModel> Candidates;
        public static IEnumerable<TechnologyViewModel> Technologies;

        public FarrirtebRepository()
        {

        }

        async Task<IEnumerable<CandidateViewModel>> GetCandidatesAsync(string path)
        {
            HttpClient client = new HttpClient();

            IEnumerable<CandidateViewModel> candidates = new List<CandidateViewModel>();

            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                candidates = await response.Content.ReadAsAsync<IEnumerable<CandidateViewModel>>();
            }

            client.Dispose();

            return candidates;
        }

        async Task<IEnumerable<TechnologyViewModel>> GetTechnologiesAsync(string path)
        {
            HttpClient client = new HttpClient();

            IEnumerable<TechnologyViewModel> candidates = new List<TechnologyViewModel>();

            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                candidates = await response.Content.ReadAsAsync<IEnumerable<TechnologyViewModel>>();
            }

            client.Dispose();

            return candidates;
        }

        public IEnumerable<CandidateViewModel> GetCandidates(bool reset)
        {
            if (reset)
                Candidates = GetCandidatesAsync("https://app.ifs.aero/EternalBlue/api/candidates").Result;

            return Candidates.Where(x => x.Status == CandidateStaus.None);
        }

        public IEnumerable<TechnologyViewModel> GetTechnologies()
        {
            if (Technologies == null)
                Technologies = GetTechnologiesAsync("https://app.ifs.aero/EternalBlue/api/technologies").Result;

            return Technologies;
        }

        public void UpdateCandidate(CandidateViewModel candidate)
        {
            var _candidate = Candidates.First(x => x.candidateId == candidate.candidateId);
            _candidate = candidate;
        }

        public CandidateViewModel GetById(string candidateId)
        {
            return Candidates.First(x => x.candidateId == candidateId);
        }

        public IEnumerable<CandidateViewModel> GetApprovedCandidates(bool reset)
        {
            if (reset)
                Candidates = GetCandidatesAsync("https://app.ifs.aero/EternalBlue/api/candidates").Result;

            return Candidates.Where(x => x.Status == CandidateStaus.Accepted);
        }

        public CandidateViewModel GetCandidateDetails(string id)
        {
            var candidate = GetById(id);
            var technologies = GetTechnologies();

            if (technologies == null)
                return candidate;

            var experiences = candidate.experience;

            var candidaExperiences = from experience in experiences
                                     join technology in technologies
                                       on experience.technologyId?.ToLower() equals technology.guid.ToLower()
                                     select new ExperienceViewModel { technologyName = technology.name, technologyId = experience.technologyId, yearsOfExperience = experience.yearsOfExperience };

            candidate.experience = candidaExperiences.ToList();

            return candidate;
        }
    }
}
