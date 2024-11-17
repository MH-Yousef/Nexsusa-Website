using Data.Dtos.NavBarDTOs;
using Microsoft.AspNetCore.Mvc;
using Services.HomePageServices.NavBarItemServices;
using Services.LanguageServices;

namespace Nexsusa_Api.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NavbarController(INavBarItemService _navBarItemService, ILanguageService _languageService) : Controller
    {
        public async Task<IActionResult> List(int? langId = null)
        {
            List<NavBarItemDTO> result;
            //var currentLanguage = HttpContext.Session.GetString("CurrentLanguage");
            ViewBag.Languages = (await _languageService.Get(true)).Data;
            if (langId != null)
            {

                result = (await _navBarItemService.GetList(langId.Value)).Data;

                ViewBag.SelectedLanguageId = result != null ? langId : 0;
            }
            else
            {

                var defaultLanguageId = (await _languageService.Get()).Data.FirstOrDefault(x => x.IsDefault).Id;
                result = (await _navBarItemService.GetList(defaultLanguageId)).Data;
                ViewBag.SelectedLanguageId = defaultLanguageId;

            }
            return View(result);
        }
        public async Task<IActionResult> Manage(int id, int? languageId = null)
        {
            if (id > 0)
            {
                if (languageId == null)
                {
                    languageId = (await _languageService.Get()).Data.FirstOrDefault(x => x.IsDefault).Id;
                }
                var model = await _navBarItemService.GetById(id, languageId.Value);
                return View(model.Data);
            }
            ViewBag.Languages = (await _languageService.Get(true)).Data;
            return View();

        }
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _navBarItemService.Delete(id);
            return Json(result);
        }
        public async Task<IActionResult> Save(List<NavBarItemDTO> dto)
        {
            var result = await _navBarItemService.Manage(dto);
            return Json(dto);
        }
    }
}
