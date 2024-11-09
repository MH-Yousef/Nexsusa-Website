using Core.HomePage.HomePageItems;
using Data.Dtos.ClientSaysDTOs;
using Data.Dtos.LanguageDTOs;
using Services._Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ClientSaysServices
{
    public interface IClientSaysService
    {
        Task<ResponseResult<List<ClientSaysDTO>>> Get();
        //====================================================================================================
        Task<ResponseResult<ClientSaysDTO>> GetById(int id);
        //====================================================================================================
        Task<ResponseResult<ClientSaysDTO>> Create(ClientSaysDTO dto);
        //====================================================================================================
        Task<ResponseResult<ClientSaysDTO>> Update(ClientSaysDTO dto);
        //====================================================================================================
        Task<ResponseResult<ClientSaysDTO>> Delete(int id);
        //====================================================================================================
    }
}
