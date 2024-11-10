using AutoMapper;
using Core.HomePage.HomePageItems;
using Data.Context;
using Data.Dtos.SliderDTOs;
using Microsoft.EntityFrameworkCore;
using Services._Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Services.HomePageServices.SliderServices
{
    public class SliderService : BaseService, ISliderService
    {
        public SliderService(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<ResponseResult<List<SliderDTO>>> GetAll()
        {
            try
            {
                var sliders = await _dbContext.Sliders
                    .Where(x => !x.IsDeleted)
                    .ToListAsync();

                if (sliders == null || !sliders.Any())
                {
                    return Error<List<SliderDTO>>("No sliders found", HttpStatusCode.NotFound);
                }

                var sliderDtos = _mapper.Map<List<SliderDTO>>(sliders);
                return Success(sliderDtos);
            }
            catch (Exception ex)
            {
                return Error<List<SliderDTO>>(ex);
            }
        }

        public async Task<ResponseResult<SliderDTO>> GetById(int id)
        {
            try
            {
                var slider = await _dbContext.Sliders
                    .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

                if (slider == null)
                {
                    return Error<SliderDTO>("Slider not found", HttpStatusCode.NotFound);
                }

                var sliderDto = _mapper.Map<SliderDTO>(slider);
                return Success(sliderDto);
            }
            catch (Exception ex)
            {
                return Error<SliderDTO>(ex);
            }
        }

        public async Task<ResponseResult<SliderDTO>> Create(SliderDTO dto)
        {
            try
            {
                var slider = _mapper.Map<Slider>(dto);
                await _dbContext.Sliders.AddAsync(slider);
                await _dbContext.SaveChangesAsync();

                dto.Id = slider.Id;  // Assign the new slider Id to the DTO
                return Success(dto);
            }
            catch (Exception ex)
            {
                return Error<SliderDTO>(ex);
            }
        }

        public async Task<ResponseResult<SliderDTO>> Update(SliderDTO dto)
        {
            try
            {
                var slider = await _dbContext.Sliders
                    .FirstOrDefaultAsync(x => x.Id == dto.Id && !x.IsDeleted);

                if (slider == null)
                {
                    return Error<SliderDTO>("Slider not found", HttpStatusCode.NotFound);
                }

                slider = _mapper.Map(dto, slider);
                _dbContext.Update(slider);

                await _dbContext.SaveChangesAsync();
                return Success(dto);
            }
            catch (Exception ex)
            {
                return Error<SliderDTO>(ex);
            }
        }

        public async Task<ResponseResult<SliderDTO>> Delete(int id)
        {
            try
            {
                var slider = await _dbContext.Sliders
                    .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

                if (slider == null)
                {
                    return Error<SliderDTO>("Slider not found", HttpStatusCode.NotFound);
                }

                _dbContext.Sliders.Remove(slider);
                await _dbContext.SaveChangesAsync();

                return Success<SliderDTO>();
            }
            catch (Exception ex)
            {
                return Error<SliderDTO>(ex);
            }
        }
    }

}
