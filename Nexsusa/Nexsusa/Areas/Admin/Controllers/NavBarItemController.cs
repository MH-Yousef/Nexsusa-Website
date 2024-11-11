using Data.Dtos.NavBarDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services._Base;
using Services.HomePageServices.NavBarItemServices;

namespace Nexsusa_Api.Areas.Admin.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class NavBarItemController(INavBarItemService _navBarItemService) : ControllerBase
    {
        [HttpGet]
        public async Task<ResponseResult<List<NavBarItemDTO>>> Get(int languageId)
        {
            var result = await _navBarItemService.Get(languageId);
            return result;
        }

        [HttpGet("{id}")]
        public async Task<ResponseResult<NavBarItemDTO>> GetById(int id, int languageId)
        {
            var result = await _navBarItemService.GetById(id, languageId);
            return result;
        }

        [HttpPost]
        public async Task<ResponseResult<List<NavBarItemDTO>>> Create(List<NavBarItemDTO> dto)
        {
            var result = await _navBarItemService.Create(dto);
            return result;
        }


    }
}
