using Data.Dtos.NavBarDTOs;
using Microsoft.AspNetCore.Mvc;
using Services._Base;
using Services.HomePageServices.NavBarItemServices;

namespace Nexsusa_Api.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class HomePageController(INavBarItemService _navbarService) : ControllerBase
    {
        [HttpPost]
        public async Task<ResponseResult<List<NavBarItemDTO>>> Create(List<NavBarItemDTO> dto)
        {
            var result = await _navbarService.Create(dto);
            return result;
        }
    }
}
