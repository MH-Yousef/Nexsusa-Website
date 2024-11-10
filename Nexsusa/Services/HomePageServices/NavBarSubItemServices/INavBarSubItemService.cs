using Data.Dtos.NavBarDTOs;
using Services._Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.HomePageServices.NavBarSubItemServices
{
    public interface INavBarItemSubItemService
    {
        Task<ResponseResult<List<NavBarItemSubItemDTO>>> Get();
        Task<ResponseResult<NavBarItemSubItemDTO>> GetById(int id);
        Task<ResponseResult<NavBarItemSubItemDTO>> Create(NavBarItemSubItemDTO dto);
        Task<ResponseResult<NavBarItemSubItemDTO>> Update(NavBarItemSubItemDTO dto);
        Task<ResponseResult<NavBarItemSubItemDTO>> Delete(int id);
    }

}
