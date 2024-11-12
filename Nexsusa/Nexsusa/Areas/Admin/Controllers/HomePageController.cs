using Data.Dtos.HomePageDTOs;
using Microsoft.AspNetCore.Mvc;
using Services._Base;
using Services.HomePageServices;

namespace Nexsusa_Api.Areas.Admin.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class HomePageController(IHomePageServices _homePageServices) : ControllerBase
    {
        [HttpGet]
        public async Task<ResponseResult<HomePageDTO>> GetHomePage(int languageId)
        {
            var result = await _homePageServices.GetHomePage(languageId);
            return result;
        }
    }
}
