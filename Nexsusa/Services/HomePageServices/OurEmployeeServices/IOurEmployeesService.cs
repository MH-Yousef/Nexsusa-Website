using Data.Dtos.OurEmployeeDTOs;
using Services._Base;

namespace Services.HomePageServices.OurEmployeeServices
{
    public interface IOurEmployeesService
    {
        Task<ResponseResult<List<OurEmployeesDTO>>> GetList(int LanguageId);
        Task<ResponseResult<OurEmployeesDTO>> GetById(int id, int LanguageId);
        Task<ResponseResult<OurEmployeesDTO>> Manage(OurEmployeesDTO dto);
        Task<ResponseResult<OurEmployeesItemDTO>> ManageSubItem(OurEmployeesItemDTO subDto);
        Task<ResponseResult<OurEmployeesDTO>> GetFirst();
    }

}
