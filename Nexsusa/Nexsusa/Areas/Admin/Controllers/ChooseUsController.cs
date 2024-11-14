using Data.Dtos.ChooseUsDTOs;
using Data.Dtos.NavBarDTOs;
using Microsoft.AspNetCore.Mvc;
using Services.HomePageServices.ChooseUsServices;
using Services.LanguageServices;

namespace Nexsusa_Api.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ChooseUsController(IChooseUsService _chooseUsService,ILanguageService _languageService) : Controller
    {
        public async Task<IActionResult> List(int? langId = null)
        {
            List<ChooseUsDTO> result;
            ViewBag.Languages = (await _languageService.Get()).Data;
            if (langId != null)
            {

                result = (await _chooseUsService.GetList(langId.Value)).Data;

                ViewBag.SelectedLanguageId = result != null ? langId : 0;
            }
            else
            {

                var defaultLanguageId = (await _languageService.Get()).Data.FirstOrDefault(x => x.IsDefault).Id;
                result = (await _chooseUsService.GetList(defaultLanguageId)).Data;
                ViewBag.SelectedLanguageId = defaultLanguageId;

            }
            return View(result);
        }
    }
}
