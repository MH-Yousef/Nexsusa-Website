using Data.Dtos.WorkShowCaseDTOs;
using Services._Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.HomePageServices.WorkShowCaseServices
{
    public interface IWorkShowCaseService
    {
        Task<ResponseResult<List<WorkShowCaseDTO>>> GetList(int languageId);

        // Get a single WorkShowCase by Id with translations based on language
        Task<ResponseResult<WorkShowCaseDTO>> GetById(int id, int languageId);

        // Create or Update WorkShowCase and related NavBars with translations
        Task<ResponseResult<List<WorkShowCaseDTO>>> Manage(List<WorkShowCaseDTO> dtos);

    }

}
