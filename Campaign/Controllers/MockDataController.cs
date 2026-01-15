using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Campaign.Controllers
{
    public class MockDataController : Controller
    {
        private readonly IMockDataService _mockDataService;
        private readonly ICampaignRecommendationService _recommendationService;

        public MockDataController(
            IMockDataService mockDataService,
            ICampaignRecommendationService recommendationService)
        {
            _mockDataService = mockDataService;
            _recommendationService = recommendationService;
        }

        [HttpPost]
        public IActionResult SeedAllData()
        {
            try
            {
                _mockDataService.SeedAllMockData();
                TempData["Success"] = "Mock veriler başarıyla oluşturuldu!";
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Hata: {ex.Message}";
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult UserRecommendations(int userId)
        {
            var recommendations = _recommendationService.GetRecommendedCampaigns(userId);
            var scoreBreakdown = _recommendationService.GetUserScoreBreakdown(userId);

            ViewBag.Recommendations = recommendations;
            ViewBag.ScoreBreakdown = scoreBreakdown;

            return View();
        }
    }
}
