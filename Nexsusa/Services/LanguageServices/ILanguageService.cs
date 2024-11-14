using Core.Domains.Languages;
using Data.Dtos.LanguageDTOs;
using Services._Base;

namespace Services.LanguageServices
{
    public interface ILanguageService
    {
        Task<ResponseResult<List<Language>>> Get(bool IsActiveDisable = false);
        //====================================================================================================
        Task<ResponseResult<LanguageDTO>> GetById(int id);
        //====================================================================================================
        Task<ResponseResult<Language>> Create(Language dto);
        //====================================================================================================
        Task<ResponseResult<LanguageDTO>> Update(LanguageDTO dto);
        //====================================================================================================
        Task<ResponseResult<LanguageDTO>> Delete(int id);
        //====================================================================================================
        void SetLanguage(string culture);
        //====================================================================================================
    }
}
