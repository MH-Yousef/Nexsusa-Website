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
        Task<ResponseResult<List<WhoWeAreDTO>>> GetAll();
        Task<ResponseResult<WhoWeAreDTO>> GetById(int id);
        Task<ResponseResult<WhoWeAreDTO>> Create(WhoWeAreDTO dto);
        Task<ResponseResult<WhoWeAreDTO>> Update(WhoWeAreDTO dto);
        Task<ResponseResult<WhoWeAreDTO>> Delete(int id);

        Task<ResponseResult<List<WhoWeAreItemDTO>>> GetItemsByWhoWeAreId(int whoWeAreId);
    }

}
