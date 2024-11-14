using Data.Dtos.AboutDTOs;
using Services._Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.AboutServices
{
    public interface IAboutService
    {
        Task<ResponseResult<AboutDTO>> Get(int languageId);
        Task<ResponseResult<List<AboutDTO>>> Manage(List<AboutDTO> dtos);
    }
}
