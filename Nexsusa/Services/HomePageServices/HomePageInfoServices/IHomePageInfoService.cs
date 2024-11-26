using Data.Dtos.HomePageDTOs;
using Services._Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.HomePageServices.HomePageInfoServices
{
    public interface IHomePageInfoService
    {
        Task<ResponseResult<HomePageInfoDTO>> GetHomePageInfoAsync(int langId);
        Task<ResponseResult<HomePageInfoDTO>> ManageAsync(HomePageInfoDTO dto);
    }
}
