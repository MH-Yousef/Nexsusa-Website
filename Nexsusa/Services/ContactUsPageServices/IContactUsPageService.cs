using Data.Dtos.ContactUsDTOs;
using Services._Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ContactUsPageServices
{
    public interface IContactUsPageService
    {
        Task<ResponseResult<ContactUsPageDTO>> Get(int langId);
       
        Task<ResponseResult<ContactUsPageDTO>> Manage(ContactUsPageDTO dto);
        Task<ResponseResult<ContactUsPageDTO>> GetFirst();
        Task<ResponseResult<ContactUsPageDTO>> GetById(int id,int langId);
        Task<ResponseResult<ContactUsItemDTO>> ManageSubItem(ContactUsItemDTO subDto);
        Task<ResponseResult<ContactUsItemDTO>> GetSubItem(int id, int contactUsPageId, int languageId);
    }
}
