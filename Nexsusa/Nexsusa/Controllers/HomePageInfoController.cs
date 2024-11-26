using Data.Dtos.HomePageDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.HomePageServices.HomePageInfoServices;

namespace Nexsusa_Api.Controllers
{
    [Route("api/[controller]")]
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
    }
}
