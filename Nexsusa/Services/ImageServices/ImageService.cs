using AutoMapper;
using Data.Context;
using Microsoft.AspNetCore.Http;
using Services._Base;

namespace Services.ImageServices
{
    public class ImageService(AppDbContext dbContext, IMapper mapper) : BaseService(dbContext, mapper), IImageService
    {
        public async Task<string> UploadImage(IFormFile file)
        {
            try
            {
                var directory = Directory.GetCurrentDirectory();
                var path = Path.Combine(directory, "wwwroot", "Images");

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                if (file != null && file.Length > 0)
                {
                    var extension = Path.GetExtension(file.FileName);
                    var fileName = Guid.NewGuid() + extension;
                    var saveLocation = Path.Combine(path, fileName);

                    using (var stream = new FileStream(saveLocation, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    return fileName;
                }

                return "No file uploaded.";
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

    }
}
