using Data.Dtos.RegularBlogsDTOs;
using Services._Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.HomePageServices.RegularBlogsServices
{
    public interface IRegularBlogsService
    {
        Task<ResponseResult<List<RegularBlogsDTO>>> Get();
        Task<ResponseResult<RegularBlogsDTO>> GetById(int id);
        Task<ResponseResult<RegularBlogsDTO>> Create(RegularBlogsDTO dto);
        Task<ResponseResult<RegularBlogsDTO>> Update(RegularBlogsDTO dto);
        Task<ResponseResult<RegularBlogsDTO>> Delete(int id);
    }

}
