using Core.Domains.Languages;
using Data.Dtos.LanguageDTOs;
using Services._Base;

namespace Services.LanguageServices
{
    public interface ILanguageService
    {
        Task<IEnumerable<Language>> Get();
        //====================================================================================================
        Task<ResponseResult<LanguageDTO>> GetById(int id);
        //====================================================================================================
        Task<ResponseResult<Language>> Create(Language dto);
        //====================================================================================================
        Task<ResponseResult<LanguageDTO>> Update(LanguageDTO dto);
        //====================================================================================================
        Task<ResponseResult<LanguageDTO>> Delete(int id);
        //====================================================================================================
    }
}
