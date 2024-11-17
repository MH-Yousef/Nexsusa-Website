using Data.Dtos.ServiceDTOs;
using Data.Dtos.WorkingProcessDTOs;
using Microsoft.AspNetCore.Mvc;
using Services.HomePageServices.ServiceServices;
using Services.LanguageServices;

namespace Nexsusa_Api.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ServicesController(IServiceService _serviceService, ILanguageService _languageService) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Manage(int? languageId = null)
        {
            ServiceDTO result;
            var firstItam = await _serviceService.GetFirst();
            ViewBag.Languages = (await _languageService.Get(true)).Data;
            if (languageId != null)
            {
                ViewBag.SelectedLanguageId = languageId;
                if (firstItam?.Data == null)
                {
                    return View(new ServiceDTO());
                }
                result = (await _serviceService.GetById(firstItam.Data.Id, languageId.Value)).Data;
            }
            else
            {
                var defaultLanguageId = (await _languageService.Get()).Data.FirstOrDefault(x => x.IsDefault).Id;
                if (firstItam?.Data == null)
                {
                    result = new ServiceDTO
                    {
                        ServiceItems = []
                    };
                }
                else
                {
                    result = (await _serviceService.GetById(firstItam.Data.Id, defaultLanguageId)).Data;
                }
                ViewBag.SelectedLanguageId = defaultLanguageId;
            }
            return View(result);
        }
        // save item
        public async Task<IActionResult> Save(ServiceDTO dto)
        {
            if (dto.Title == null || dto.Description == null)
            {
                return Json(new { success = false, message = "Title and Description are required" });
            }
            var result = await _serviceService.Manage(dto);
            return Json(result);
        }
        // get item
        [HttpGet]
        public async Task<IActionResult> Get(int languageId)
        {
            var firstService = await _serviceService.GetFirst();
            if (firstService?.Data == null)
            {
                return Json(new { success = false, message = "Service not found" });
            }

            int id = firstService.Data.Id;
            var result = await _serviceService.GetById(id, languageId);
            return Json(result);
        }
        // add Sub Item
        public async Task<IActionResult> ManageSubItem(ServiceItemDTO subDto)
        {
            var defaultLanguageId = (await _languageService.Get()).Data.FirstOrDefault(x => x.IsDefault).Id;
            if (subDto.LangId != defaultLanguageId && subDto.Id == 0)
            {
                return Json("ok");
            }
            else
            {
                var result = await _serviceService.ManageSubItem(subDto);
            }

            return Json("ok");
        }

        // get Sub Item
        [HttpGet]
        public async Task<IActionResult> GetSubItem(int id, int serviceId, int languageId)
        {
            if (id == 0)
            {
                return Json("Ok");
            }
            var result = (await _serviceService.GetById(serviceId, languageId)).Data.ServiceItems.FirstOrDefault(x => x.Id == id);
            return Json(result);
        }
    }
}
