using Data.Dtos.ClientSaysItemDTOs;
using Data.Dtos.LanguageDTOs;
using Services._Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ClientSaysItemServices
{
    public interface IClientSaysItemService
    {
        Task<ResponseResult<List<ClientSaysItemDTO>>> Get();
        //====================================================================================================
        Task<ResponseResult<ClientSaysItemDTO>> GetById(int id);
        //====================================================================================================
        Task<ResponseResult<ClientSaysItemDTO>> Create(ClientSaysItemDTO dto);
        //====================================================================================================
        Task<ResponseResult<ClientSaysItemDTO>> Update(ClientSaysItemDTO dto);
        //====================================================================================================
        Task<ResponseResult<ClientSaysItemDTO>> Delete(int id);
        //====================================================================================================
    }
}
