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
        Task<ResponseResult<List<FooterDTO>>> GetList(int LanguageId);
        Task<ResponseResult<FooterDTO>> GetById(int id, int LanguageId);
        Task<ResponseResult<List<FooterDTO>>> Manage(List<FooterDTO> dtos);
        Task<ResponseResult<FooterDTO>> Delete(int id);
    }
}
