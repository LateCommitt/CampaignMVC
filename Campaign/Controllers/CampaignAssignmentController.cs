using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using EntityLayer.Enums;
using Microsoft.AspNetCore.Mvc;
using Campaign.Models;
using System.Linq;

namespace Campaign.Controllers
{
    public class CampaignAssignmentController : Controller
    {
        private readonly ICampaignAssignmentService _campaignAssignmentService;
        private readonly IUserService _userService;
        private readonly ICampaignService _campaignService;

        public CampaignAssignmentController(
            ICampaignAssignmentService campaignAssignmentService,
            IUserService userService,
            ICampaignService campaignService)
        {
            _campaignAssignmentService = campaignAssignmentService;
            _userService = userService;
            _campaignService = campaignService;
        }

        public IActionResult Index()
        {
            var assignments = _campaignAssignmentService.GetAllList();
            return View(assignments);
        }

        [HttpPost]
        public IActionResult MarkAsUsed(int id)
        {
            _campaignAssignmentService.MarkAsUsed(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Statistics()
        {
            var assignments = _campaignAssignmentService.GetAllList();
            
            var statusCounts = assignments
                .GroupBy(a => a.Status)
                .Select(g => new StatusCountInfo { Status = g.Key, Count = g.Count() })
                .ToList();

            var campaignUsage = assignments
                .GroupBy(a => a.CampaignId)
                .Select(g => new CampaignUsageInfo
                { 
                    CampaignId = g.Key,
                    Campaign = _campaignService.TGetById(g.Key),
                    Total = g.Count(),
                    Used = g.Count(a => a.Status == CampaignStatus.USED)
                })
                .ToList();

            ViewBag.StatusCounts = statusCounts;
            ViewBag.CampaignUsage = campaignUsage;

            return View();
        }
    }
}
