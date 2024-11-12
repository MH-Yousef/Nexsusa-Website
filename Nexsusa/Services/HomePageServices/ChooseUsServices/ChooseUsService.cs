using AutoMapper;
using Core.Domains.Enums;
using Core.Domains.Languages;
using Core.HomePage.HomePageItems;
using Data.Context;
using Data.Dtos.ChooseUsDTOs;
using Data.Dtos.QuestionDTOs;
using Microsoft.EntityFrameworkCore;
using Services._Base;
using Services._GenericServices;
using Services._GenericServices.Test;

namespace Services.HomePageServices.ChooseUsServices
{
    public class ChooseUsService : BaseService, IChooseUsService
    {
        private readonly GenericService<ChooseUs> genericService;

        public ChooseUsService(AppDbContext dbContext, IMapper mapper, GenericService<ChooseUs> genericService) : base(dbContext, mapper)
        {
            this.genericService = genericService;
        }

        public async Task<ResponseResult<List<ChooseUsDTO>>> GetList(int LanguageId)
        {
            try
            {
                var choosUsItems = await genericService.GetListAsync(LanguageId, StringResourceEnums.ChooseUs, x => x.Questions);
                var dto = _mapper.Map<List<ChooseUs>, List<ChooseUsDTO>>(choosUsItems.Data);
                foreach (var item in dto)
                {
                    item.LangId = LanguageId;
                    if (item.Questions != null)
                    {
                        foreach (var sub in item.Questions)
                        {
                            sub.LangId = LanguageId;
                            sub.Title = genericService.ApplyTranslations<QuestionDTO>(sub, LanguageId, sub.Id, StringResourceEnums.Question).Title;
                            sub.Description = genericService.ApplyTranslations<QuestionDTO>(sub, LanguageId, sub.Id, StringResourceEnums.Question).Description;
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
                var entity = await genericService.GetByIdAsync(id, LanguageId, StringResourceEnums.ChooseUs, x => x.Questions);
                var dto = _mapper.Map<ChooseUs, ChooseUsDTO>(entity);
                if (dto.Questions != null)
                {
                    foreach (var sub in dto.Questions)
                    {
                        sub.LangId = LanguageId;
                        sub.Title = genericService.ApplyTranslations<QuestionDTO>(sub, LanguageId, sub.Id, StringResourceEnums.Question).Title;
                        sub.Description = genericService.ApplyTranslations<QuestionDTO>(sub, LanguageId, sub.Id, StringResourceEnums.Question).Description;
                    }
                }
                return Success(dto);
            }
            catch (Exception ex)
            {
                return Error<ChooseUsDTO>(ex);
            }
        }

        public async Task<ResponseResult<List<ChooseUsDTO>>> Manage(List<ChooseUsDTO> dto)
        {
            try
            {
                var defultLanguage = await _dbContext.Languages.FirstAsync(x => x.IsDefault);
                var defultModel = dto.FirstOrDefault(x => x.LangId == defultLanguage.Id);
                var MainItem = _mapper.Map<ChooseUs>(defultModel);
                if (MainItem.Id == 0)
                {
                    await genericService.CreateAsync(MainItem);
                }
                else
                {
                    await genericService.UpdateAsync(MainItem);
                }
                // Add Translations
                foreach (var item in dto)
                {
                    var translations = new List<(string ColumnName, string ColumnValue)>
                    {
                        ("Title", item.Title),
                        ("Description", item.Description)
                    };
                    if (item.Id > 0)
                    {
                        await genericService.UpdateTranslationsAsync(StringResourceEnums.ChooseUs, translations, MainItem.Id, item.LangId);
                    }
                    else
                    {
                        await genericService.AddTranslationsAsync(StringResourceEnums.ChooseUs, translations, MainItem.Id, item.LangId);
                    }
                    if (item.Questions.Any())
                    {

                        foreach (var subTtem in item.Questions)
                        {
                            var subNavBar = await _dbContext.NavBarItemSubItems.FirstOrDefaultAsync(x => x.NavBarItemId == MainItem.Id);
                            var Subtranslations = new List<(string ColumnName, string ColumnValue)>
                            {
                                ("Title", subTtem.Title),
                                ("Description", subTtem.Description)
                            };
                            if (subTtem.Id > 0)
                            {
                                await genericService.UpdateTranslationsAsync(StringResourceEnums.Question, Subtranslations, subNavBar.Id, subTtem.LangId);
                            }
                            else
                            {
                                await genericService.AddTranslationsAsync(StringResourceEnums.Question, Subtranslations, subNavBar.Id, subTtem.LangId);
                            }
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
        public Task<ResponseResult<ChooseUsDTO>> Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
