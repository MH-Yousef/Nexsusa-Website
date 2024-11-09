using AutoMapper;
using Core.HomePage.HomePageItems;
using Data.Context;
using Data.Dtos.ImageDTOs;
using Microsoft.EntityFrameworkCore;
using Services._Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Services.ImageServices
{
    public class ImageService : BaseService, IImageService
    {
        public ImageService(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<ResponseResult<List<ImageDTO>>> Get()
        {
            try
            {
                var images = await _dbContext.Images
                    .Where(x => !x.IsDeleted)
                    .ToListAsync();

                if (images == null || !images.Any())
                {
                    return Error<List<ImageDTO>>("Images not found", HttpStatusCode.NotFound);
                }

                var imageDtos = _mapper.Map<List<ImageDTO>>(images);
                return Success(imageDtos);
            }
            catch (Exception ex)
            {
                return Error<List<ImageDTO>>(ex);
            }
        }

        public async Task<ResponseResult<ImageDTO>> GetById(int id)
        {
            try
            {
                var image = await _dbContext.Images
                    .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

                if (image == null)
                {
                    return Error<ImageDTO>("Image not found", HttpStatusCode.NotFound);
                }

                var imageDto = _mapper.Map<ImageDTO>(image);
                return Success(imageDto);
            }
            catch (Exception ex)
            {
                return Error<ImageDTO>(ex);
            }
        }

        public async Task<ResponseResult<ImageDTO>> Create(ImageDTO dto)
        {
            try
            {
                var image = _mapper.Map<Image>(dto);
                await _dbContext.Images.AddAsync(image);
                await _dbContext.SaveChangesAsync();

                dto.Id = image.Id;
                return Success(dto);
            }
            catch (Exception ex)
            {
                return Error<ImageDTO>(ex);
            }
        }

        public async Task<ResponseResult<ImageDTO>> Update(ImageDTO dto)
        {
            try
            {
                var image = await _dbContext.Images
                    .FirstOrDefaultAsync(x => x.Id == dto.Id && !x.IsDeleted);

                if (image == null)
                {
                    return Error<ImageDTO>("Image not found", HttpStatusCode.NotFound);
                }

                image = _mapper.Map(dto, image);
                _dbContext.Update(image);

                await _dbContext.SaveChangesAsync();

                return Success(dto);
            }
            catch (Exception ex)
            {
                return Error<ImageDTO>(ex);
            }
        }

        public async Task<ResponseResult<ImageDTO>> Delete(int id)
        {
            try
            {
                var image = await _dbContext.Images
                    .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

                if (image == null)
                {
                    return Error<ImageDTO>("Image not found", HttpStatusCode.NotFound);
                }

                _dbContext.Images.Remove(image);
                await _dbContext.SaveChangesAsync();

                return Success<ImageDTO>();
            }
            catch (Exception ex)
            {
                return Error<ImageDTO>(ex);
            }
        }
    }

}
