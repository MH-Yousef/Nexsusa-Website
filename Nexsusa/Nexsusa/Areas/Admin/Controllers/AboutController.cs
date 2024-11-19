using Data.Dtos.AboutDTOs;
using Microsoft.AspNetCore.Mvc;
using Services.AboutServices;
using Services.LanguageServices;

namespace Nexsusa_Api.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AboutController(ILanguageService _languageService, IAboutService _aboutService) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Manage(int? languageId = null)
        {
            ViewBag.Languages = (await _languageService.Get(true)).Data;
            if (languageId == null)
            {
                var defaultLanguageId = (await _languageService.Get()).Data.FirstOrDefault(x => x.IsDefault).Id;
                ViewBag.SelectedLanguageId = defaultLanguageId;
                var model = await _aboutService.GetFirst();
                return View(model.Data);
            }
            else
            {
                ViewBag.SelectedLanguageId = languageId;
                var result = await _aboutService.Get(languageId.Value);
                return View(result.Data);
            }

        }

        public async Task<IActionResult> Save(AboutDTO dto)
        {
            if (dto.Title == null || dto.Description == null)
            {
                return Json(new { success = false, message = "Title and Description are required" });
            }
            var result = await _aboutService.Manage(dto);
            return Json(result);
        }
        // get item
        [HttpGet]
        public async Task<IActionResult> Get(int languageId)
        {
            var result = await _aboutService.Get(languageId);
            return Json(result);
        }
    }
}
