using Data.Dtos.WorkShowCaseDTOs;
using Services._Base;

namespace Services.HomePageServices.WorkShowCaseServices
{
    public interface IWorkShowCaseService
    {
        Task<ResponseResult<List<WorkShowCaseDTO>>> GetList(int languageId);
        Task<ResponseResult<WorkShowCaseDTO>> GetById(int id, int languageId);
        Task<ResponseResult<WorkShowCaseDTO>> Manage(WorkShowCaseDTO dtos);
        Task<ResponseResult<WorkShowCaseItemDTO>> ManageSubItem(WorkShowCaseItemDTO subDto);
        Task<ResponseResult<WorkShowCaseDTO>> GetFirst();
    }

}
