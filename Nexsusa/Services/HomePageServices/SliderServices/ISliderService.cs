using Data.Dtos.SliderDTOs;
using Services._Base;

namespace Services.HomePageServices.SliderServices
{
    public interface ISliderService
    {
        Task<ResponseResult<List<SliderDTO>>> GetList(int languageId);
        Task<ResponseResult<SliderDTO>> GetById(int id, int languageId);
        Task<ResponseResult<SliderDTO>> Manage(SliderDTO dtos);
        Task<ResponseResult<SliderDTO>> GetFirst();
    }
}
