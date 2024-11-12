using AutoMapper;
using Core.Domains.Enums;
using Core.HomePage.HomePageItems;
using Data.Context;
using Data.Dtos.WorkingProcessDTOs;
using Microsoft.EntityFrameworkCore;
using Services._Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Services.HomePageServices.WorkingProcessServices
{
    public class WorkingProcessService : BaseService, IWorkingProcessService
    {
        private readonly Services._GenericServices.GenericService<WorkingProcess> _genericService;
        public WorkingProcessService(AppDbContext dbContext, IMapper mapper, _GenericServices.GenericService<WorkingProcess> genericService) : base(dbContext, mapper)
        {
            _genericService = genericService;
        }

        public async Task<ResponseResult<List<WorkingProcessDTO>>> GetList(int languageId)
        {
            try
            {
                // Retrieve WorkingProcess data with translations
                var workingProcesses = await _genericService.GetListAsync(languageId, StringResourceEnums.WorkingProcess);

                var workingProcessDtos = _mapper.Map<List<WorkingProcess>, List<WorkingProcessDTO>>(workingProcesses.Data);

                // Apply translations for each WorkingProcess item
                foreach (var item in workingProcessDtos)
                {
                    item.LangId = languageId;
                    item.Title = _genericService.ApplyTranslations<WorkingProcessDTO>(item, languageId, item.Id, StringResourceEnums.WorkingProcess).Title;
                    item.SubTitle = _genericService.ApplyTranslations<WorkingProcessDTO>(item, languageId, item.Id, StringResourceEnums.WorkingProcess).SubTitle;

                    // Apply translations for each WorkingProcessItem related to the WorkingProcess
                    if (item.WorkingProcessItems != null)
                    {
                        foreach (var subItem in item.WorkingProcessItems)
                        {
                            subItem.LangId = languageId;
                            subItem.Title = _genericService.ApplyTranslations<WorkingProcessItemDTO>(subItem, languageId, subItem.Id, StringResourceEnums.WorkingProcessItem).Title;
                            subItem.Description = _genericService.ApplyTranslations<WorkingProcessItemDTO>(subItem, languageId, subItem.Id, StringResourceEnums.WorkingProcessItem).Description;
                        }
                    }
                }

                return Success(workingProcessDtos);
            }
            catch (Exception ex)
            {
                return Error<List<WorkingProcessDTO>>(ex);
            }
        }


        public async Task<ResponseResult<WorkingProcessDTO>> GetById(int id, int languageId)
        {
            try
            {
                var workingProcess = await _genericService.GetByIdAsync(id, languageId, StringResourceEnums.WorkingProcess);

                var workingProcessDto = _mapper.Map<WorkingProcess, WorkingProcessDTO>(workingProcess);

                // Apply translations for the WorkingProcess item
                workingProcessDto.LangId = languageId;
                workingProcessDto.Title = _genericService.ApplyTranslations<WorkingProcessDTO>(workingProcessDto, languageId, workingProcessDto.Id, StringResourceEnums.WorkingProcess).Title;
                workingProcessDto.SubTitle = _genericService.ApplyTranslations<WorkingProcessDTO>(workingProcessDto, languageId, workingProcessDto.Id, StringResourceEnums.WorkingProcess).SubTitle;

                // Apply translations for each WorkingProcessItem
                if (workingProcessDto.WorkingProcessItems != null)
                {
                    foreach (var subItem in workingProcessDto.WorkingProcessItems)
                    {
                        subItem.LangId = languageId;
                        subItem.Title = _genericService.ApplyTranslations<WorkingProcessItemDTO>(subItem, languageId, subItem.Id, StringResourceEnums.WorkingProcessItem).Title;
                        subItem.Description = _genericService.ApplyTranslations<WorkingProcessItemDTO>(subItem, languageId, subItem.Id, StringResourceEnums.WorkingProcessItem).Description;
                    }
                }

                return Success(workingProcessDto);
            }
            catch (Exception ex)
            {
                return Error<WorkingProcessDTO>(ex);
            }
        }

        public async Task<ResponseResult<List<WorkingProcessDTO>>> Manage(List<WorkingProcessDTO> dtos)
        {
            try
            {
                var defaultLanguage = await _dbContext.Languages.FirstAsync(x => x.IsDefault);
                var defaultModel = dtos.FirstOrDefault(x => x.LangId == defaultLanguage.Id);

                var workingProcess = _mapper.Map<WorkingProcessDTO, WorkingProcess>(defaultModel);

                if (defaultModel.Id > 0)
                {
                    await _genericService.UpdateAsync(workingProcess);
                }
                else
                {
                    await _genericService.CreateAsync(workingProcess);
                }

                // Add or Update Translations for WorkingProcess
                foreach (var item in dtos)
                {
                    var translations = new List<(string ColumnName, string ColumnValue)>
                    {
                        ("Title", item.Title),
                        ("SubTitle", item.SubTitle)
                    };

                    if (item.Id > 0)
                    {
                        await _genericService.UpdateTranslationsAsync(StringResourceEnums.WorkingProcess, translations, workingProcess.Id, item.LangId);
                    }
                    else
                    {
                        await _genericService.AddTranslationsAsync(StringResourceEnums.WorkingProcess, translations, workingProcess.Id, item.LangId);
                    }

                    // Handle translations for related WorkingProcessItems
                    if (item.WorkingProcessItems != null && item.WorkingProcessItems.Any())
                    {
                        foreach (var subItem in item.WorkingProcessItems)
                        {
                            var subTranslations = new List<(string ColumnName, string ColumnValue)>
                            {
                                ("Title", subItem.Title),
                                ("Description", subItem.Description)
                            };

                            if (subItem.Id > 0)
                            {
                                await _genericService.UpdateTranslationsAsync(StringResourceEnums.WorkingProcessItem, subTranslations, subItem.Id, subItem.LangId);
                            }
                            else
                            {
                                await _genericService.AddTranslationsAsync(StringResourceEnums.WorkingProcessItem, subTranslations, subItem.Id, subItem.LangId);
                            }
                        }
                    }
                }

                return Success(dtos);
            }
            catch (Exception ex)
            {
                return Error<List<WorkingProcessDTO>>(ex);
            }
        }
    }


}
