using AutoMapper;
using Core.Domains.Enums;
using Core.HomePage.HomePageItems;
using Data.Context;
using Data.Dtos.WorkShowCaseDTOs;
using Microsoft.EntityFrameworkCore;
using Services._Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Services.HomePageServices.WorkShowCaseServices
{
    public class WorkShowCaseService : BaseService, IWorkShowCaseService
    {
        private readonly Services._GenericServices.GenericService<WorkShowCase> _genericService;
        public WorkShowCaseService(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<ResponseResult<List<WorkShowCaseDTO>>> GetList(int languageId)
        {
            try
            {
                var workShowCases = await _genericService.GetListAsync(languageId, StringResourceEnums.WorkShowCase);
                var dto = _mapper.Map<List<WorkShowCase>, List<WorkShowCaseDTO>>(workShowCases.Data);

                foreach (var item in dto)
                {
                    item.LangId = languageId;
                    item.Title = _genericService.ApplyTranslations<WorkShowCaseDTO>(item, languageId, item.Id, StringResourceEnums.WorkShowCase).Title;
                    item.Description = _genericService.ApplyTranslations<WorkShowCaseDTO>(item, languageId, item.Id, StringResourceEnums.WorkShowCase).Description;

                    // Apply translations for WorkShowCaseNavBar
                    if (item.WorkShowCaseNavBar != null)
                    {
                        item.WorkShowCaseNavBar.Name = _genericService.ApplyTranslations<WorkShowCaseNavBarDTO>(item.WorkShowCaseNavBar, languageId, item.WorkShowCaseNavBar.Id, StringResourceEnums.WorkShowCaseNavBar).Name;
                    }

                    // Apply translations for WorkShowCaseNavBarItems
                    if (item.WorkShowCaseNavBar?.WorkShowCaseNavBarItems != null)
                    {
                        foreach (var navBarItem in item.WorkShowCaseNavBar.WorkShowCaseNavBarItems)
                        {
                            navBarItem.Name = _genericService.ApplyTranslations<WorkShowCaseNavBarItemDTO>(navBarItem, languageId, navBarItem.Id, StringResourceEnums.WorkShowCaseNavBarItem).Name;
                        }
                    }
                }

                return Success(dto);
            }
            catch (Exception ex)
            {
                return Error<List<WorkShowCaseDTO>>(ex);
            }
        }

        public async Task<ResponseResult<WorkShowCaseDTO>> GetById(int id, int languageId)
        {
            try
            {
                var workShowCase = await _genericService.GetByIdAsync(id, languageId, StringResourceEnums.WorkShowCase);
                var dto = _mapper.Map<WorkShowCase, WorkShowCaseDTO>(workShowCase);

                dto.LangId = languageId;
                dto.Title = _genericService.ApplyTranslations<WorkShowCaseDTO>(dto, languageId, dto.Id, StringResourceEnums.WorkShowCase).Title;
                dto.Description = _genericService.ApplyTranslations<WorkShowCaseDTO>(dto, languageId, dto.Id, StringResourceEnums.WorkShowCase).Description;

                // Apply translations for WorkShowCaseNavBar
                if (dto.WorkShowCaseNavBar != null)
                {
                    dto.WorkShowCaseNavBar.Name = _genericService.ApplyTranslations<WorkShowCaseNavBarDTO>(dto.WorkShowCaseNavBar, languageId, dto.WorkShowCaseNavBar.Id, StringResourceEnums.WorkShowCaseNavBar).Name;
                }

                // Apply translations for WorkShowCaseNavBarItems
                if (dto.WorkShowCaseNavBar?.WorkShowCaseNavBarItems != null)
                {
                    foreach (var navBarItem in dto.WorkShowCaseNavBar.WorkShowCaseNavBarItems)
                    {
                        navBarItem.Name = _genericService.ApplyTranslations<WorkShowCaseNavBarItemDTO>(navBarItem, languageId, navBarItem.Id, StringResourceEnums.WorkShowCaseNavBarItem).Name;
                    }
                }

                return Success(dto);
            }
            catch (Exception ex)
            {
                return Error<WorkShowCaseDTO>(ex);
            }
        }

        public async Task<ResponseResult<List<WorkShowCaseDTO>>> Manage(List<WorkShowCaseDTO> dtos)
        {
            try
            {
                var defaultLanguage = await _dbContext.Languages.FirstAsync(x => x.IsDefault);
                var defaultModel = dtos.FirstOrDefault(x => x.LangId == defaultLanguage.Id);

                var workShowCase = _mapper.Map<WorkShowCaseDTO, WorkShowCase>(defaultModel);

                if (defaultModel.Id > 0)
                {
                    await _genericService.UpdateAsync(workShowCase);
                }
                else
                {
                    await _genericService.CreateAsync(workShowCase);
                }

                // Add or Update translations for WorkShowCase
                foreach (var item in dtos)
                {
                    var translations = new List<(string ColumnName, string ColumnValue)>
                    {
                        ("Title", item.Title),
                        ("Description", item.Description)
                    };

                    if (item.Id > 0)
                    {
                        await _genericService.UpdateTranslationsAsync(StringResourceEnums.WorkShowCase, translations, workShowCase.Id, item.LangId);
                    }
                    else
                    {
                        await _genericService.AddTranslationsAsync(StringResourceEnums.WorkShowCase, translations, workShowCase.Id, item.LangId);
                    }

                    // Handle translations for WorkShowCaseNavBar
                    if (item.WorkShowCaseNavBar != null)
                    {
                        var navBarTranslations = new List<(string ColumnName, string ColumnValue)>
                        {
                            ("Name", item.WorkShowCaseNavBar.Name)
                        };

                        if (item.WorkShowCaseNavBar.Id > 0)
                        {
                            await _genericService.UpdateTranslationsAsync(StringResourceEnums.WorkShowCaseNavBar, navBarTranslations, item.WorkShowCaseNavBar.Id, item.LangId);
                        }
                        else
                        {
                            await _genericService.AddTranslationsAsync(StringResourceEnums.WorkShowCaseNavBar, navBarTranslations, item.WorkShowCaseNavBar.Id, item.LangId);
                        }

                        // Handle translations for WorkShowCaseNavBarItems
                        if (item.WorkShowCaseNavBar.WorkShowCaseNavBarItems != null)
                        {
                            foreach (var navBarItem in item.WorkShowCaseNavBar.WorkShowCaseNavBarItems)
                            {
                                var navBarItemTranslations = new List<(string ColumnName, string ColumnValue)>
                                {
                                    ("Name", navBarItem.Name)
                                };

                                if (navBarItem.Id > 0)
                                {
                                    await _genericService.UpdateTranslationsAsync(StringResourceEnums.WorkShowCaseNavBarItem, navBarItemTranslations, navBarItem.Id, item.LangId);
                                }
                                else
                                {
                                    await _genericService.AddTranslationsAsync(StringResourceEnums.WorkShowCaseNavBarItem, navBarItemTranslations, navBarItem.Id, item.LangId);
                                }
                            }
                        }
                    }
                }

                return Success(dtos);
            }
            catch (Exception ex)
            {
                return Error<List<WorkShowCaseDTO>>(ex);
            }
        }
    }

}
