using Data.Dtos;
using Services._Base;

namespace Services.LanguageServices
{
    public interface ILanguageService
    {
        Task<ResponseResult<List<LanguageDTO>>> Get();
        //====================================================================================================
        Task<ResponseResult<LanguageDTO>> GetById(int id);
        //====================================================================================================
        Task<ResponseResult<LanguageDTO>> Create(LanguageDTO dto);
        //====================================================================================================
        Task<ResponseResult<LanguageDTO>> Update(LanguageDTO dto);
        //====================================================================================================
        Task<ResponseResult<LanguageDTO>> Delete(int id);
        //====================================================================================================
    }
}
