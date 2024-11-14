using Data.Dtos.AboutDTOs;
using Microsoft.AspNetCore.Mvc;
using Services.AboutServices;

namespace Nexsusa_Api.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class AboutUsController(IAboutService _aboutService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get(int languageId)
        {
            var result = await _aboutService.Get(languageId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Manage(List<AboutDTO> dto)
        {
            var result = await _aboutService.Manage(dto);
            return Ok(result);
        }
    }
}
