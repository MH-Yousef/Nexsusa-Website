using Data.Dtos.ContactUsDTOs;
using Services._Base;

namespace Services.ContactUsPageServices
{
    public interface IContactUsService
    {
        Task<ResponseResult<ContactUsDTO>> GetById(int id, int languageId);
        Task<ResponseResult<ContactUsDTO>> GetFirst();
        Task<ResponseResult<ContactUsDTO>> GetList(int languageId);
        Task<ResponseResult<ContactUsDTO>> Manage(ContactUsDTO dto);
    }
}
