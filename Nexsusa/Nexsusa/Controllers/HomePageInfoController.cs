using Data.Dtos.HomePageDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.HomePageServices.HomePageInfoServices;

namespace Nexsusa_Api.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class HomePageInfoController : ControllerBase
    {
        private readonly IHomePageInfoService _homePageInfoService;

        public HomePageInfoController(IHomePageInfoService homePageInfoService)
        {
            _homePageInfoService = homePageInfoService;
        }

        [HttpPost]
        public async Task<IActionResult> Manage(HomePageInfoDTO dto)
        {
            await _homePageInfoService.ManageAsync(dto);
            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> GetHomePageInfo(int languageId)
        {
            var response = await _homePageInfoService.GetHomePageInfoAsync(languageId);
            return Ok(response);
        }
    }
}
