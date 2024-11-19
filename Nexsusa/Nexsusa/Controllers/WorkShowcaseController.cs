using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.HomePageServices.WorkShowCaseServices;

namespace Nexsusa_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkShowcaseController(IWorkShowCaseService _workShowCaseService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get(int languageId)
        {
            var result = await _workShowCaseService.GetList(languageId);
            return Ok(result);
        }
    }
}
