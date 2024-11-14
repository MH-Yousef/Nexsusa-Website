using Data.Dtos.ServicePageDTOs;
using Microsoft.AspNetCore.Mvc;
using Services.ServicePageServices;

namespace Nexsusa_Api.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ServicePageController(IServicePageService _servicePageService) : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> Get(int languageId)
        {
            var result = await _servicePageService.Get(languageId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Manage(List<ServicePageDTO> dto)
        {
            var result = await _servicePageService.Manage(dto);
            return Ok(result);
        }
    }
}
