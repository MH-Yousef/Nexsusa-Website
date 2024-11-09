using Data.Dtos.WhoWeAreDTOs;
using Services._Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.WhoWeAreItemServices
{
    public interface IWhoWeAreItemService
    {
        Task<ResponseResult<List<WhoWeAreItemDTO>>> GetAll();
        Task<ResponseResult<WhoWeAreItemDTO>> GetById(int id);
        Task<ResponseResult<WhoWeAreItemDTO>> Create(WhoWeAreItemDTO dto);
        Task<ResponseResult<WhoWeAreItemDTO>> Update(WhoWeAreItemDTO dto);
        Task<ResponseResult<WhoWeAreItemDTO>> Delete(int id);
    }


}
