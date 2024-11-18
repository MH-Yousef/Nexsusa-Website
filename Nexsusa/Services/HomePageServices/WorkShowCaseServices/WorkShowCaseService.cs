using AutoMapper;
using Core.Domains.Enums;
using Core.HomePage.HomePageItems;
using Data.Context;
using Data.Dtos.ServiceDTOs;
using Data.Dtos.WorkShowCaseDTOs;
using Microsoft.EntityFrameworkCore;
using Services._Base;
using Services._GenericServices;

namespace Services.HomePageServices.WorkShowCaseServices
{
    public class WorkShowCaseService : BaseService, IWorkShowCaseService
    {
        private readonly GenericService<WorkShowCase> _genericService;
        public WorkShowCaseService(AppDbContext dbContext, IMapper mapper, GenericService<WorkShowCase> genericService) : base(dbContext, mapper)
        {
            _genericService = genericService;
        }

        public async Task<ResponseResult<List<WorkShowCaseDTO>>> GetList(int langId)
        {
            try
            {
                var items = await _genericService.GetListAsync(langId, StringResourceEnums.WorkShowCase, x => x.WorkShowCaseItems);
                var dto = _mapper.Map<List<WorkShowCase>, List<WorkShowCaseDTO>>(items.Data);

                // Iterate through the services and their items for translations
                foreach (var service in dto)
                {
                    service.LangId = langId;

                    if (service.WorkShowCaseItems != null)
                    {
                        foreach (var item in service.WorkShowCaseItems)
                        {
                            var Translate = _genericService.ApplyTranslations<WorkShowCaseItemDTO>(item, langId, item.Id, StringResourceEnums.WorkShowCaseItem);
                            item.LangId = langId;
                            item.Title = Translate.Title;
                            item.SubTitle = Translate.SubTitle;
                            item.Description = Translate.Description;
                            item.SubDescription = Translate.SubDescription;
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

        public async Task<ResponseResult<WorkShowCaseDTO>> GetById(int id, int langId)
        {
            try
            {
                var service = await _genericService.GetByIdAsync(id, langId, StringResourceEnums.WorkShowCase, x => x.WorkShowCaseItems);
                var dto = _mapper.Map<WorkShowCase, WorkShowCaseDTO>(service);

                if (dto.WorkShowCaseItems != null)
                {
                    foreach (var item in dto.WorkShowCaseItems)
                    {
                        var Translate = _genericService.ApplyTranslations<WorkShowCaseItemDTO>(item, langId, item.Id, StringResourceEnums.WorkShowCaseItem);
                        item.LangId = langId;
                        item.SubTitle = Translate.SubTitle;
                        item.Title = Translate.Title;
                        item.Description = Translate.Description;
                        item.SubDescription = Translate.SubDescription;
                    }
                }

                return Success(dto);
            }
            catch (Exception ex)
            {
                return Error<WorkShowCaseDTO>(ex);
            }
        }

        public async Task<ResponseResult<WorkShowCaseDTO>> Manage(WorkShowCaseDTO dto)
        {
            try
            {
                var defaultLanguage = await _dbContext.Languages.FirstAsync(x => x.IsDefault);
                bool isDefualtExists = await _dbContext.WorkShowCases.AnyAsync();
                bool IsDefultModel = dto.LangId == defaultLanguage.Id;
                bool IsTranslateionExixts = await _dbContext.StringResources.AnyAsync(x => x.ResourceId == dto.Id && x.GroupKey == StringResourceEnums.WorkShowCase && x.LanguageId == dto.LangId);
                var service = _mapper.Map<WorkShowCaseDTO, WorkShowCase>(dto);
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
                        await _genericService.UpdateTranslationsAsync(StringResourceEnums.WorkShowCase, translations, service.Id, dto.LangId);
                    }
                    else
                    {
                        await _genericService.AddTranslationsAsync(StringResourceEnums.WorkShowCase, translations, service.Id, dto.LangId);
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
                            await _genericService.UpdateTranslationsAsync(StringResourceEnums.WorkShowCase, translations, dto.Id, dto.LangId);
                        }
                        else
                        {
                            await _genericService.AddTranslationsAsync(StringResourceEnums.WorkShowCase, translations, dto.Id, dto.LangId);
                        }


                    }
                    else
                    {
                        return Error<WorkShowCaseDTO>("Only default language can be managed first");
                    }
                }

                return Success(dto);
            }
            catch (Exception ex)
            {
                return Error<WorkShowCaseDTO>(ex);
            }
        }
        // Mabnage SubItem
        public async Task<ResponseResult<WorkShowCaseItemDTO>> ManageSubItem(WorkShowCaseItemDTO subDto)
        {
            try
            {
                var defaultLanguage = await _dbContext.Languages.FirstAsync(x => x.IsDefault);
                bool IsTranslateionExixts = await _dbContext.StringResources.AnyAsync(x => x.ResourceId == subDto.Id && x.GroupKey == StringResourceEnums.WorkShowCaseItem && x.LanguageId == subDto.LangId);
                var subItem = _mapper.Map<WorkShowCaseItemDTO, WorkShowCaseItem>(subDto);
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
                                ("Description", subDto.Description),
                                ("SubTitle", subDto.SubTitle),
                                ("SubDescription", subDto.SubDescription)
                             };
                if (subDto.Id > 0 && IsTranslateionExixts)
                {
                    await _genericService.UpdateTranslationsAsync(StringResourceEnums.WorkShowCaseItem, subTranslations, subItem.Id, subDto.LangId);
                }
                else
                {
                    await _genericService.AddTranslationsAsync(StringResourceEnums.WorkShowCaseItem, subTranslations, subItem.Id, subDto.LangId);
                }
                return Success(subDto);
            }
            catch (Exception ex)
            {
                return Error<WorkShowCaseItemDTO>(ex);
            }
        }
        public async Task<ResponseResult<WorkShowCaseDTO>> GetFirst()
        {
            try
            {
                var result = await _dbContext.WorkShowCases.Include(x => x.WorkShowCaseItems).FirstOrDefaultAsync();
                var serviceDto = _mapper.Map<WorkShowCaseDTO>(result);
                return Success(serviceDto);
            }
            catch (Exception ex)
            {
                return Error<WorkShowCaseDTO>(ex);
            }
        }
    }

}
