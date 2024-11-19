using Core.HomePage.HomePageItems;
using Data.Dtos.ClientSaysDTOs;
using Data.Dtos.ContactUsDTOs;
using Services._Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.HomePageServices.ContactUsServices
{
    public interface IContactUsService
    {
        Task<ResponseResult<List<ContactUsDTO>>> GetList(int langId);
        Task<ResponseResult<ContactUsDTO>> GetById(int id,int langId);
        Task<ResponseResult<List<ContactUsDTO>>> Manage(List<ContactUsDTO> dto);
        //====================================================================================================
        Task<ResponseResult<ContactUsDTO>> Delete(int id);
      
        //===============================================================
    }
}
