using Core.Domains.Languages;
using Microsoft.AspNetCore.Mvc;
using Services.LanguageServices;


namespace Nexsusa_Api.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LanguageController(ILanguageService _languageService) : Controller
    {
        public async Task<IActionResult> List()
        {
            ViewBag.Languages = (await _languageService.Get(true)).Data;
            var result = await _languageService.GetAllStringResources();
            return View(result.Data);
        }

        public async Task<IActionResult> Get(int id)
        {
            var model = await _languageService.GetStringResourceById(id);
            return Json(model.Data);
        }

        public async Task<IActionResult> Save(Translate dto)
        {
            try
            {
                if (dto.Id > 0)
                {
                    await _languageService.UpdateStringResource(dto);
                }
                else
                {
                    await _languageService.CreateStringResource(dto);
                }
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}
