using Services.LanguageServices;
using Microsoft.AspNetCore.Mvc;
using Services.ContactUsPageServices;
using Data.Dtos.ContactUsDTOs;

namespace Nexsusa_Api.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContactUsController(IContactUsService _contactUsService, ILanguageService _languageService) : Controller
    {
        public async Task<IActionResult> Manage()
        {
            ViewBag.Languages = (await _languageService.Get(true)).Data;
            var model = await _contactUsService.GetFirst();
            return View(model.Data);
        }

        public async Task<IActionResult> Save(ContactUsDTO dto)
        {
            if (dto.Title == null || dto.Description == null)
            {
                return Json(new { success = false, message = "Title and Description are required" });
            }
            var result = await _contactUsService.Manage(dto);
            return Json(result);
        }
        [HttpGet]
        public async Task<IActionResult> Get(int languageId)
        {
            var firstSlider = await _contactUsService.GetFirst();
            if (firstSlider?.Data == null)
            {
                return Json(new { success = false, message = "Slider not found" });
            }

            int id = firstSlider.Data.Id;
            var result = await _contactUsService.GetById(id, languageId);
            return Json(result);
        }
    }
}
