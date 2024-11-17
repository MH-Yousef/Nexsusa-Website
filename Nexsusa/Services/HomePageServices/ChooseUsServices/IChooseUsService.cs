using Data.Dtos.ChooseUsDTOs;
using Data.Dtos.QuestionDTOs;
using Services._Base;

namespace Services.HomePageServices.ChooseUsServices
{
    public interface IChooseUsService
    {
        Task<ResponseResult<List<ChooseUsDTO>>> GetList(int LanguageId);
        //====================================================================================================
        Task<ResponseResult<ChooseUsDTO>> GetById(int id, int LanguageId);
        //====================================================================================================
        Task<ResponseResult<ChooseUsDTO>> Manage(ChooseUsDTO dto);
        //====================================================================================================
        Task<ResponseResult<ChooseUsDTO>> Delete(int id);
        Task<ResponseResult<ChooseUsDTO>> GetFirst();
        Task<ResponseResult<QuestionDTO>> ManageSubItem(QuestionDTO subDto);
        //====================================================================================================
    }
}
