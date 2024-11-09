using Data.Dtos.OurEmployeeDTOs;
using Services._Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.OurEmployeesItemServices
{
    public interface IOurEmployeesItemService
    {
        Task<ResponseResult<List<OurEmployeesItemDTO>>> Get();
        Task<ResponseResult<OurEmployeesItemDTO>> GetById(int id);
        Task<ResponseResult<OurEmployeesItemDTO>> Create(OurEmployeesItemDTO dto);
        Task<ResponseResult<OurEmployeesItemDTO>> Update(OurEmployeesItemDTO dto);
        Task<ResponseResult<OurEmployeesItemDTO>> Delete(int id);
    }

}
