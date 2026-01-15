using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using EntityLayer.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;

namespace Campaign.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IUserMetricsService _userMetricsService;
        private readonly ICampaignAssignmentService _campaignAssignmentService;
        private readonly ICampaignRecommendationService _recommendationService;
        private readonly ICampaignService _campaignService;

        public UserController(
            IUserService userService,
            IUserMetricsService userMetricsService,
            ICampaignAssignmentService campaignAssignmentService,
            ICampaignRecommendationService recommendationService,
            ICampaignService campaignService)
        {
            _userService = userService;
            _userMetricsService = userMetricsService;
            _campaignAssignmentService = campaignAssignmentService;
            _recommendationService = recommendationService;
            _campaignService = campaignService;
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

        [HttpPost]
        public IActionResult ProcessUserScore([FromBody] ProcessScoreRequest request)
        {
            try
            {
                // Skor hesapla ve kampanya ata
                _campaignAssignmentService.AssignCampaignsToUser(request.UserId);

                // Skor bilgisini al
                var scoreBreakdown = _recommendationService.GetUserScoreBreakdown(request.UserId);

                return Json(new
                {
                    success = true,
                    message = "Skor hesaplandı ve kampanya atandı!",
                    score = scoreBreakdown?.TotalScore ?? 0,
                    usageScore = scoreBreakdown?.UsageScore ?? 0,
                    spendScore = scoreBreakdown?.SpendScore ?? 0,
                    loyaltyScore = scoreBreakdown?.LoyaltyScore ?? 0
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Hata: {ex.Message}" });
            }
        }

        [HttpPost]
        public IActionResult CreateQuickUser([FromBody] QuickUserRequest request)
        {
            try
            {
                // Kullanıcı oluştur
                var user = new User
                {
                    Name = request.Name,
                    Email = $"{request.Name.ToLower().Replace(" ", "")}@example.com",
                    City = "İstanbul",
                    Segment = Enum.Parse<Segment>(request.Segment)
                };
                _userService.TAdd(user);

                // Segment'e göre mock metrikler oluştur
                var random = new Random();
                int monthlyDataGb, monthlySpendTry, loyaltyYears;

                switch (user.Segment)
                {
                    case Segment.HIGH_USAGE:
                        monthlyDataGb = random.Next(50, 150);
                        monthlySpendTry = random.Next(200, 500);
                        loyaltyYears = random.Next(3, 10);
                        break;
                    case Segment.MEDIUM_USAGE:
                        monthlyDataGb = random.Next(20, 50);
                        monthlySpendTry = random.Next(100, 200);
                        loyaltyYears = random.Next(1, 5);
                        break;
                    default:
                        monthlyDataGb = random.Next(5, 20);
                        monthlySpendTry = random.Next(50, 100);
                        loyaltyYears = random.Next(0, 3);
                        break;
                }

                var metrics = new UserMetrics
                {
                    UserId = user.Id,
                    MonthlyDataGb = monthlyDataGb,
                    MonthlySpendTry = monthlySpendTry,
                    LoyaltyYears = loyaltyYears
                };
                _userMetricsService.TAdd(metrics);

                // İlk kampanyayı ata (Kampanya 1)
                var firstCampaign = _campaignAssignmentService.GetAllList()
                    .FirstOrDefault()?.CampaignId ?? 1;

                // İlk kampanyayı bul
                var firstCampaignObj = _campaignAssignmentService.GetAllList().FirstOrDefault()?.Campaign;
                int calculatedScore = 0;
                if (firstCampaignObj != null)
                {
                    calculatedScore = _campaignAssignmentService.CalculateScore(user, firstCampaignObj);
                }

                var assignment = new CampaignAssignment
                {
                    UserId = user.Id,
                    CampaignId = firstCampaign,
                    Score = calculatedScore,
                    Status = CampaignStatus.ASSIGNED,
                    AssignedAt = DateTime.Now
                };
                _campaignAssignmentService.TAdd(assignment);

                return Json(new { success = true, userId = user.Id });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        public IActionResult ScoreDetails(int id)
        {
            var user = _userService.TGetById(id);
            if (user == null) return NotFound();

            var metrics = _userMetricsService.GetList().FirstOrDefault(m => m.UserId == id);
            var assignments = _campaignAssignmentService.GetAssignmentsByUser(id);
            var scoreBreakdown = _recommendationService.GetUserScoreBreakdown(id);
            var recommendations = _recommendationService.GetRecommendedCampaigns(id).Take(3).ToList();

            ViewBag.Metrics = metrics;
            ViewBag.Assignments = assignments;
            ViewBag.ScoreBreakdown = scoreBreakdown;
            ViewBag.Recommendations = recommendations;

            return View(user);
        }

        public IActionResult UserRecommendations(int id)
        {
            // Herhangi bir kullanıcı için skor hesapla ve önerileri göster
            var user = _userService.TGetById(id);
            if (user == null) return NotFound();

            // Database'den güncel verileri al
            var metrics = _userMetricsService.GetList().FirstOrDefault(m => m.UserId == id);
            var assignments = _campaignAssignmentService.GetAssignmentsByUser(id);

            // Skor hesapla (formül ile)
            var scoreBreakdown = _recommendationService.GetUserScoreBreakdown(id);

            // Kampanya önerilerini al (formül ile hesaplanmış)
            var recommendations = _recommendationService.GetRecommendedCampaigns(id).ToList();

            ViewBag.Metrics = metrics;
            ViewBag.Assignments = assignments;
            ViewBag.ScoreBreakdown = scoreBreakdown;
            ViewBag.Recommendations = recommendations;

            return View("AliRecommendations", user); // Aynı view'ı kullan
        }

        public IActionResult AliRecommendations()
        {
            // Ali kullanıcısını bul (tam eşleşme)
            var aliUser = _userService.GetList().FirstOrDefault(u => u.Name.ToLower() == "ali");

            if (aliUser == null)
            {
                // Ali kullanıcısı yoksa oluştur
                aliUser = new User
                {
                    Name = "Ali",
                    Email = "ali@example.com",
                    City = "İstanbul",
                    Segment = Segment.HIGH_USAGE
                };
                _userService.TAdd(aliUser);

                // Yüksek kullanım metrikleri oluştur
                var metrics = new UserMetrics
                {
                    UserId = aliUser.Id,
                    MonthlyDataGb = 120, // Yüksek veri kullanımı
                    MonthlySpendTry = 350, // Yüksek harcama
                    LoyaltyYears = 5 // Uzun süreli müşteri
                };
                _userMetricsService.TAdd(metrics);
            }

            // Database'den güncel verileri al
            var metricsAli = _userMetricsService.GetList().FirstOrDefault(m => m.UserId == aliUser.Id);
            var assignmentsAli = _campaignAssignmentService.GetAssignmentsByUser(aliUser.Id);

            // Skor hesapla (formül ile)
            var scoreBreakdownAli = _recommendationService.GetUserScoreBreakdown(aliUser.Id);

            // Kampanya önerilerini al (formül ile hesaplanmış)
            var recommendationsAli = _recommendationService.GetRecommendedCampaigns(aliUser.Id).ToList();

            ViewBag.Metrics = metricsAli;
            ViewBag.Assignments = assignmentsAli;
            ViewBag.ScoreBreakdown = scoreBreakdownAli;
            ViewBag.Recommendations = recommendationsAli;

            return View(aliUser);
        }
    }

    public class ProcessScoreRequest
    {
        public int UserId { get; set; }
    }

    public class QuickUserRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Segment { get; set; } = string.Empty;
    }
}

