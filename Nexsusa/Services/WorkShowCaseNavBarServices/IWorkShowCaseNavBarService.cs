using Data.Dtos.WorkShowCaseDTOs;
using Services._Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.WorkShowCaseNavBarServices
{
    public interface IWorkShowCaseNavBarService
    {
        Task<ResponseResult<List<WorkShowCaseNavBarDTO>>> GetAll();
        Task<ResponseResult<WorkShowCaseNavBarDTO>> GetById(int id);
        Task<ResponseResult<WorkShowCaseNavBarDTO>> Create(WorkShowCaseNavBarDTO dto);
        Task<ResponseResult<WorkShowCaseNavBarDTO>> Update(WorkShowCaseNavBarDTO dto);
        Task<ResponseResult<WorkShowCaseNavBarDTO>> Delete(int id);
    }

}
