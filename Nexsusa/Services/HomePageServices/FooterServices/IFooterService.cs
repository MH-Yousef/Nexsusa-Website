using Data.Dtos.FooterDTOs;
using Services._Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.HomePageServices.FooterServices
{
    public interface IFooterService
    {
        Task<ResponseResult<List<FooterDTO>>> Get();
        Task<ResponseResult<FooterDTO>> GetById(int id);
        Task<ResponseResult<FooterDTO>> Create(FooterDTO dto);
        Task<ResponseResult<FooterDTO>> Update(FooterDTO dto);
        Task<ResponseResult<FooterDTO>> Delete(int id);
    }
}
