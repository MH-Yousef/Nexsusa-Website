using Data.Dtos.ImageDTOs;
using Services._Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ImageServices
{
    public interface IImageService
    {
        Task<ResponseResult<List<ImageDTO>>> Get();
        Task<ResponseResult<ImageDTO>> GetById(int id);
        Task<ResponseResult<ImageDTO>> Create(ImageDTO dto);
        Task<ResponseResult<ImageDTO>> Update(ImageDTO dto);
        Task<ResponseResult<ImageDTO>> Delete(int id);
    }
}
