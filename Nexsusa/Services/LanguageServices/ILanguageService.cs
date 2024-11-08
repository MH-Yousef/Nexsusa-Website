using Data.Dtos.LanguageDTOs;
using Services._Base;

namespace Services.LanguageServices
{
    public interface ILanguageService
    {
        Task<ResponseResult<List<LanguageDTO>>> Get();
        //====================================================================================================
        Task<ResponseResult<LanguageDTO>> GetById(int id);
        //====================================================================================================
        Task<ResponseResult<CreateLanguageDTO>> Create(CreateLanguageDTO dto);
        //====================================================================================================
        Task<ResponseResult<UpdateLanguageDTO>> Update(UpdateLanguageDTO dto);
        //====================================================================================================
        Task<ResponseResult<LanguageDTO>> Delete(int id);
        //====================================================================================================
    }
}
