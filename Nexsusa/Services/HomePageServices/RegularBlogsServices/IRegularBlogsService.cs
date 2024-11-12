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
        Task<ResponseResult<List<RegularBlogsDTO>>> Get(int langId);
        Task<ResponseResult<RegularBlogsDTO>> GetById(int id,int langId);
        Task<ResponseResult<List<RegularBlogsDTO>>> Manage(List<RegularBlogsDTO> dtos);
  
    }

}
