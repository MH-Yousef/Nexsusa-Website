using Data.Dtos.WorkingProcessDTOs;
using Services._Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.HomePageServices.WorkingProcessItemServices
{
    public interface IWorkingProcessItemService
    {
        Task<ResponseResult<List<WorkingProcessItemDTO>>> GetAll();
        Task<ResponseResult<WorkingProcessItemDTO>> GetById(int id);
        Task<ResponseResult<WorkingProcessItemDTO>> Create(WorkingProcessItemDTO dto);
        Task<ResponseResult<WorkingProcessItemDTO>> Update(WorkingProcessItemDTO dto);
        Task<ResponseResult<WorkingProcessItemDTO>> Delete(int id);
    }

}
