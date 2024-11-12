using Core.HomePage.HomePageItems;
using Data.Dtos.ClientSaysDTOs;
using Data.Dtos.LanguageDTOs;
using Services._Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.HomePageServices.ClientSaysServices
{
    public interface IClientSaysService
    {
        Task<ResponseResult<List<ClientSaysDTO>>> GetList(int LanguageId);
        //====================================================================================================
        Task<ResponseResult<ClientSaysDTO>> GetById(int id, int LanguageId);
        //====================================================================================================
        Task<ResponseResult<List<ClientSaysDTO>>> Manage(List<ClientSaysDTO> dto);
        //====================================================================================================
        Task<ResponseResult<ClientSaysDTO>> Delete(int id);
        //====================================================================================================
    }
}
