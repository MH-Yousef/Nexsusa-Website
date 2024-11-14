using Data.Dtos.NavBarDTOs;
using Microsoft.AspNetCore.Mvc;
using Services.HomePageServices.NavBarItemServices;
using Services.LanguageServices;

namespace Nexsusa_Api.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NavbarController(INavBarItemService _navBarItemService,ILanguageService _languageService) : Controller
    {
        public async Task<IActionResult> Navbar(int? langId=null)
        {
            List<NavBarItemDTO> result;
            //var currentLanguage = HttpContext.Session.GetString("CurrentLanguage");
            ViewBag.Languages =  (await _languageService.Get()).Data;
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
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _navBarItemService.GetById(id, 1);
            return View(model.Data);
        }
        public async Task<IActionResult> Save(NavBarItemDTO dto)
        {
            return Json(dto);
        }
    }
}
