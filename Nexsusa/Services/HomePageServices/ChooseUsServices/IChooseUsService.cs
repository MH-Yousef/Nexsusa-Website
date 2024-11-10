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
        Task<ResponseResult<List<ChooseUsDTO>>> Get();

        //====================================================================================================
        Task<ResponseResult<ChooseUsDTO>> GetById(int id);
        //====================================================================================================
        Task<ResponseResult<List<ChooseUsDTO>>> Create(List<ChooseUsDTO> dto);
        //====================================================================================================
        Task<ResponseResult<ChooseUsDTO>> Update(ChooseUsDTO dto);
        //====================================================================================================
        Task<ResponseResult<ChooseUsDTO>> Delete(int id);
        //====================================================================================================
    }
}
