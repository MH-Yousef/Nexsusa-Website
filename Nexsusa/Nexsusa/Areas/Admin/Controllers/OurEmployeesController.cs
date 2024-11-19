using Data.Dtos.OurEmployeeDTOs;
using Microsoft.AspNetCore.Mvc;
using Services.HomePageServices.OurEmployeeServices;
using Services.LanguageServices;

namespace Nexsusa_Api.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OurEmployeesController(IOurEmployeesService _ourEmployeesService, ILanguageService _languageService) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Manage(int? languageId = null)
        {
            OurEmployeesDTO result;
            var firstItam = await _ourEmployeesService.GetFirst();
            ViewBag.Languages = (await _languageService.Get(true)).Data;
            if (languageId != null)
            {
                ViewBag.SelectedLanguageId = languageId;
                if (firstItam?.Data == null)
                {
                    return View(new OurEmployeesDTO());
                }
                result = (await _ourEmployeesService.GetById(firstItam.Data.Id, languageId.Value)).Data;
            }
            else
            {
                var defaultLanguageId = (await _languageService.Get()).Data.FirstOrDefault(x => x.IsDefault).Id;
                if (firstItam?.Data == null)
                {
                    result = new OurEmployeesDTO
                    {
                        OurEmployeesItems = []
                    };
                }
                else
                {
                    result = (await _ourEmployeesService.GetById(firstItam.Data.Id, defaultLanguageId)).Data;
                }
                ViewBag.SelectedLanguageId = defaultLanguageId;
            }
            return View(result);
        }
        // save item
        public async Task<IActionResult> Save(OurEmployeesDTO dto)
        {
            if (dto.Title == null || dto.Description == null)
            {
                return Json(new { success = false, message = "Title and Description are required" });
            }
            var result = await _ourEmployeesService.Manage(dto);
            return Json(result);
        }
        // get item
        [HttpGet]
        public async Task<IActionResult> Get(int languageId)
        {
            var firstService = await _ourEmployeesService.GetFirst();
            if (firstService?.Data == null)
            {
                return Json(new { success = false, message = "Service not found" });
            }

            int id = firstService.Data.Id;
            var result = await _ourEmployeesService.GetById(id, languageId);
            return Json(result);
        }
        // add Sub Item
        public async Task<IActionResult> ManageSubItem(OurEmployeesItemDTO subDto)
        {
            var defaultLanguageId = (await _languageService.Get()).Data.FirstOrDefault(x => x.IsDefault).Id;
            if (subDto.LangId != defaultLanguageId && subDto.Id == 0)
            {
                return Json("ok");
            }
            else
            {
                var result = await _ourEmployeesService.ManageSubItem(subDto);
                return Json(result);
            }


        }
        // get Sub Item
        [HttpGet]
        public async Task<IActionResult> GetSubItem(int id, int OurEmployeesId, int languageId)
        {
            if (id == 0)
            {
                return Json("Ok");
            }
            var result = (await _ourEmployeesService.GetById(OurEmployeesId, languageId)).Data.OurEmployeesItems.FirstOrDefault(x => x.Id == id);
            return Json(result);
        }
    }
}
