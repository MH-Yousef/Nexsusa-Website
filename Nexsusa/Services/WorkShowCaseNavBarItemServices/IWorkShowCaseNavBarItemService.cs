using Data.Dtos.WorkShowCaseDTOs;
using Services._Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.WorkShowCaseNavBarItemServices
{
    public interface IWorkShowCaseNavBarItemService
    {
        Task<ResponseResult<List<WorkShowCaseNavBarItemDTO>>> GetAll();
        Task<ResponseResult<WorkShowCaseNavBarItemDTO>> GetById(int id);
        Task<ResponseResult<WorkShowCaseNavBarItemDTO>> Create(WorkShowCaseNavBarItemDTO dto);
        Task<ResponseResult<WorkShowCaseNavBarItemDTO>> Update(WorkShowCaseNavBarItemDTO dto);
        Task<ResponseResult<WorkShowCaseNavBarItemDTO>> Delete(int id);
    }

}
