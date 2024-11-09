using Data.Dtos.SliderDTOs;
using Services._Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.SliderServices
{
    public interface ISliderService
    {
        Task<ResponseResult<List<SliderDTO>>> GetAll();
        Task<ResponseResult<SliderDTO>> GetById(int id);
        Task<ResponseResult<SliderDTO>> Create(SliderDTO dto);
        Task<ResponseResult<SliderDTO>> Update(SliderDTO dto);
        Task<ResponseResult<SliderDTO>> Delete(int id);
    }
}
