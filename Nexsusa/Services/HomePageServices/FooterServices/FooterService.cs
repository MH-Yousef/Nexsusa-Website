using AutoMapper;
using Core.Domains.Enums;
using Core.HomePage.HomePageItems;
using Data.Context;
using Data.Dtos.FooterDTOs;
using Data.Dtos.FooterServiceDTOs;
using Data.Dtos.QuickLinkDTOs;
using Microsoft.EntityFrameworkCore;
using Services._Base;
using Services._GenericServices;
using System.Net;

namespace Services.HomePageServices.FooterServices
{
    public class FooterService : BaseService, IFooterService
    {
        private readonly GenericService<Footer> genericService;
        public FooterService(AppDbContext dbContext, IMapper mapper, GenericService<Footer> genericService) : base(dbContext, mapper)
        {
            this.genericService = genericService;
        }
        public async Task<ResponseResult<List<FooterDTO>>> GetList(int LanguageId)
        {
            try
            {
                var items = await genericService.GetListAsync(LanguageId, StringResourceEnums.Footer, x => x.QuickLinks, x => x.Services);
                var dto = _mapper.Map<List<Footer>, List<FooterDTO>>(items.Data);
                foreach (var item in dto)
                {
                    item.LangId = LanguageId;
                    if (item.QuickLinks != null)
                    {
                        foreach (var sub in item.QuickLinks)
                        {
                            sub.LangId = LanguageId;
                            sub.Title = genericService.ApplyTranslations<QuickLinkDTO>(sub, LanguageId, sub.Id, StringResourceEnums.QuickLink).Title;
                        }
                    }
                    if (item.Services != null)
                    {
                        foreach (var sub in item.Services)
                        {
                            sub.LangId = LanguageId;
                            sub.Title = genericService.ApplyTranslations<FooterServiceDTO>(sub, LanguageId, sub.Id, StringResourceEnums.FooterService).Title;
                        }
                    }
                }
                return Success(dto);
            }
            catch (Exception ex)
            {
                return Error<List<FooterDTO>>(ex);
            }
        }

        public async Task<ResponseResult<FooterDTO>> GetById(int id, int LanguageId)
        {
            try
            {
                var model = await genericService.GetByIdAsync(id, LanguageId, StringResourceEnums.Footer, x => x.QuickLinks, x => x.Services);
                var dto = _mapper.Map<Footer, FooterDTO>(model);

                if (dto.QuickLinks != null)
                {
                    foreach (var item in dto.QuickLinks)
                    {
                        item.LangId = LanguageId;
                        item.Title = genericService.ApplyTranslations<QuickLinkDTO>(item, LanguageId, item.Id, StringResourceEnums.QuickLink).Title;
                    }
                }
                if (dto.Services != null)
                {
                    foreach (var item in dto.Services)
                    {
                        item.LangId = LanguageId;
                        item.Title = genericService.ApplyTranslations<FooterServiceDTO>(item, LanguageId, item.Id, StringResourceEnums.FooterService).Title;
                    }
                }

                return Success(dto);
            }
            catch (Exception ex)
            {
                return Error<FooterDTO>(ex);
            }
        }
        public async Task<ResponseResult<List<FooterDTO>>> Manage(List<FooterDTO> dtos)
        {
            try
            {
                var defultLanguage = await _dbContext.Languages.FirstAsync(x => x.IsDefault);
                var defultModel = dtos.Where(x => x.LangId == defultLanguage.Id).FirstOrDefault();
                var mainItem = _mapper.Map<FooterDTO, Footer>(defultModel);
                if (defultModel.Id > 0)
                {
                    var result = await genericService.UpdateAsync(mainItem);
                }
                else
                {
                    var result = await genericService.CreateAsync(mainItem);
                }

                // Add Translations
                foreach (var item in dtos)
                {
                    var translations = new List<(string ColumnName, string ColumnValue)>
                            {
                                ("Description", item.Description),
                            };
                    if (item.Id > 0)
                    {
                        await genericService.UpdateTranslationsAsync(StringResourceEnums.Footer, translations, mainItem.Id, item.LangId);
                    }
                    else
                    {
                        await genericService.AddTranslationsAsync(StringResourceEnums.Footer, translations, mainItem.Id, item.LangId);
                    }
                    if (item.QuickLinks.Any())
                    {

                        foreach (var subItem in item.QuickLinks)
                        {
                            var subItemModel = await _dbContext.QuickLinks.FirstOrDefaultAsync(x => x.FooterId == mainItem.Id);
                            var Subtranslations = new List<(string ColumnName, string ColumnValue)>
                            {
                                ("Title", subItem.Title),
                            };
                            if (subItem.Id > 0)
                            {
                                await genericService.UpdateTranslationsAsync(StringResourceEnums.QuickLink, Subtranslations, subItemModel.Id, subItem.LangId);
                            }
                            else
                            {
                                await genericService.AddTranslationsAsync(StringResourceEnums.QuickLink, Subtranslations, subItemModel.Id, subItem.LangId);
                            }
                        }
                    }
                    if (item.Services.Any())
                    {

                        foreach (var subItem in item.Services)
                        {
                            var subItemModel = await _dbContext.FooterServices.FirstOrDefaultAsync(x => x.FooterId == mainItem.Id);
                            var Subtranslations = new List<(string ColumnName, string ColumnValue)>
                            {
                                ("Title", subItem.Title),
                            };
                            if (subItem.Id > 0)
                            {
                                await genericService.UpdateTranslationsAsync(StringResourceEnums.FooterService, Subtranslations, subItemModel.Id, subItem.LangId);
                            }
                            else
                            {
                                await genericService.AddTranslationsAsync(StringResourceEnums.FooterService, Subtranslations, subItemModel.Id, subItem.LangId);
                            }
                        }
                    }
                }

                return Success(dtos);
            }
            catch (Exception ex)
            {
                return Error<List<FooterDTO>>(ex);
            }
        }
        public Task<ResponseResult<FooterDTO>> Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
