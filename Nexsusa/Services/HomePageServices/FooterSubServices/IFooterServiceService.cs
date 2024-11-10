using Data.Dtos.FooterServiceDTOs;
using Services._Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.HomePageServices.FooterSubServices
{
    public interface IFooterServiceService
    {
        Task<ResponseResult<List<FooterServiceDTO>>> Get();
        Task<ResponseResult<FooterServiceDTO>> GetById(int id);
        Task<ResponseResult<FooterServiceDTO>> Create(FooterServiceDTO dto);
        Task<ResponseResult<FooterServiceDTO>> Update(FooterServiceDTO dto);
        Task<ResponseResult<FooterServiceDTO>> Delete(int id);
    }
}
