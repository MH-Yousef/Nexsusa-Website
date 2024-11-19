using Microsoft.AspNetCore.Mvc;
using Services.HomePageServices.ServiceServices;

namespace Nexsusa_Api.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ServicePageController : ControllerBase
    {
        private readonly IServiceService _serviceService;

        public ServicePageController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int languageId)
        {
            var result = await _serviceService.GetList(languageId);
            return Ok(result);
        }
    }
}
