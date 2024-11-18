using Data.Dtos.WhoWeAreDTOs;
using Microsoft.AspNetCore.Mvc;
using Services.HomePageServices.WhoWeAreServices;
using Services.LanguageServices;

namespace Nexsusa_Api.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class WhoWeAreController(IWhoWeAreService _whoWeAreService, ILanguageService _languageService) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Manage(int? languageId = null)
        {
            WhoWeAreDTO result;
            var firstItam = await _whoWeAreService.GetFirst();
            ViewBag.Languages = (await _languageService.Get(true)).Data;
            if (languageId != null)
            {
                ViewBag.SelectedLanguageId = languageId;
                if (firstItam?.Data == null)
                {
                    return View(new WhoWeAreDTO());
                }
                result = (await _whoWeAreService.GetById(firstItam.Data.Id, languageId.Value)).Data;
            }
            else
            {
                var defaultLanguageId = (await _languageService.Get()).Data.FirstOrDefault(x => x.IsDefault).Id;
                if (firstItam?.Data == null)
                {
                    result = new WhoWeAreDTO
                    {
                        WhoWeAreItems = []
                    };
                }
                else
                {
                    result = (await _whoWeAreService.GetById(firstItam.Data.Id, defaultLanguageId)).Data;
                }
                ViewBag.SelectedLanguageId = defaultLanguageId;
            }
            return View(result);
        }
        // save item
        public async Task<IActionResult> Save(WhoWeAreDTO dto)
        {
            if (dto.Title == null || dto.Description == null)
            {
                return Json(new { success = false, message = "Title and Description are required" });
            }
            var result = await _whoWeAreService.Manage(dto);
            return Json(result);
        }
        // get item
        [HttpGet]
        public async Task<IActionResult> Get(int languageId)
        {
            var firstService = await _whoWeAreService.GetFirst();
            if (firstService?.Data == null)
            {
                return Json(new { success = false, message = "Service not found" });
            }

            int id = firstService.Data.Id;
            var result = await _whoWeAreService.GetById(id, languageId);
            return Json(result);
        }
        // add Sub Item
        public async Task<IActionResult> ManageSubItem(WhoWeAreItemDTO subDto)
        {
            var defaultLanguageId = (await _languageService.Get()).Data.FirstOrDefault(x => x.IsDefault).Id;
            if (subDto.LangId != defaultLanguageId && subDto.Id == 0)
            {
                return Json("ok");
            }
            else
            {
                var result = await _whoWeAreService.ManageSubItem(subDto);
                return Json(result);
            }


        }

        // get Sub Item
        [HttpGet]
        public async Task<IActionResult> GetSubItem(int id, int WhoWeAreId, int languageId)
        {
            if (id == 0)
            {
                return Json("Ok");
            }
            var result = (await _whoWeAreService.GetById(WhoWeAreId, languageId)).Data.WhoWeAreItems.FirstOrDefault(x => x.Id == id);
            return Json(result);
        }
    }
}
