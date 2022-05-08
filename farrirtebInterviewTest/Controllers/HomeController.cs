using farrirtebInterviewTest.Data;
using farrirtebInterviewTest.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace farrirtebInterviewTest.Controllers
{
    public class HomeController : Controller
    {
        public static bool reset = true;
        private readonly ILogger<HomeController> _logger;
        private readonly IFarrirtebRepository _farrirtebRepository;

        public HomeController(ILogger<HomeController> logger, IFarrirtebRepository farrirtebRepository)
        {
            _logger = logger;
            _farrirtebRepository = farrirtebRepository;
        }

        public IActionResult Index()
        {
            var candidates = _farrirtebRepository.GetCandidates(reset);
            // var technologies = _farrirtebRepository.GetTechnologies(reset);


            //var result = from candidate in candidates
            //             join technology in technologies
            //               on candidate. equals technology.Id
            //             select new { candidate.Id, Value1 = candidate.Value, Value2 = technology.Value };

            //var candidateWithExperiences = from candidate to 

            reset = false;

            return View(candidates);
        }

        public IActionResult Reset()
        {
            reset = true;
            return RedirectToAction("Index");
        }

        public IActionResult Details(string id)
        {
            var candidate = _farrirtebRepository.GetCandidateDetails(id);

            return View("Details", candidate);
        }

        public IActionResult Approved()
        {
            var candidate = _farrirtebRepository.GetApprovedCandidates(false);

            return View("Approved", candidate);
        }


        [HttpPost]
        public IActionResult Reject(CandidateViewModel candidate)
        {
            var _candidate = _farrirtebRepository.GetById(candidate.candidateId);
            _candidate.Status = CandidateStaus.Rejected;

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Accept(CandidateViewModel candidate)
        {
            var _candidate = _farrirtebRepository.GetById(candidate.candidateId);
            _candidate.Status = CandidateStaus.Accepted;

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}