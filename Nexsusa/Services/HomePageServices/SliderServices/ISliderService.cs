using Data.Dtos.SliderDTOs;
using Services._Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.HomePageServices.SliderServices
{
    public interface ISliderService
    {
        Task<ResponseResult<List<SliderDTO>>> GetList(int languageId);
        Task<ResponseResult<SliderDTO>> GetById(int id, int languageId);
        Task<ResponseResult<List<SliderDTO>>> Manage(List<SliderDTO> dtos);
    }
}
