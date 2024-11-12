using Data.Dtos.OurCompanyDTOs;
using Services._Base;

namespace Services.HomePageServices.OurCompanyServices
{
    public interface IOurCompanyService
    {
        Task<ResponseResult<List<OurCompanyDTO>>> GetList(int LanguageId);
        Task<ResponseResult<OurCompanyDTO>> GetById(int id, int LanguageId);
        Task<ResponseResult<List<OurCompanyDTO>>> Manage(List<OurCompanyDTO> dtos);
        Task<ResponseResult<OurCompanyDTO>> Delete(int id);
    }

}
