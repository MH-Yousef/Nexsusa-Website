using Data.Dtos.ChooseUsDTOs;
using Data.Dtos.ServiceDTOs;
using Data.Dtos.WorkingProcessDTOs;
using Microsoft.AspNetCore.Mvc;
using Services.HomePageServices.WorkingProcessServices;
using Services.LanguageServices;

namespace Nexsusa_Api.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class WorkingProcessController(IWorkingProcessService _workingProcessService, ILanguageService _languageService) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Manage(int? languageId = null)
        {
            WorkingProcessDTO result;
            var firstItam = await _workingProcessService.GetFirst();
            ViewBag.Languages = (await _languageService.Get(true)).Data;
            if (languageId != null)
            {
                ViewBag.SelectedLanguageId = languageId;
                if (firstItam?.Data == null)
                {
                    return View(new WorkingProcessDTO());
                }
                result = (await _workingProcessService.GetById(firstItam.Data.Id, languageId.Value)).Data;
            }
            else
            {
                var defaultLanguageId = (await _languageService.Get()).Data.FirstOrDefault(x => x.IsDefault).Id;
                if (firstItam?.Data == null)
                {
                    result = new WorkingProcessDTO
                    {
                        WorkingProcessItems = []
                    };
                }
                else
                {
                    result = (await _workingProcessService.GetById(firstItam.Data.Id, defaultLanguageId)).Data;
                }
                ViewBag.SelectedLanguageId = defaultLanguageId;
            }
            return View(result);
        }
        // save item
        public async Task<IActionResult> Save(WorkingProcessDTO dto)
        {
            if (dto.Title == null || dto.SubTitle == null)
            {
                return Json(new { success = false, message = "Title and Description are required" });
            }
            var result = await _workingProcessService.Manage(dto);
            return Json(result);
        }
        // get item
        [HttpGet]
        public async Task<IActionResult> Get(int languageId)
        {
            var firstService = await _workingProcessService.GetFirst();
            if (firstService?.Data == null)
            {
                return Json(new { success = false, message = "Service not found" });
            }

            int id = firstService.Data.Id;
            var result = await _workingProcessService.GetById(id, languageId);
            return Json(result);
        }
        // add Sub Item
        public async Task<IActionResult> ManageSubItem(WorkingProcessItemDTO subDto)
        {
            var defaultLanguageId = (await _languageService.Get()).Data.FirstOrDefault(x => x.IsDefault).Id;
            if (subDto.LangId != defaultLanguageId && subDto.Id == 0)
            {
                return Json("ok");
            }
            else
            {
                var result = await _workingProcessService.ManageSubItem(subDto);
            }

            return Json("ok");
        }

        // get Sub Item
        [HttpGet]
        public async Task<IActionResult> GetSubItem(int id, int parentId, int languageId)
        {
            if (id == 0)
            {
                return Json("Ok");
            }
            var result = (await _workingProcessService.GetById(parentId, languageId)).Data.WorkingProcessItems.FirstOrDefault(x => x.Id == id);
            return Json(result);
        }
    }
}
