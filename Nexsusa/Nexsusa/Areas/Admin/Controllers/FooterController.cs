using Data.Dtos.FooterDTOs;
using Data.Dtos.HomePageDTOs;
using Microsoft.AspNetCore.Mvc;
using Services._Base;
using Services.HomePageServices.FooterServices;
using Services.LanguageServices;

namespace Nexsusa_Api.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FooterController(ILanguageService _languageService, IFooterService _footerService) : Controller
    {
        public async Task<IActionResult> Manage(int? languageId = null)
        {
            ViewBag.Languages = (await _languageService.Get(true)).Data;
            if (languageId == null)
            {
                var defaultLanguageId = (await _languageService.Get()).Data.FirstOrDefault(x => x.IsDefault).Id;
                ViewBag.SelectedLanguageId = defaultLanguageId;
                var model = await _footerService.GetFirst();
                return View(model.Data);
            }
            else
            {
                ViewBag.SelectedLanguageId = languageId;
                var result = await _footerService.Get(languageId.Value);
                return View(result.Data);
            }
        }
        public async Task<IActionResult> Save(FooterDTO dto)
        {
            try
            {
                bool isInputsNull =  dto.Description == null || dto.PhoneNumber == null || dto.Email == null || dto.Address == null;
                if (isInputsNull)
                {
                    return Json(new { success = false, message = "All Fields must be filled." });
                }
                var response = await _footerService.Manage(dto);
                if (response.IsSuccess)
                {
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false, message = "Footer doesn't updated!!!!" });
                }
            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = ex.GetError() });

            }


        }
    }
}
