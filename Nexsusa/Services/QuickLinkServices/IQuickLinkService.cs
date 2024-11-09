using Data.Dtos.QuickLinkDTOs;
using Services._Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.QuickLinkServices
{
    public interface IQuickLinkService
    {
        Task<ResponseResult<List<QuickLinkDTO>>> Get();
        Task<ResponseResult<QuickLinkDTO>> GetById(int id);
        Task<ResponseResult<QuickLinkDTO>> Create(QuickLinkDTO dto);
        Task<ResponseResult<QuickLinkDTO>> Update(QuickLinkDTO dto);
        Task<ResponseResult<QuickLinkDTO>> Delete(int id);
    }
}
