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
        Task<ResponseResult<FooterDTO>> Get(int LanguageId);
        Task<ResponseResult<FooterDTO>> GetFirst();
        Task<ResponseResult<FooterDTO>> Manage(FooterDTO dto);
    }
}
