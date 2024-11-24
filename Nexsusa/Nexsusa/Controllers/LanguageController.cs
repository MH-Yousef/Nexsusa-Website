using Core.Domains.Languages;
using Microsoft.AspNetCore.Mvc;
using Services._Base;
using Services.LanguageServices;

namespace Nexsusa_Api.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class LanguageController(ILanguageService _languageService) : ControllerBase
    {
        [HttpGet]
        public async Task<ResponseResult<List<Language>>> Get()
        {
            var result = await _languageService.Get();
            return result;
        }

        [HttpPost]
        // set Langage
        public IActionResult Create(string culture)
        {
            _languageService.SetLanguage(culture);
            return Ok();
        }   

    }
}
