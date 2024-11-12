using Data.Dtos.OurEmployeeDTOs;
using Services._Base;

namespace Services.HomePageServices.OurEmployeeServices
{
    public interface IOurEmployeesService
    {
        Task<ResponseResult<List<OurEmployeesDTO>>> GetList(int LanguageId);
        Task<ResponseResult<OurEmployeesDTO>> GetById(int id, int LanguageId);
        Task<ResponseResult<List<OurEmployeesDTO>>> Manage(List<OurEmployeesDTO> dtos);
        Task<ResponseResult<OurEmployeesDTO>> Delete(int id);
    }

}
