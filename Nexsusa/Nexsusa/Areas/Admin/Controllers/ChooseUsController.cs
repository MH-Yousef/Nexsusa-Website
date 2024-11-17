using Data.Dtos.ChooseUsDTOs;
using Data.Dtos.NavBarDTOs;
using Data.Dtos.QuestionDTOs;
using Data.Dtos.ServiceDTOs;
using Microsoft.AspNetCore.Mvc;
using Services.HomePageServices.ChooseUsServices;
using Services.LanguageServices;

namespace Nexsusa_Api.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ChooseUsController(IChooseUsService _chooseUsService, ILanguageService _languageService) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Manage(int? languageId = null)
        {
            ChooseUsDTO result;
            var firstItam = await _chooseUsService.GetFirst();
            ViewBag.Languages = (await _languageService.Get(true)).Data;
            if (languageId != null)
            {
                ViewBag.SelectedLanguageId = languageId;
                if (firstItam?.Data == null)
                {
                    return View(new ChooseUsDTO());
                }
                result = (await _chooseUsService.GetById(firstItam.Data.Id, languageId.Value)).Data;
            }
            else
            {
                var defaultLanguageId = (await _languageService.Get()).Data.FirstOrDefault(x => x.IsDefault).Id;
                if (firstItam?.Data == null)
                {
                    result = new ChooseUsDTO();
                    result.Questions = new List<QuestionDTO>();
                }
                else
                {
                    result = (await _chooseUsService.GetById(firstItam.Data.Id, defaultLanguageId)).Data;
                }
                ViewBag.SelectedLanguageId = defaultLanguageId;
            }
            return View(result);
        }

        public async Task<IActionResult> Save(ChooseUsDTO dto)
        {
            if (dto.Title == null || dto.Description == null)
            {
                return Json(new { success = false, message = "Title and Description are required" });
            }
            var result = await _chooseUsService.Manage(dto);
            return Json(result);
        }

        // get item
        [HttpGet]
        public async Task<IActionResult> Get(int languageId)
        {
            var firstService = await _chooseUsService.GetFirst();
            if (firstService?.Data == null)
            {
                return Json(new { success = false, message = "Service not found" });
            }

            int id = firstService.Data.Id;
            var result = await _chooseUsService.GetById(id, languageId);
            return Json(result);
        }
        // add Sub Item
        public async Task<IActionResult> ManageSubItem(QuestionDTO subDto)
        {
            var defaultLanguageId = (await _languageService.Get()).Data.FirstOrDefault(x => x.IsDefault).Id;
            if (subDto.LangId != defaultLanguageId && subDto.Id == 0)
            {
                return Json("ok");
            }
            else
            {
                var result = await _chooseUsService.ManageSubItem(subDto);
            }

            return Json("ok");
        }

        // get Sub Item
        [HttpGet]
        public async Task<IActionResult> GetSubItem(int id, int ChooseUsId, int languageId)
        {
            if (id == 0)
            {
                return Json("Ok");
            }
            var result = (await _chooseUsService.GetById(ChooseUsId, languageId)).Data.Questions.FirstOrDefault(x => x.Id == id);
            return Json(result);
        }
    }
}
