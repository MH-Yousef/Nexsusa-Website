using Data.Dtos.WorkShowCaseDTOs;
using Services._Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.WorkShowCaseServiceServices
{
    public interface IWorkShowCaseService
    {
        Task<ResponseResult<List<WorkShowCaseServiceDTO>>> GetAll();
        Task<ResponseResult<WorkShowCaseServiceDTO>> GetById(int id);
        Task<ResponseResult<WorkShowCaseServiceDTO>> Create(WorkShowCaseServiceDTO dto);
        Task<ResponseResult<WorkShowCaseServiceDTO>> Update(WorkShowCaseServiceDTO dto);
        Task<ResponseResult<WorkShowCaseServiceDTO>> Delete(int id);
    }

}
