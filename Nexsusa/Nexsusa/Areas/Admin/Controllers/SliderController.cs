using Data.Dtos.SliderDTOs;
using Microsoft.AspNetCore.Mvc;
using Services.HomePageServices.SliderServices;
using Services.LanguageServices;

namespace Nexsusa_Api.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController(ILanguageService _languageService, ISliderService _sliderService) : Controller
    {
        public async Task<IActionResult> Manage()
        {
            ViewBag.Languages = (await _languageService.Get(true)).Data;
            var model = await _sliderService.GetFirst();
            return View(model.Data);
        }

        public async Task<IActionResult> Save(SliderDTO dto)
        {
            if (dto.Title == null || dto.Description == null)
            {
                return Json(new { success = false, message = "Title and Description are required" });
            }
            var result = await _sliderService.Manage(dto);
            return Json(result);
        }
        // get item
        [HttpGet]
        public async Task<IActionResult> Get(int languageId)
        {
            var firstSlider = await _sliderService.GetFirst();
            if (firstSlider?.Data == null)
            {
                return Json(new { success = false, message = "Slider not found" });
            }

            int id = firstSlider.Data.Id;
            var result = await _sliderService.GetById(id, languageId);
            return Json(result);
        }
    }
}
