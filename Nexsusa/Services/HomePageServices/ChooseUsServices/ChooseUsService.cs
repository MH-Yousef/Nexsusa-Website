using AutoMapper;
using Core.Domains.Enums;
using Core.HomePage.HomePageItems;
using Data.Context;
using Data.Dtos.ChooseUsDTOs;
using Data.Dtos.QuestionDTOs;
using Microsoft.EntityFrameworkCore;
using Services._Base;
using Services._GenericServices;

namespace Services.HomePageServices.ChooseUsServices
{
    public class ChooseUsService : BaseService, IChooseUsService
    {
        private readonly GenericService<ChooseUs> _genericService;

        public ChooseUsService(AppDbContext dbContext, IMapper mapper, GenericService<ChooseUs> genericService) : base(dbContext, mapper)
        {
            this._genericService = genericService;
        }

        public async Task<ResponseResult<List<ChooseUsDTO>>> GetList(int LanguageId)
        {
            try
            {
                var choosUsItems = await _genericService.GetListAsync(LanguageId, StringResourceEnums.ChooseUs, x => x.Questions);
                var dto = _mapper.Map<List<ChooseUs>, List<ChooseUsDTO>>(choosUsItems.Data);
                foreach (var item in dto)
                {
                    item.LangId = LanguageId;
                    if (item.Questions != null)
                    {
                        foreach (var sub in item.Questions)
                        {
                            sub.LangId = LanguageId;
                            sub.Title = _genericService.ApplyTranslations<QuestionDTO>(sub, LanguageId, sub.Id, StringResourceEnums.Question).Title;
                            sub.Description = _genericService.ApplyTranslations<QuestionDTO>(sub, LanguageId, sub.Id, StringResourceEnums.Question).Description;
                        }
                    }
                }
                return Success(dto);
            }
            catch (Exception ex)
            {
                return Error<List<ChooseUsDTO>>(ex);
            }
        }

        public async Task<ResponseResult<ChooseUsDTO>> GetById(int id, int LanguageId)
        {
            try
            {
                var entity = await _genericService.GetByIdAsync(id, LanguageId, StringResourceEnums.ChooseUs, x => x.Questions);
                var dto = _mapper.Map<ChooseUs, ChooseUsDTO>(entity);
                if (dto.Questions != null)
                {
                    foreach (var sub in dto.Questions)
                    {
                        sub.LangId = LanguageId;
                        sub.Title = _genericService.ApplyTranslations<QuestionDTO>(sub, LanguageId, sub.Id, StringResourceEnums.Question).Title;
                        sub.Description = _genericService.ApplyTranslations<QuestionDTO>(sub, LanguageId, sub.Id, StringResourceEnums.Question).Description;
                    }
                }
                return Success(dto);
            }
            catch (Exception ex)
            {
                return Error<ChooseUsDTO>(ex);
            }
        }

        public async Task<ResponseResult<ChooseUsDTO>> Manage(ChooseUsDTO dto)
        {
            try
            {
                var defaultLanguage = await _dbContext.Languages.FirstAsync(x => x.IsDefault);
                bool isDefualtExists = await _dbContext.Services.AnyAsync();
                bool IsDefultModel = dto.LangId == defaultLanguage.Id;
                bool IsTranslateionExixts = await _dbContext.StringResources.AnyAsync(x => x.ResourceId == dto.Id && x.GroupKey == StringResourceEnums.ChooseUs && x.LanguageId == dto.LangId);
                var mainItem = _mapper.Map<ChooseUsDTO, ChooseUs>(dto);
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
                        await _genericService.UpdateTranslationsAsync(StringResourceEnums.ChooseUs, translations, mainItem.Id, dto.LangId);
                    }
                    else
                    {
                        await _genericService.AddTranslationsAsync(StringResourceEnums.ChooseUs, translations, mainItem.Id, dto.LangId);
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
                            await _genericService.UpdateTranslationsAsync(StringResourceEnums.ChooseUs, translations, dto.Id, dto.LangId);
                        }
                        else
                        {
                            await _genericService.AddTranslationsAsync(StringResourceEnums.ChooseUs, translations, dto.Id, dto.LangId);
                        }


                    }
                    else
                    {
                        return Error<ChooseUsDTO>("Only default language can be managed first");
                    }
                }

                return Success(dto);
            }
            catch (Exception ex)
            {
                return Error<ChooseUsDTO>(ex);
            }
        }
        // Mabnage SubItem
        public async Task<ResponseResult<QuestionDTO>> ManageSubItem(QuestionDTO subDto)
        {
            try
            {
                var defaultLanguage = await _dbContext.Languages.FirstAsync(x => x.IsDefault);
                bool IsTranslateionExists = await _dbContext.StringResources.AnyAsync(x => x.ResourceId == subDto.Id && x.GroupKey == StringResourceEnums.ServiceItem && x.LanguageId == subDto.LangId);
                var subItem = _mapper.Map<QuestionDTO, Question>(subDto);
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
                if (subDto.Id > 0 && IsTranslateionExists)
                {
                    await _genericService.UpdateTranslationsAsync(StringResourceEnums.Question, subTranslations, subItem.Id, subDto.LangId);
                }
                else
                {
                    await _genericService.AddTranslationsAsync(StringResourceEnums.Question, subTranslations, subItem.Id, subDto.LangId);
                }
                return Success(subDto);
            }
            catch (Exception ex)
            {
                return Error<QuestionDTO>(ex);
            }
        }
        public Task<ResponseResult<ChooseUsDTO>> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseResult<ChooseUsDTO>> GetFirst()
        {
            try
            {
                var result = await _dbContext.ChooseUs.Include(x => x.Questions).FirstOrDefaultAsync();
                var serviceDto = _mapper.Map<ChooseUsDTO>(result);
                return Success(serviceDto);
            }
            catch (Exception ex)
            {
                return Error<ChooseUsDTO>(ex);
            }
        }
    }
}
