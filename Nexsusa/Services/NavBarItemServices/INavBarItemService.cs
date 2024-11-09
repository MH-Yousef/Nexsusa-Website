using Data.Dtos.NavBarDTOs;
using Services._Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.NavBarServices
{
    public interface INavBarItemService
    {
        Task<ResponseResult<List<NavBarItemDTO>>> Get();
        Task<ResponseResult<NavBarItemDTO>> GetById(int id);
        Task<ResponseResult<NavBarItemDTO>> Create(NavBarItemDTO dto);
        Task<ResponseResult<NavBarItemDTO>> Update(NavBarItemDTO dto);
        Task<ResponseResult<NavBarItemDTO>> Delete(int id);
    }
}
