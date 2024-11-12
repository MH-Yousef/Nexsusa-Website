using Core.HomePage;
using Data.Dtos.HomePageDTOs;
using Services._Base;

namespace Services.HomePageServices
{
    public interface IHomePageServices
    {
        Task<ResponseResult<HomePageDTO>> GetHomePage(int languageId);
        Task<ResponseResult<HomePageInfoDTO>> GetHomePageInfo();
        Task<ResponseResult<HomePageInfo>> Create(HomePageInfo model);
        Task<ResponseResult<HomePageInfo>> Update(HomePageInfo model);
    }
}
