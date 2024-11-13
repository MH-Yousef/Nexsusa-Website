using Data.Dtos.HomePageDTOs;
using Data.Dtos.NavBarDTOs;
using Microsoft.AspNetCore.Mvc;
using Services._Base;
using Services.HomePageServices;
using Services.HomePageServices.NavBarItemServices;

namespace Nexsusa_Api.Controllers
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
