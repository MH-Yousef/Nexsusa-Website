using Core.HomePage.HomePageItems;
using Data.Dtos.FooterDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services._Base;
using Services.HomePageServices.FooterServices;

namespace Nexsusa_Api.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class FooterController(IFooterService _footerService) : ControllerBase
    {
        [HttpGet]
        public async Task<ResponseResult<FooterDTO>> GetFooter(int languageId)
        {
            var result = await _footerService.Get(languageId);
            return result;
        }
    }
}
