using Data.Dtos.ServiceDTOs;
using Services._Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.HomePageServices.ServiceServices
{
    public interface IServiceService
    {
        Task<ResponseResult<List<ServiceDTO>>> GetList(int langId);
        Task<ResponseResult<ServiceDTO>> GetById(int id,int langId);
        Task<ResponseResult<List<ServiceDTO>>> Manage(List<ServiceDTO> dtos);
        
    }
}
