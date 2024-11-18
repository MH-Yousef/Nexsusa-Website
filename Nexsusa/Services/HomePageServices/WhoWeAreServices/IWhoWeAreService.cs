using Data.Dtos.WhoWeAreDTOs;
using Services._Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.HomePageServices.WhoWeAreServices
{
    public interface IWhoWeAreService
    {
        Task<ResponseResult<List<WhoWeAreDTO>>> GetList(int languageId);
        Task<ResponseResult<WhoWeAreDTO>> GetById(int id, int languageId);
        Task<ResponseResult<WhoWeAreDTO>> Manage(WhoWeAreDTO dto);
        Task<ResponseResult<WhoWeAreItemDTO>> ManageSubItem(WhoWeAreItemDTO subDto);
        Task<ResponseResult<WhoWeAreDTO>> GetFirst();
    }
}
