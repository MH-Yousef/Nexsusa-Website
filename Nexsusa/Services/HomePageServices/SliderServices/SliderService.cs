using AutoMapper;
using Core.Domains.Enums;
using Core.HomePage.HomePageItems;
using Data.Context;
using Data.Dtos.OurCompanyDTOs;
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
        private readonly Services._GenericServices.GenericService<Slider> _genericService;
        public SliderService(AppDbContext dbContext, IMapper mapper, _GenericServices.GenericService<Slider> genericService = null) : base(dbContext, mapper)
        {
            _genericService = genericService;
        }

        public async Task<ResponseResult<List<SliderDTO>>> GetList(int languageId)
        {
            try
            {
                // Retrieve sliders with translations
                var sliders = await _genericService.GetListAsync(languageId, StringResourceEnums.Slider);

                var sliderDtos = _mapper.Map<List<Slider>, List<SliderDTO>>(sliders.Data);

                // Apply translations for each slider item
                foreach (var slider in sliderDtos)
                {
                    slider.LangId = languageId;
                    slider.Title = _genericService.ApplyTranslations<SliderDTO>(slider, languageId, slider.Id, StringResourceEnums.Slider).Title;
                    slider.Description = _genericService.ApplyTranslations<SliderDTO>(slider, languageId, slider.Id, StringResourceEnums.Slider).Description;
                }

                return Success(sliderDtos);
            }
            catch (Exception ex)
            {
                return Error<List<SliderDTO>>(ex);
            }
        }

        public async Task<ResponseResult<SliderDTO>> GetById(int id, int languageId)
        {
            try
            {
                var slider = await _genericService.GetByIdAsync(id, languageId, StringResourceEnums.Slider);

                var sliderDto = _mapper.Map<Slider, SliderDTO>(slider);

                // Apply translations for the slider
                sliderDto.LangId = languageId;
                sliderDto.Title = _genericService.ApplyTranslations<SliderDTO>(sliderDto, languageId, sliderDto.Id, StringResourceEnums.Slider).Title;
                sliderDto.Description = _genericService.ApplyTranslations<SliderDTO>(sliderDto, languageId, sliderDto.Id, StringResourceEnums.Slider).Description;

                return Success(sliderDto);
            }
            catch (Exception ex)
            {
                return Error<SliderDTO>(ex);
            }
        }

        public async Task<ResponseResult<SliderDTO>> Manage(SliderDTO dto)
        {
            try
            {
                var defaultLanguage = await _dbContext.Languages.FirstAsync(x => x.IsDefault);
                bool isDefualtExists = await _dbContext.Sliders.AnyAsync();
                bool IsDefultModel = dto.LangId == defaultLanguage.Id;
                bool IsTranslateionExixts = await _dbContext.StringResources.AnyAsync(x => x.ResourceId == dto.Id && x.GroupKey == StringResourceEnums.Slider && x.LanguageId == dto.LangId);   
                var mainItem = _mapper.Map<SliderDTO, Slider>(dto);

                if (IsDefultModel)
                {
                    if (dto.Id > 0)
                    {
                        await _genericService.UpdateAsync(mainItem);
                    }
                    else
                    {
                        await _genericService.CreateAsync(mainItem);
                    }
                    var translations = new List<(string ColumnName, string ColumnValue)>
                            {
                                ("Title", dto.Title),
                                ("Description", dto.Description)
                            };

                    if (dto.Id > 0)
                    {
                        await _genericService.UpdateTranslationsAsync(StringResourceEnums.Slider, translations, mainItem.Id, dto.LangId);
                    }
                    else
                    {
                        await _genericService.AddTranslationsAsync(StringResourceEnums.Slider, translations, mainItem.Id, dto.LangId);
                    }
                }
                else
                {
                    if (isDefualtExists)
                    {
                        var translations = new List<(string ColumnName, string ColumnValue)>
                            {
                                ("Title", dto.Title),
                                ("Description", dto.Description)
                            };

                        if (dto.Id > 0 && IsTranslateionExixts)
                        {
                            await _genericService.UpdateTranslationsAsync(StringResourceEnums.Slider, translations, dto.Id, dto.LangId);
                        }
                        else
                        {
                            await _genericService.AddTranslationsAsync(StringResourceEnums.Slider, translations, dto.Id, dto.LangId);
                        }


                    }
                    else
                    {
                        return Error<SliderDTO>("Only default language can be managed first");
                    }
                }
                return Success(dto);
            }
            catch (Exception ex)
            {
                return Error<SliderDTO>(ex);
            }
        }

        public async Task<ResponseResult<SliderDTO>> GetFirst()
        {
            try
            {
                var result = await _dbContext.Sliders.FirstOrDefaultAsync();
                var sliderDto = _mapper.Map<Slider, SliderDTO>(result);
                return Success(sliderDto);

            }
            catch (Exception ex)
            {
                return Error<SliderDTO>(ex);
            }
        }
    }

}
