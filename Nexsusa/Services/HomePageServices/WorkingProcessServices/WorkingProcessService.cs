using AutoMapper;
using Core.Domains.Enums;
using Core.HomePage.HomePageItems;
using Data.Context;
using Data.Dtos.WorkingProcessDTOs;
using Microsoft.EntityFrameworkCore;
using Services._Base;
using Services._GenericServices;
using Services.ImageServices;

namespace Services.HomePageServices.WorkingProcessServices
{

    public class WorkingProcessService : BaseService, IWorkingProcessService
    {
        private readonly GenericService<WorkingProcess> _genericService;
        private readonly IImageService _imageService;
        public WorkingProcessService(AppDbContext dbContext, IMapper mapper, _GenericServices.GenericService<WorkingProcess> genericService, IImageService imageService) : base(dbContext, mapper)
        {
            _genericService = genericService;
            _imageService = imageService;
        }
        public async Task<ResponseResult<List<WorkingProcessDTO>>> GetList(int languageId)
        {
            try
            {
                // Retrieve WorkingProcess data with translations
                var workingProcesses = await _genericService.GetListAsync(languageId, StringResourceEnums.WorkingProcess, x => x.WorkingProcessItems);

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
                var workingProcess = await _genericService.GetByIdAsync(id, languageId, StringResourceEnums.WorkingProcess, x => x.WorkingProcessItems);

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
        public async Task<ResponseResult<WorkingProcessDTO>> Manage(WorkingProcessDTO dto)
        {
            try
            {
                var defaultLanguage = await _dbContext.Languages.FirstAsync(x => x.IsDefault);
                bool isDefualtExists = await _dbContext.WorkingProcesses.AnyAsync();
                bool IsDefultModel = dto.LangId == defaultLanguage.Id;
                bool IsTranslateionExixts = await _dbContext.StringResources.AnyAsync(x => x.ResourceId == dto.Id && x.GroupKey == StringResourceEnums.Service && x.LanguageId == dto.LangId);
                var service = _mapper.Map<WorkingProcessDTO, WorkingProcess>(dto);
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
                                ("SubTitle", dto.SubTitle)
                            };

                    if (dto.Id > 0)
                    {
                        await _genericService.UpdateTranslationsAsync(StringResourceEnums.WorkingProcess, translations, service.Id, dto.LangId);
                    }
                    else
                    {
                        await _genericService.AddTranslationsAsync(StringResourceEnums.WorkingProcess, translations, service.Id, dto.LangId);
                    }
                }
                else
                {
                    if (isDefualtExists)
                    {
                        var translations = new List<(string ColumnName, string ColumnValue)>
                            {
                                ("Title", dto.Title),
                                ("SubTitle", dto.SubTitle)
                            };

                        if (dto.Id > 0 && IsTranslateionExixts)
                        {
                            await _genericService.UpdateTranslationsAsync(StringResourceEnums.WorkingProcess, translations, dto.Id, dto.LangId);
                        }
                        else
                        {
                            await _genericService.AddTranslationsAsync(StringResourceEnums.WorkingProcess, translations, dto.Id, dto.LangId);
                        }


                    }
                    else
                    {
                        return Error<WorkingProcessDTO>("Only default language can be managed first");
                    }
                }

                return Success(dto);
            }
            catch (Exception ex)
            {
                return Error<WorkingProcessDTO>(ex);
            }
        }
        // Mabnage SubItem
        public async Task<ResponseResult<WorkingProcessItemDTO>> ManageSubItem(WorkingProcessItemDTO subDto)
        {
            try
            {
                var defaultLanguage = await _dbContext.Languages.FirstAsync(x => x.IsDefault);
                bool IsTranslateionExixts = await _dbContext.StringResources.AnyAsync(x => x.ResourceId == subDto.Id && x.GroupKey == StringResourceEnums.WorkingProcessItem && x.LanguageId == subDto.LangId);
                var subItem = _mapper.Map<WorkingProcessItemDTO, WorkingProcessItem>(subDto);
                if (subDto.File != null)
                {
                    var imageResult = await _imageService.UploadImage(subDto.File);
                    subItem.ImageUrl = imageResult;
                }
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
                                ("Title", subDto.Title),
                                ("Description", subDto.Description)
                             };
                if (subDto.Id > 0 && IsTranslateionExixts)
                {
                    await _genericService.UpdateTranslationsAsync(StringResourceEnums.WorkingProcessItem, subTranslations, subItem.Id, subDto.LangId);
                }
                else
                {
                    await _genericService.AddTranslationsAsync(StringResourceEnums.WorkingProcessItem, subTranslations, subItem.Id, subDto.LangId);
                }
                return Success(subDto);
            }
            catch (Exception ex)
            {
                return Error<WorkingProcessItemDTO>(ex);
            }
        }
        public async Task<ResponseResult<WorkingProcessDTO>> GetFirst()
        {
            try
            {
                var result = await _dbContext.WorkingProcesses.Include(x => x.WorkingProcessItems).FirstOrDefaultAsync();
                var serviceDto = _mapper.Map<WorkingProcessDTO>(result);
                return Success(serviceDto);
            }
            catch (Exception ex)
            {
                return Error<WorkingProcessDTO>(ex);
            }
        }
    }


}
