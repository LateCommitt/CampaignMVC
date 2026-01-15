using System.Diagnostics;
using Campaign.Models;
using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Campaign.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICampaignService _campaignService;
        private readonly ICampaignAssignmentService _campaignAssignmentService;
        private readonly IUserService _userService;

        public HomeController(
            ILogger<HomeController> logger,
            ICampaignService campaignService,
            ICampaignAssignmentService campaignAssignmentService,
            IUserService userService)
        {
            _logger = logger;
            _campaignService = campaignService;
            _campaignAssignmentService = campaignAssignmentService;
            _userService = userService;
        }

        public IActionResult Index()
        {
            var activeCampaigns = _campaignService.GetList().Where(c => c.IsActive).Count();
            var totalAssignments = _campaignAssignmentService.GetAllList().Count;
            var usedCampaigns = _campaignAssignmentService.GetAllList()
                .Count(a => a.Status == EntityLayer.Enums.CampaignStatus.USED);
            var totalUsers = _userService.GetList().Count;

            ViewBag.ActiveCampaigns = activeCampaigns;
            ViewBag.TotalAssignments = totalAssignments;
            ViewBag.UsedCampaigns = usedCampaigns;
            ViewBag.TotalUsers = totalUsers;
            ViewBag.UsageRate = totalAssignments > 0 ? (double)usedCampaigns / totalAssignments * 100 : 0;

            // Günlük kampanya dağılımı
            var dailyAssignments = _campaignAssignmentService.GetAllList()
                .Where(a => a.AssignedAt.Date == DateTime.Today)
                .GroupBy(a => a.CampaignId)
                .Select(g => new DailyAssignmentInfo { CampaignId = g.Key, Count = g.Count() })
                .ToList();

            ViewBag.DailyAssignments = dailyAssignments;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
