using Data.Dtos.ImageDTOs;
using Microsoft.AspNetCore.Http;
using Services._Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ImageServices
{
    public interface IImageService
    {
        Task<string> UploadImage(IFormFile file);
    }
}
