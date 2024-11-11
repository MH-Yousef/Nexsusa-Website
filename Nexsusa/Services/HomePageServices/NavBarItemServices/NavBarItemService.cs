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

        public async Task<ResponseResult<List<NavBarItemDTO>>> Get(int LanguageId)
        {
            try
            {
                var navbarItems = await _dbContext.NavBarItems.AsNoTracking()
                    .Include(x => x.NavBarItemSubItems)
                    .Where(x => !x.IsDeleted).Select(x => new NavBarItemDTO
                    {
                        Id = x.Id,
                        CreatedDate = x.CreatedDate,
                        HasSubItem = x.HasSubItem,
                        Icon = x.Icon,
                        IsVisible = x.IsVisible,
                        Name = _dbContext.StringResources.Where(t => t.ResourceId == x.Id && t.Key == nameof(x.Name) && t.LanguageId == LanguageId && t.GroupKey == StringResourceEnums.NavBarItem).FirstOrDefault().Value,
                        LangId = LanguageId,
                        UpdatedDate = x.UpdatedDate,
                        Url = x.Url,
                        NavBarItemSubItems = x.NavBarItemSubItems.Select(s => new NavBarItemSubItemDTO
                        {
                            Id = s.Id,
                            CreatedDate = s.CreatedDate,
                            Icon = s.Icon,
                            IsVisible = s.IsVisible,
                            Name = _dbContext.StringResources.Where(t => t.ResourceId == s.Id && t.Key == nameof(s.Name) && t.LanguageId == LanguageId && t.GroupKey == StringResourceEnums.NavBarSubItem).FirstOrDefault().Value,
                            UpdatedDate = s.UpdatedDate,
                            Url = s.Url
                        }).ToList()
                    }).ToListAsync();
                if (navbarItems == null)
                {
                    return Error<List<NavBarItemDTO>>("NavBar item not found", HttpStatusCode.NotFound);
                }
                return Success(navbarItems);
            }
            catch (Exception ex)
            {
                return Error<List<NavBarItemDTO>>(ex);
            }

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

        public async Task<ResponseResult<List<NavBarItemDTO>>> Create(List<NavBarItemDTO> dtos)
        {
            try
            {
                var defultLanguage = await _dbContext.Languages.FirstAsync(x => x.IsDefault);
                var defultModel = dtos.Where(x => x.LangId == defultLanguage.Id).FirstOrDefault();
                var all = _mapper.Map<List<NavBarItem>>(dtos);
                var navBarItem = _mapper.Map<NavBarItemDTO, NavBarItem>(defultModel);
                var result = await genericService.CreateAync(navBarItem);
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
                }
                return Success(dtos);
            }
            catch (Exception ex)
            {
                return Error<List<NavBarItemDTO>>(ex);
            }
        }

        public async Task<ResponseResult<NavBarItemDTO>> Update(NavBarItemDTO dto)
        {
            try
            {
                var navBarItem = await _dbContext.NavBarItems
                    .FirstOrDefaultAsync(x => x.Id == dto.Id && !x.IsDeleted);

                if (navBarItem == null)
                {
                    return Error<NavBarItemDTO>("NavBar item not found", HttpStatusCode.NotFound);
                }

                navBarItem = _mapper.Map(dto, navBarItem);
                _dbContext.Update(navBarItem);

                await _dbContext.SaveChangesAsync();

                return Success(dto);
            }
            catch (Exception ex)
            {
                return Error<NavBarItemDTO>(ex);
            }
        }

        public async Task<ResponseResult<NavBarItemDTO>> Delete(int id)
        {
            try
            {
                var navBarItem = await _dbContext.NavBarItems
                    .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

                if (navBarItem == null)
                {
                    return Error<NavBarItemDTO>("NavBar item not found", HttpStatusCode.NotFound);
                }

                navBarItem.IsDeleted = true;
                var translations = await _dbContext.StringResources.Where(x => x.ResourceId == id && x.GroupKey == StringResourceEnums.NavBarItem).ToListAsync();
                foreach (var item in translations)
                {
                    item.IsDeleted = true;
                    _dbContext.Update(item);
                }
                _dbContext.Update(navBarItem);
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
