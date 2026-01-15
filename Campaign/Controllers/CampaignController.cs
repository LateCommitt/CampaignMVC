using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using EntityLayer.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;

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
        [ValidateAntiForgeryToken]
        public IActionResult Create(EntityLayer.Concrete.Campaign campaign)
        {
            // Enum binding sorununu düzelt - Request.Form'dan direkt al
            if (Request.Form.ContainsKey("Type"))
            {
                var typeValue = Request.Form["Type"].ToString();
                if (Enum.TryParse<CampaignType>(typeValue, out var parsedType))
                {
                    campaign.Type = parsedType;
                    ModelState.Remove("Type");
                    ModelState.SetModelValue("Type", new Microsoft.AspNetCore.Mvc.ModelBinding.ValueProviderResult(typeValue));
                }
            }

            if (Request.Form.ContainsKey("TargetSegment"))
            {
                var segmentValue = Request.Form["TargetSegment"].ToString();
                if (Enum.TryParse<Segment>(segmentValue, out var parsedSegment))
                {
                    campaign.TargetSegment = parsedSegment;
                    ModelState.Remove("TargetSegment");
                    ModelState.SetModelValue("TargetSegment", new Microsoft.AspNetCore.Mvc.ModelBinding.ValueProviderResult(segmentValue));
                }
            }

            // ModelState hatalarını kontrol et
            if (!ModelState.IsValid)
            {
                // Hataları logla
                foreach (var error in ModelState)
                {
                    foreach (var errorMessage in error.Value.Errors)
                    {
                        // Debug için hataları görebilirsiniz
                        System.Diagnostics.Debug.WriteLine($"Key: {error.Key}, Error: {errorMessage.ErrorMessage}");
                    }
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

            try
            {
                _campaignService.TAdd(campaign);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Hata durumunda logla ve kullanıcıya göster
                ModelState.AddModelError("", $"Kampanya eklenirken bir hata oluştu: {ex.Message}");

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
