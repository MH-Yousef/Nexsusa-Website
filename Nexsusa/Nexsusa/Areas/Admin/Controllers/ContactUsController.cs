using Data.Dtos.ContactUsDTOs;

using Services.LanguageServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Services.HomePageServices.ContactUsServices;
using Services.ContactUsPageServices;

namespace Nexsusa_Api.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContactUsController : Controller
    {
        private readonly IContactUsPageService _contactUsService;
        private readonly ILanguageService _languageService;

        public ContactUsController(IContactUsPageService contactUsService, ILanguageService languageService)
        {
            _contactUsService = contactUsService;
            _languageService = languageService;
        }

        [HttpGet]
        public async Task<IActionResult> Manage(int? languageId = null)
        {
            ContactUsPageDTO result;
            var firstItem = await _contactUsService.GetFirst();
            ViewBag.Languages = (await _languageService.Get(true)).Data;
            if (languageId != null)
            {
                ViewBag.SelectedLanguageId = languageId;
                if (firstItem?.Data == null)
                {
                    return View(new ContactUsPageDTO());
                }
                result = (await _contactUsService.GetById(firstItem.Data.Id, languageId.Value)).Data;
            }
            else
            {
                var defaultLanguageId = (await _languageService.Get()).Data.FirstOrDefault(x => x.IsDefault).Id;
                if (firstItem?.Data == null)
                {
                    result = new ContactUsPageDTO();
                    result.ContactUsItems = new List<ContactUsItemDTO>();
                }
                else
                {
                    result = (await _contactUsService.GetById(firstItem.Data.Id, defaultLanguageId)).Data;
                }
                ViewBag.SelectedLanguageId = defaultLanguageId;
            }
            return View(result);
        }

        public async Task<IActionResult> Save(ContactUsPageDTO dto)
        {
            if (dto.Title == null || dto.Description == null)
            {
                return Json(new { success = false, message = "Title and Description are required" });
            }
            var result = await _contactUsService.Manage(dto);
            return Json(result);
        }

        // Add Sub Item (ContactUsItem)
        [HttpPost]
        public async Task<IActionResult> ManageSubItem(ContactUsItemDTO subDto)
        {
            var result = await _contactUsService.ManageSubItem(subDto);
            return Json(result);
        }

        // Get Sub Item (ContactUsItem)
        [HttpGet]
        public async Task<IActionResult> GetSubItem(int id, int contactUsPageId, int languageId)
        {
            var result = await _contactUsService.GetSubItem(id, contactUsPageId, languageId);
            return Json(result);
        }
    }
}
