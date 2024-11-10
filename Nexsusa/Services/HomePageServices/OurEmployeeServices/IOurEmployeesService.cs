using Data.Dtos.OurEmployeeDTOs;
using Services._Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.HomePageServices.OurEmployeeServices
{
    public interface IOurEmployeesService
    {
        Task<ResponseResult<List<OurEmployeesDTO>>> Get();
        Task<ResponseResult<OurEmployeesDTO>> GetById(int id);
        Task<ResponseResult<OurEmployeesDTO>> Create(OurEmployeesDTO dto);
        Task<ResponseResult<OurEmployeesDTO>> Update(OurEmployeesDTO dto);
        Task<ResponseResult<OurEmployeesDTO>> Delete(int id);
    }

}
