using AutoMapper;
using Core.Domains.Enums;
using Core.HomePage.HomePageItems;
using Data.Context;
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
        public WhoWeAreService(AppDbContext dbContext, IMapper mapper, GenericService<WhoWeAre> genericService = null) : base(dbContext, mapper)
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
                    if (item.WhoWeAreItem != null)
                    {
                        foreach (var subItem in item.WhoWeAreItem)
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
                var whoWeAre = await _genericService.GetByIdAsync(id, languageId, StringResourceEnums.WhoWeAre);

                var whoWeAreDto = _mapper.Map<WhoWeAre, WhoWeAreDTO>(whoWeAre);

                // Apply translations for the WhoWeAre item
                whoWeAreDto.LangId = languageId;
                whoWeAreDto.Title = _genericService.ApplyTranslations<WhoWeAreDTO>(whoWeAreDto, languageId, whoWeAreDto.Id, StringResourceEnums.WhoWeAre).Title;
                whoWeAreDto.Description = _genericService.ApplyTranslations<WhoWeAreDTO>(whoWeAreDto, languageId, whoWeAreDto.Id, StringResourceEnums.WhoWeAre).Description;

                // Apply translations for each WhoWeAreItem
                if (whoWeAreDto.WhoWeAreItem != null)
                {
                    foreach (var subItem in whoWeAreDto.WhoWeAreItem)
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

        // Manage (Create/Update) WhoWeAre and its Items with Translations
        public async Task<ResponseResult<List<WhoWeAreDTO>>> Manage(List<WhoWeAreDTO> dtos)
        {
            try
            {
                var defaultLanguage = await _dbContext.Languages.FirstAsync(x => x.IsDefault);
                var defaultModel = dtos.FirstOrDefault(x => x.LangId == defaultLanguage.Id);

                var whoWeAre = _mapper.Map<WhoWeAreDTO, WhoWeAre>(defaultModel);

                if (defaultModel.Id > 0)
                {
                    await _genericService.UpdateAsync(whoWeAre);
                }
                else
                {
                    await _genericService.CreateAsync(whoWeAre);
                }

                // Add or Update Translations for WhoWeAre
                foreach (var item in dtos)
                {
                    var translations = new List<(string ColumnName, string ColumnValue)>
                    {
                        ("Title", item.Title),
                        ("Description", item.Description)
                    };

                    if (item.Id > 0)
                    {
                        await _genericService.UpdateTranslationsAsync(StringResourceEnums.WhoWeAre, translations, whoWeAre.Id, item.LangId);
                    }
                    else
                    {
                        await _genericService.AddTranslationsAsync(StringResourceEnums.WhoWeAre, translations, whoWeAre.Id, item.LangId);
                    }

                    // Handle translations for related WhoWeAreItems
                    if (item.WhoWeAreItem != null && item.WhoWeAreItem.Any())
                    {
                        foreach (var subItem in item.WhoWeAreItem)
                        {
                            var subTranslations = new List<(string ColumnName, string ColumnValue)>
                            {
                                ("Description", subItem.Description)
                            };

                            if (subItem.Id > 0)
                            {
                                await _genericService.UpdateTranslationsAsync(StringResourceEnums.WhoWeAreItem, subTranslations, subItem.Id, subItem.LangId);
                            }
                            else
                            {
                                await _genericService.AddTranslationsAsync(StringResourceEnums.WhoWeAreItem, subTranslations, subItem.Id, subItem.LangId);
                            }
                        }
                    }
                }

                return Success(dtos);
            }
            catch (Exception ex)
            {
                return Error<List<WhoWeAreDTO>>(ex);
            }
        }
    }

}
