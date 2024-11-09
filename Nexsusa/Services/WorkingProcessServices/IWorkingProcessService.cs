using Data.Dtos.WorkingProcessDTOs;
using Services._Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.WorkingProcessServices
{
    public interface IWorkingProcessService
    {
        Task<ResponseResult<List<WorkingProcessDTO>>> GetAll();
        Task<ResponseResult<WorkingProcessDTO>> GetById(int id);
        Task<ResponseResult<WorkingProcessDTO>> Create(WorkingProcessDTO dto);
        Task<ResponseResult<WorkingProcessDTO>> Update(WorkingProcessDTO dto);
        Task<ResponseResult<WorkingProcessDTO>> Delete(int id);
    }


}
