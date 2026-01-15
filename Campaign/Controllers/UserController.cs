using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using EntityLayer.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace Campaign.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IUserMetricsService _userMetricsService;
        private readonly ICampaignAssignmentService _campaignAssignmentService;

        public UserController(
            IUserService userService,
            IUserMetricsService userMetricsService,
            ICampaignAssignmentService campaignAssignmentService)
        {
            _userService = userService;
            _userMetricsService = userMetricsService;
            _campaignAssignmentService = campaignAssignmentService;
        }

        public IActionResult Index()
        {
            var users = _userService.GetList();
            return View(users);
        }

        public IActionResult Create()
        {
            ViewBag.Segments = Enum.GetValues(typeof(Segment))
                .Cast<Segment>()
                .Select(e => new SelectListItem
                {
                    Value = e.ToString(),
                    Text = e.ToString()
                }).ToList();

            return View();
        }

        [HttpPost]
        public IActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                _userService.TAdd(user);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Segments = Enum.GetValues(typeof(Segment))
                .Cast<Segment>()
                .Select(e => new SelectListItem
                {
                    Value = e.ToString(),
                    Text = e.ToString()
                }).ToList();

            return View(user);
        }

        public IActionResult Details(int id)
        {
            var user = _userService.TGetById(id);
            if (user == null)
            {
                return NotFound();
            }

            var metrics = _userMetricsService.GetList().FirstOrDefault(m => m.UserId == id);
            var assignments = _campaignAssignmentService.GetAssignmentsByUser(id);

            ViewBag.Metrics = metrics;
            ViewBag.Assignments = assignments;

            return View(user);
        }

        public IActionResult Edit(int id)
        {
            var user = _userService.TGetById(id);
            if (user == null)
            {
                return NotFound();
            }

            ViewBag.Segments = Enum.GetValues(typeof(Segment))
                .Cast<Segment>()
                .Select(e => new SelectListItem
                {
                    Value = e.ToString(),
                    Text = e.ToString(),
                    Selected = e == user.Segment
                }).ToList();

            return View(user);
        }

        [HttpPost]
        public IActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                _userService.TUpdate(user);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Segments = Enum.GetValues(typeof(Segment))
                .Cast<Segment>()
                .Select(e => new SelectListItem
                {
                    Value = e.ToString(),
                    Text = e.ToString(),
                    Selected = e == user.Segment
                }).ToList();

            return View(user);
        }

        [HttpPost]
        public IActionResult AssignCampaign(int userId)
        {
            _campaignAssignmentService.AssignCampaignsToUser(userId);
            return RedirectToAction(nameof(Details), new { id = userId });
        }
    }
}

