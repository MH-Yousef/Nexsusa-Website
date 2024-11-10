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
        Task<ResponseResult<List<ServiceDTO>>> GetAll();
        Task<ResponseResult<ServiceDTO>> GetById(int id);
        Task<ResponseResult<ServiceDTO>> Create(ServiceDTO dto);
        Task<ResponseResult<ServiceDTO>> Update(ServiceDTO dto);
        Task<ResponseResult<ServiceDTO>> Delete(int id);
    }
}
