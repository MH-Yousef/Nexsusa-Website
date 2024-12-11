using Core.SocialLinks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services._Base;
using Services.SocialLinkServices;

namespace Nexsusa_Api.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class SocialLinksController(ISocialLinkService _socialLinkService) : ControllerBase
    {
        [HttpGet]
        public async Task<ResponseResult<List<SocialLink>>> Get()
        {
            var response = await _socialLinkService.Get();
            return response;
        }
    }
}
