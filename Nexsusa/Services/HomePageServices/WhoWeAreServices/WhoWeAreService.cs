using AutoMapper;
using Core.Domains.Enums;
using Core.HomePage.HomePageItems;
using Data.Context;
using Data.Dtos.WhoWeAreDTOs;
using Data.Dtos.WhoWeAreDTOs;
using Microsoft.EntityFrameworkCore;
using Services._Base;
using Services._GenericServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Services.HomePageServices.WhoWeAreServices
{
    public class WhoWeAreService : BaseService, IWhoWeAreService
    {
        private readonly GenericService<WhoWeAre> _genericService;
        public WhoWeAreService(AppDbContext dbContext, IMapper mapper, GenericService<WhoWeAre> genericService) : base(dbContext, mapper)
        {
            _genericService = genericService;
        }

        public async Task<ResponseResult<List<WhoWeAreDTO>>> GetList(int languageId)
        {
            try
            {
                // Retrieve WhoWeAre data with translations
                var whoWeAreItems = await _genericService.GetListAsync(languageId, StringResourceEnums.WhoWeAre);

                var whoWeAreDtos = _mapper.Map<List<WhoWeAre>, List<WhoWeAreDTO>>(whoWeAreItems.Data);

                // Apply translations for each WhoWeAre item
                foreach (var item in whoWeAreDtos)
                {
                    item.LangId = languageId;
                    item.Title = _genericService.ApplyTranslations<WhoWeAreDTO>(item, languageId, item.Id, StringResourceEnums.WhoWeAre).Title;
                    item.Description = _genericService.ApplyTranslations<WhoWeAreDTO>(item, languageId, item.Id, StringResourceEnums.WhoWeAre).Description;

                    // Apply translations for each WhoWeAreItem related to the WhoWeAre
                    if (item.WhoWeAreItems != null)
                    {
                        foreach (var subItem in item.WhoWeAreItems)
                        {
                            subItem.LangId = languageId;
                            subItem.Description = _genericService.ApplyTranslations<WhoWeAreItemDTO>(subItem, languageId, subItem.Id, StringResourceEnums.WhoWeAreItem).Description;
                            
                        }
                    }
                }

                return Success(whoWeAreDtos);
            }
            catch (Exception ex)
            {
                return Error<List<WhoWeAreDTO>>(ex);
            }
        }

        public async Task<ResponseResult<WhoWeAreDTO>> GetById(int id, int languageId)
        {
            try
            {
                var whoWeAre = await _genericService.GetByIdAsync(id, languageId, StringResourceEnums.WhoWeAre,x => x.WhoWeAreItems);

                var whoWeAreDto = _mapper.Map<WhoWeAre, WhoWeAreDTO>(whoWeAre);

                // Apply translations for the WhoWeAre item
                whoWeAreDto.LangId = languageId;
                whoWeAreDto.Title = _genericService.ApplyTranslations<WhoWeAreDTO>(whoWeAreDto, languageId, whoWeAreDto.Id, StringResourceEnums.WhoWeAre).Title;
                whoWeAreDto.Description = _genericService.ApplyTranslations<WhoWeAreDTO>(whoWeAreDto, languageId, whoWeAreDto.Id, StringResourceEnums.WhoWeAre).Description;

                // Apply translations for each WhoWeAreItem
                if (whoWeAreDto.WhoWeAreItems != null)
                {
                    foreach (var subItem in whoWeAreDto.WhoWeAreItems)
                    {
                        subItem.LangId = languageId;
                        subItem.Description = _genericService.ApplyTranslations<WhoWeAreItemDTO>(subItem, languageId, subItem.Id, StringResourceEnums.WhoWeAreItem).Description;
                    }
                }

                return Success(whoWeAreDto);
            }
            catch (Exception ex)
            {
                return Error<WhoWeAreDTO>(ex);
            }
        }

        public async Task<ResponseResult<WhoWeAreDTO>> Manage(WhoWeAreDTO dto)
        {
            try
            {
                var defaultLanguage = await _dbContext.Languages.FirstAsync(x => x.IsDefault);
                bool isDefualtExists = await _dbContext.WhoWeAres.AnyAsync();
                bool IsDefultModel = dto.LangId == defaultLanguage.Id;
                bool IsTranslateionExixts = await _dbContext.StringResources.AnyAsync(x => x.ResourceId == dto.Id && x.GroupKey == StringResourceEnums.WhoWeAre && x.LanguageId == dto.LangId);
                var service = _mapper.Map<WhoWeAreDTO, WhoWeAre>(dto);
                if (IsDefultModel)
                {
                    if (dto.Id > 0)
                    {
                        await _genericService.UpdateAsync(service);
                    }
                    else
                    {
                        await _genericService.CreateAsync(service);
                    }
                    var translations = new List<(string ColumnName, string ColumnValue)>
                            {
                                ("Title", dto.Title),
                                ("Description", dto.Description)
                            };

                    if (dto.Id > 0)
                    {
                        await _genericService.UpdateTranslationsAsync(StringResourceEnums.WhoWeAre, translations, service.Id, dto.LangId);
                    }
                    else
                    {
                        await _genericService.AddTranslationsAsync(StringResourceEnums.WhoWeAre, translations, service.Id, dto.LangId);
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
                            await _genericService.UpdateTranslationsAsync(StringResourceEnums.WhoWeAre, translations, dto.Id, dto.LangId);
                        }
                        else
                        {
                            await _genericService.AddTranslationsAsync(StringResourceEnums.WhoWeAre, translations, dto.Id, dto.LangId);
                        }


                    }
                    else
                    {
                        return Error<WhoWeAreDTO>("Only default language can be managed first");
                    }
                }

                return Success(dto);
            }
            catch (Exception ex)
            {
                return Error<WhoWeAreDTO>(ex);
            }
        }
        // Mabnage SubItem
        public async Task<ResponseResult<WhoWeAreItemDTO>> ManageSubItem(WhoWeAreItemDTO subDto)
        {
            try
            {
                var defaultLanguage = await _dbContext.Languages.FirstAsync(x => x.IsDefault);
                bool IsTranslateionExixts = await _dbContext.StringResources.AnyAsync(x => x.ResourceId == subDto.Id && x.GroupKey == StringResourceEnums.WhoWeAreItem && x.LanguageId == subDto.LangId);
                var subItem = _mapper.Map<WhoWeAreItemDTO, WhoWeAreItem>(subDto);
                if (subDto.LangId == defaultLanguage.Id)
                {
                    if (subDto.Id > 0)
                    {
                        await _genericService.UpdateAsync(subItem);
                    }
                    else
                    {
                        await _genericService.CreateAsync(subItem);
                    }
                }
                var subTranslations = new List<(string ColumnName, string ColumnValue)>
                             {
                                ("Description", subDto.Description),
                             };
                if (subDto.Id > 0 && IsTranslateionExixts)
                {
                    await _genericService.UpdateTranslationsAsync(StringResourceEnums.WhoWeAreItem, subTranslations, subItem.Id, subDto.LangId);
                }
                else
                {
                    await _genericService.AddTranslationsAsync(StringResourceEnums.WhoWeAreItem, subTranslations, subItem.Id, subDto.LangId);
                }
                return Success(subDto);
            }
            catch (Exception ex)
            {
                return Error<WhoWeAreItemDTO>(ex);
            }
        }
        public async Task<ResponseResult<WhoWeAreDTO>> GetFirst()
        {
            try
            {
                var result = await _dbContext.WhoWeAres.Include(x => x.WhoWeAreItems).FirstOrDefaultAsync();
                var serviceDto = _mapper.Map<WhoWeAreDTO>(result);
                return Success(serviceDto);
            }
            catch (Exception ex)
            {
                return Error<WhoWeAreDTO>(ex);
            }
        }
    }

}
