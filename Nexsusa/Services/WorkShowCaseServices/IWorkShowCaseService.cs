using Data.Dtos.WorkShowCaseDTOs;
using Services._Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.WorkShowCaseServices
{
    public interface IWorkShowCaseService
    {
        Task<ResponseResult<List<WorkShowCaseDTO>>> GetAll();
        Task<ResponseResult<WorkShowCaseDTO>> GetById(int id);
        Task<ResponseResult<WorkShowCaseDTO>> Create(WorkShowCaseDTO dto);
        Task<ResponseResult<WorkShowCaseDTO>> Update(WorkShowCaseDTO dto);
        Task<ResponseResult<WorkShowCaseDTO>> Delete(int id);
    }

}
