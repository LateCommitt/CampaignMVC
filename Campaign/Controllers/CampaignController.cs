using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using EntityLayer.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Campaign.Controllers
{
    public class CampaignController : Controller
    {
        private readonly ICampaignService _campaignService;

        public CampaignController(ICampaignService campaignService)
        {
            _campaignService = campaignService;
        }

        public IActionResult Index()
        {
            var campaigns = _campaignService.GetList();
            return View(campaigns);
        }

        public IActionResult Create()
        {
            ViewBag.CampaignTypes = Enum.GetValues(typeof(CampaignType))
                .Cast<CampaignType>()
                .Select(e => new SelectListItem
                {
                    Value = e.ToString(),
                    Text = e.ToString()
                }).ToList();

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
        public IActionResult Create(EntityLayer.Concrete.Campaign campaign)
        {
            if (ModelState.IsValid)
            {
                _campaignService.TAdd(campaign);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.CampaignTypes = Enum.GetValues(typeof(CampaignType))
                .Cast<CampaignType>()
                .Select(e => new SelectListItem
                {
                    Value = e.ToString(),
                    Text = e.ToString()
                }).ToList();

            ViewBag.Segments = Enum.GetValues(typeof(Segment))
                .Cast<Segment>()
                .Select(e => new SelectListItem
                {
                    Value = e.ToString(),
                    Text = e.ToString()
                }).ToList();

            return View(campaign);
        }

        public IActionResult Edit(int id)
        {
            var campaign = _campaignService.TGetById(id);
            if (campaign == null)
            {
                return NotFound();
            }

            ViewBag.CampaignTypes = Enum.GetValues(typeof(CampaignType))
                .Cast<CampaignType>()
                .Select(e => new SelectListItem
                {
                    Value = e.ToString(),
                    Text = e.ToString(),
                    Selected = e == campaign.Type
                }).ToList();

            ViewBag.Segments = Enum.GetValues(typeof(Segment))
                .Cast<Segment>()
                .Select(e => new SelectListItem
                {
                    Value = e.ToString(),
                    Text = e.ToString(),
                    Selected = e == campaign.TargetSegment
                }).ToList();

            return View(campaign);
        }

        [HttpPost]
        public IActionResult Edit(EntityLayer.Concrete.Campaign campaign)
        {
            if (ModelState.IsValid)
            {
                _campaignService.TUpdate(campaign);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.CampaignTypes = Enum.GetValues(typeof(CampaignType))
                .Cast<CampaignType>()
                .Select(e => new SelectListItem
                {
                    Value = e.ToString(),
                    Text = e.ToString(),
                    Selected = e == campaign.Type
                }).ToList();

            ViewBag.Segments = Enum.GetValues(typeof(Segment))
                .Cast<Segment>()
                .Select(e => new SelectListItem
                {
                    Value = e.ToString(),
                    Text = e.ToString(),
                    Selected = e == campaign.TargetSegment
                }).ToList();

            return View(campaign);
        }

        public IActionResult Delete(int id)
        {
            var campaign = _campaignService.TGetById(id);
            if (campaign == null)
            {
                return NotFound();
            }

            return View(campaign);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var campaign = _campaignService.TGetById(id);
            if (campaign != null)
            {
                _campaignService.TDelete(campaign);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult ToggleActive(int id)
        {
            var campaign = _campaignService.TGetById(id);
            if (campaign != null)
            {
                campaign.IsActive = !campaign.IsActive;
                _campaignService.TUpdate(campaign);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
