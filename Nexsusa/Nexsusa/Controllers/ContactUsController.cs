using Data.Dtos.ContactUsDTOs;
using Microsoft.AspNetCore.Mvc;
using Services._Base;
using Services.ContactUsPageServices;

namespace Nexsusa_Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ContactUsController(IContactUsService _contactUsPageService) : ControllerBase
    {
        [HttpGet]
        public async Task<ResponseResult<ContactUsDTO>> Get(int languageId)
        {
            var contactUs = await _contactUsPageService.GetList(languageId);
            return contactUs;
        }
    }
}
