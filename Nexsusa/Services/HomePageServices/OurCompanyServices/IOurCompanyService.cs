using Data.Dtos.OurCompanyDTOs;
using Services._Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.HomePageServices.OurCompanyServices
{
    public interface IOurCompanyService
    {
        Task<ResponseResult<List<OurCompanyDTO>>> Get();
        Task<ResponseResult<OurCompanyDTO>> GetById(int id);
        Task<ResponseResult<OurCompanyDTO>> Create(OurCompanyDTO dto);
        Task<ResponseResult<OurCompanyDTO>> Update(OurCompanyDTO dto);
        Task<ResponseResult<OurCompanyDTO>> Delete(int id);
    }

}
