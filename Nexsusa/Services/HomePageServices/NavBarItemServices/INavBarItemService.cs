using Core.HomePage.HomePageItems;
using Data.Dtos.NavBarDTOs;
using Services._Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.HomePageServices.NavBarItemServices
{
    public interface INavBarItemService
    {
        Task<ResponseResult<List<NavBarItemDTO>>> GetList(int LanguageId);
        Task<ResponseResult<NavBarItemDTO>> GetById(int id, int languageId);
        Task<ResponseResult<List<NavBarItemDTO>>> Manage(List<NavBarItemDTO> dto);
        Task<ResponseResult<NavBarItemDTO>> Delete(int id);
    }
}
