using Data.Dtos.ServiceDTOs;
using Data.Dtos.ServicePageDTOs;
using Microsoft.AspNetCore.Mvc;
using Services._Base;
using Services.HomePageServices.ServiceServices;

namespace Nexsusa_Api.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ServicePageController : ControllerBase
    {
        private readonly IServiceService _serviceService;

        public ServicePageController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        [HttpGet]
        public async Task<ResponseResult<ServiceDTO>> Get(int languageId)
        {
            var result = (await _serviceService.GetList(languageId)).Data.FirstOrDefault();
            var response = new ResponseResult<ServiceDTO>
            {
                Data = result,
                IsSuccess = result != null
            };
            return response;
        }
    }
}
