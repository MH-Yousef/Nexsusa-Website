using Data.Dtos.ChooseUsDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services._Base;
using Services.HomePageServices.ChooseUsServices;

namespace Nexsusa_Api.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ChooseUsController(IChooseUsService chooseUsService) : ControllerBase
    {
        [HttpGet]
        public async Task<ResponseResult<List<ChooseUsDTO>>> Get(int languageId)
        {
            var result = await chooseUsService.GetList(languageId);
            return result;
        }
        [HttpGet("{id}")]
        public async Task<ResponseResult<ChooseUsDTO>> GetById(int id, int languageId)
        {
            var result = await chooseUsService.GetById(id, languageId);
            return result;
        }
        [HttpPost]
        public async Task<ResponseResult<List<ChooseUsDTO>>> Manage(List<ChooseUsDTO> dto)
        {
            var result = await chooseUsService.Manage(dto);
            return result;
        }
    }
}
