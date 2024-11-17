using Data.Dtos.OurCompanyDTOs;
using Data.Dtos.SliderDTOs;
using Microsoft.AspNetCore.Mvc;
using Services.HomePageServices.OurCompanyServices;
using Services.LanguageServices;

namespace Nexsusa_Api.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OurCompanyController(ILanguageService _languageService,IOurCompanyService _ourCompanyService) : Controller
    {
        public async Task<IActionResult> Manage()
        {
            ViewBag.Languages = (await _languageService.Get(true)).Data;
            var model = await _ourCompanyService.GetFirst();
            return View(model.Data);
        }

        public async Task<IActionResult> Save(OurCompanyDTO dto)
        {
            if (dto.Title == null || dto.Description == null)
            {
                return Json(new { success = false, message = "Title and Description are required" });
            }
            var result = await _ourCompanyService.Manage(dto);
            return Json(result);
        }
        // get item
        [HttpGet]
        public async Task<IActionResult> Get(int languageId)
        {
            var firstOurCompany = await _ourCompanyService.GetFirst();
            if (firstOurCompany?.Data == null)
            {
                return Json(new { success = false, message = "OurCompany not found" });
            }

            int id = firstOurCompany.Data.Id;
            var result = await _ourCompanyService.GetById(id, languageId);
            return Json(result);
        }
    }

    
}
