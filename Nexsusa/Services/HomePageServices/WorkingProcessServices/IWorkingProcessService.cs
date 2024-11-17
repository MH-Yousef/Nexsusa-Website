using Data.Dtos.WorkingProcessDTOs;
using Services._Base;

namespace Services.HomePageServices.WorkingProcessServices
{
    public interface IWorkingProcessService
    {
        Task<ResponseResult<List<WorkingProcessDTO>>> GetList(int languageId);
        Task<ResponseResult<WorkingProcessDTO>> GetById(int id, int languageId);
        Task<ResponseResult<WorkingProcessDTO>> Manage(WorkingProcessDTO dtos);
        Task<ResponseResult<WorkingProcessItemDTO>> ManageSubItem(WorkingProcessItemDTO subDto);
        Task<ResponseResult<WorkingProcessDTO>> GetFirst();
    }


}
