using Data.Dtos.WorkingProcessDTOs;
using Services._Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.HomePageServices.WorkingProcessServices
{
    public interface IWorkingProcessService
    {
        Task<ResponseResult<List<WorkingProcessDTO>>> GetList(int languageId);

        // Get WorkingProcess by its Id with translations based on language
        Task<ResponseResult<WorkingProcessDTO>> GetById(int id, int languageId);

        // Create or Update WorkingProcess and its related WorkingProcessItems with translations
        Task<ResponseResult<List<WorkingProcessDTO>>> Manage(List<WorkingProcessDTO> dtos);

        // Delete WorkingProcess by Id (soft delete)
      
    }


}
