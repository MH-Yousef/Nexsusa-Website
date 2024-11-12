using AutoMapper;
using Core.Domains.Enums;
using Core.HomePage.HomePageItems;
using Data.Context;
using Data.Dtos.NavBarDTOs;
using Microsoft.EntityFrameworkCore;
using Services._Base;
using Services._GenericServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Services.HomePageServices.NavBarItemServices
{
    public class NavBarItemService : BaseService, INavBarItemService
    {
        private readonly GenericService<NavBarItem> genericService;
        public NavBarItemService(AppDbContext dbContext, IMapper mapper, GenericService<NavBarItem> genericService) : base(dbContext, mapper)
        {
            this.genericService = genericService;
        }


        public async Task<ResponseResult<List<NavBarItemDTO>>> GetList(int LanguageId)
        {
            try
            {
                var navbarItems = await genericService.GetListAsync(LanguageId, StringResourceEnums.NavBarItem, x => x.NavBarItemSubItems);
                var dto = _mapper.Map<List<NavBarItem>, List<NavBarItemDTO>>(navbarItems.Data);
                foreach (var item in dto)
                {
                    item.LangId = LanguageId;
                    if (item.NavBarItemSubItems != null)
                    {
                        foreach (var sub in item.NavBarItemSubItems)
                        {
                            sub.LangId = LanguageId;
                            sub.Name = genericService.ApplyTranslations<NavBarItemSubItemDTO>(sub, LanguageId, sub.Id, StringResourceEnums.NavBarSubItem).Name;
                        }
                    }
                }
                return Success(dto);
            }
            catch (Exception ex)
            {
                return Error<List<NavBarItemDTO>>(ex);
            }
        }

        public async Task<ResponseResult<NavBarItemDTO>> GetById(int id, int languageId)
        {
            try
            {
                var navbar = await genericService.GetByIdAsync(id, languageId, StringResourceEnums.NavBarItem, x => x.NavBarItemSubItems);
                var dto = _mapper.Map<NavBarItem, NavBarItemDTO>(navbar);

                if (dto.NavBarItemSubItems != null)
                {
                    foreach (var item in dto.NavBarItemSubItems)
                    {
                        item.LangId = languageId;
                        item.Name = genericService.ApplyTranslations<NavBarItemSubItemDTO>(item, languageId, item.Id, StringResourceEnums.NavBarSubItem).Name;
                    }
                }

                return Success(dto);
            }
            catch (Exception ex)
            {
                return Error<NavBarItemDTO>(ex);
            }
        }

        public async Task<ResponseResult<List<NavBarItemDTO>>> Manage(List<NavBarItemDTO> dtos)
        {
            try
            {
                var defultLanguage = await _dbContext.Languages.FirstAsync(x => x.IsDefault);
                var defultModel = dtos.Where(x => x.LangId == defultLanguage.Id).FirstOrDefault();
                var navBarItem = _mapper.Map<NavBarItemDTO, NavBarItem>(defultModel);
                if (defultModel.Id > 0)
                {
                    var result = await genericService.UpdateAsync(navBarItem);
                }
                else
                {
                    var result = await genericService.CreateAsync(navBarItem);
                }

                // Add Translations
                foreach (var item in dtos)
                {
                    var translations = new List<(string ColumnName, string ColumnValue)>
                            {
                                ("Name", item.Name),
                            };
                    if (item.Id > 0)
                    {
                        await genericService.UpdateTranslationsAsync(StringResourceEnums.NavBarItem, translations, navBarItem.Id, item.LangId);
                    }
                    else
                    {
                        await genericService.AddTranslationsAsync(StringResourceEnums.NavBarItem, translations, navBarItem.Id, item.LangId);
                    }
                    if (item.NavBarItemSubItems.Any())
                    {

                        foreach (var subTtem in item.NavBarItemSubItems)
                        {
                            var subNavBar = await _dbContext.NavBarItemSubItems.FirstOrDefaultAsync(x => x.NavBarItemId == navBarItem.Id);
                            var Subtranslations = new List<(string ColumnName, string ColumnValue)>
                            {
                                ("Name", subTtem.Name),
                            };
                            if (subTtem.Id > 0)
                            {
                                await genericService.UpdateTranslationsAsync(StringResourceEnums.NavBarSubItem, Subtranslations, subNavBar.Id, subTtem.LangId);
                            }
                            else
                            {
                                await genericService.AddTranslationsAsync(StringResourceEnums.NavBarSubItem, Subtranslations, subNavBar.Id, subTtem.LangId);
                            }
                        }
                    }
                }

                return Success(dtos);
            }
            catch (Exception ex)
            {
                return Error<List<NavBarItemDTO>>(ex);
            }
        }
        public async Task<ResponseResult<NavBarItemDTO>> Delete(int id)
        {
            try
            {
                var model = await _dbContext.NavBarItems.Include(x => x.NavBarItemSubItems).FirstOrDefaultAsync(x => x.Id == id);
                if (model == null)
                {
                    return Error<NavBarItemDTO>("NavBarItem not found", HttpStatusCode.NotFound);
                }
                model.IsDeleted = true;
                foreach (var item in model.NavBarItemSubItems)
                {
                    item.IsDeleted = true;

                }
                _dbContext.Update(model);
                var translations = await _dbContext.StringResources.Where(x => x.ResourceId == id && x.GroupKey == StringResourceEnums.NavBarItem).ToListAsync();
                var subtranslations = await _dbContext.StringResources.Where(x => x.ResourceId == id && x.GroupKey == StringResourceEnums.NavBarSubItem).ToListAsync();
                foreach (var item in translations)
                {
                    item.IsDeleted = true;
                    _dbContext.Update(item);
                }
                foreach (var item in subtranslations)
                {
                    item.IsDeleted = true;
                    _dbContext.Update(item);
                }
                await _dbContext.SaveChangesAsync();
                return Success<NavBarItemDTO>();
            }
            catch (Exception ex)
            {
                return Error<NavBarItemDTO>(ex);
            }
        }
    }

}
