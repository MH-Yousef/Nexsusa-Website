using Data.Dtos.ChooseUsDTOs;
using Data.Dtos.LanguageDTOs;
using Services._Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.HomePageServices.ChooseUsServices
{
    public interface IChooseUsService
    {
        Task<ResponseResult<List<ChooseUsDTO>>> GetList(int LanguageId);
        //====================================================================================================
        Task<ResponseResult<ChooseUsDTO>> GetById(int id, int LanguageId);
        //====================================================================================================
        Task<ResponseResult<List<ChooseUsDTO>>> Manage(List<ChooseUsDTO> dto);
        //====================================================================================================
        Task<ResponseResult<ChooseUsDTO>> Delete(int id);
        //====================================================================================================
    }
}
