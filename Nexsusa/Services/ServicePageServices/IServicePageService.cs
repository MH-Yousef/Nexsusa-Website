using Data.Dtos.ServicePageDTOs;
using Services._Base;

namespace Services.ServicePageServices
{
    public interface IServicePageService
    {
        Task<ResponseResult<ServicePageDTO>> Get(int languageId);
        Task<ResponseResult<ServicePageDTO>> GetById(int id, int LanguageId);
        Task<ResponseResult<List<ServicePageDTO>>> Manage(List<ServicePageDTO> dtos);
    }
}
