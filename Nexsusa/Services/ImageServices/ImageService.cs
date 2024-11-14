using AutoMapper;
using Data.Context;
using Microsoft.AspNetCore.Http;
using Services._Base;

namespace Services.ImageServices
{
    public class ImageService : BaseService, IImageService
    {
        public ImageService(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<string> UploadImage(IFormFile file)
        {
            try
            {
                var directory = Directory.GetCurrentDirectory();
                var path = Path.Combine(directory,"wwwroot", "Images");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                if (file != null)
                {
                    var extension = Path.GetExtension(file.FileName);
                    var fileName = Guid.NewGuid() + extension;
                    var saveLocation = Path.Combine(path, fileName);
                    var stream = new FileStream(saveLocation, FileMode.Create);
                    await file.CopyToAsync(stream);

                    return fileName;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
