using Core.Domains.Languages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.CodeAnalysis.Host;
using Services._Base;
using Services.LanguageServices;

namespace Nexsusa_Api.Areas.Admin.Controllers
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
