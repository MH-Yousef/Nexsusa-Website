using Data.Dtos.ContactUsDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services._Base;
using Services.ContactUsPageServices;

namespace Nexsusa_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactUsController : ControllerBase
    {
        private readonly IContactUsPageService _contactUsPageService;

        public ContactUsController(IContactUsPageService contactUsPageService)
        {
            _contactUsPageService = contactUsPageService;
        }
        [HttpGet]
        public async Task<ResponseResult<ContactUsPageDTO>> Get(int langId)
        {
           var contactUs= await _contactUsPageService.Get(langId);
            return contactUs;
        }
        [HttpPost]
        public async Task<ResponseResult<ContactUsPageDTO>> Manage(ContactUsPageDTO contactUsPageDTO)
        {
            var result= await _contactUsPageService.Manage(contactUsPageDTO);
            return result;
        }
    }
}
