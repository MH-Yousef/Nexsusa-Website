using Data.Dtos.ServiceDTOs;
using Services._Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.HomePageServices.ServiceItemServices
{
    public interface IServiceItemService
    {
        Task<ResponseResult<List<ServiceItemDTO>>> GetAll();
        Task<ResponseResult<ServiceItemDTO>> GetById(int id);
        Task<ResponseResult<ServiceItemDTO>> Create(ServiceItemDTO dto);
        Task<ResponseResult<ServiceItemDTO>> Update(ServiceItemDTO dto);
        Task<ResponseResult<ServiceItemDTO>> Delete(int id);
    }
}
