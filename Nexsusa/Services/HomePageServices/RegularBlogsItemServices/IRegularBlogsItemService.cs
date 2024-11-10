using Data.Dtos.RegularBlogsDTOs;
using Services._Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.HomePageServices.RegularBlogsItemServices
{
    public interface IRegularBlogsItemService
    {
        Task<ResponseResult<List<RegularBlogsItemDTO>>> Get();
        Task<ResponseResult<RegularBlogsItemDTO>> GetById(int id);
        Task<ResponseResult<RegularBlogsItemDTO>> Create(RegularBlogsItemDTO dto);
        Task<ResponseResult<RegularBlogsItemDTO>> Update(RegularBlogsItemDTO dto);
        Task<ResponseResult<RegularBlogsItemDTO>> Delete(int id);
    }

}
