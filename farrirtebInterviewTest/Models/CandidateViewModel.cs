namespace farrirtebInterviewTest.Models
{
    public class CandidateViewModel
    {
        public string candidateId { get; set; }
        public string fullName { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string gender { get; set; }
        public string profilePicture { get; set; }
        public string email { get; set; }
        public string favoriteMusicGenre { get; set; }
        public string dad { get; set; }
        public string mom { get; set; }
        public bool canSwim { get; set; }
        public List<ExperienceViewModel> experience { get; set; }
        public CandidateStaus Status { get ; set; } = CandidateStaus.None;  
    }
}
