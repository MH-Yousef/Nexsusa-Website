using Data.Dtos.ChooseUsDTOs;
using Data.Dtos.LanguageDTOs;
using Services._Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ChooseUsServices
{
    public interface IChooseUsService
    {
        Task<ResponseResult<List<ChooseUsDTO>>> Get();
       
        //====================================================================================================
        Task<ResponseResult<ChooseUsDTO>> GetById(int id);
        //====================================================================================================
        Task<ResponseResult<ChooseUsDTO>> Create(ChooseUsDTO dto);
        //====================================================================================================
        Task<ResponseResult<ChooseUsDTO>> Update(ChooseUsDTO dto);
        //====================================================================================================
        Task<ResponseResult<ChooseUsDTO>> Delete(int id);
        //====================================================================================================
    }
}
