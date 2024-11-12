using AutoMapper;
using Core.Domains.Enums;
using Core.Domains.Languages;
using Core.HomePage.HomePageItems;
using Data.Context;
using Data.Dtos.NavBarDTOs;
using Data.Dtos.RegularBlogsDTOs;
using Microsoft.EntityFrameworkCore;
using Services._Base;
using Services._GenericServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Services.HomePageServices.RegularBlogsServices
{
    public class RegularBlogsService : BaseService, IRegularBlogsService
    {
        private readonly GenericService<RegularBlogs> _genericService;
        public RegularBlogsService(AppDbContext dbContext, IMapper mapper, GenericService<RegularBlogs> genericService) : base(dbContext, mapper)
        {
            _genericService = genericService;
        }

        public async Task<ResponseResult<List<RegularBlogsDTO>>> GetList(int langId)
        {
            try
            {
                // Get list of regular blogs with the related sub items or translations.
                var regularBlogs = await _genericService.GetListAsync(langId, StringResourceEnums.RegularBlogs, x => x.RegularBlogsItems);
                var dto = _mapper.Map<List<RegularBlogs>, List<RegularBlogsDTO>>(regularBlogs.Data);

                foreach (var item in dto)
                {
                    item.LangId = langId;
                    if (item.RegularBlogsItems != null)
                    {
                        foreach (var sub in item.RegularBlogsItems)
                        {
                            sub.LangId = langId;
                            sub.Title = _genericService.ApplyTranslations<RegularBlogsItemDTO>(sub, langId, sub.Id, StringResourceEnums.RegularBlogsItem).Title;
                            sub.Content = _genericService.ApplyTranslations<RegularBlogsItemDTO>(sub, langId, sub.Id, StringResourceEnums.RegularBlogsItem).Content;
                            
                        }
                    }
                }

                return Success(dto);
            }
            catch (Exception ex)
            {
                return Error<List<RegularBlogsDTO>>(ex);
            }
        }

        public async Task<ResponseResult<RegularBlogsDTO>> GetById(int id, int langId)
        {
            try
            {
                var blog = await _genericService.GetByIdAsync(id, langId, StringResourceEnums.RegularBlogs, x => x.RegularBlogsItems);
                var dto = _mapper.Map<RegularBlogs, RegularBlogsDTO>(blog);

                if (dto.RegularBlogsItems != null)
                {
                    foreach (var item in dto.RegularBlogsItems)
                    {
                        item.LangId = langId;
                        item.Title = _genericService.ApplyTranslations<RegularBlogsItemDTO>(item, langId, item.Id, StringResourceEnums.RegularBlogsItem).Title;
                        item.Content = _genericService.ApplyTranslations<RegularBlogsItemDTO>(item, langId, item.Id, StringResourceEnums.RegularBlogsItem).Content;
                    }
                }

                return Success(dto);
            }
            catch (Exception ex)
            {
                return Error<RegularBlogsDTO>(ex);
            }
        }

        public async Task<ResponseResult<List<RegularBlogsDTO>>> Manage(List<RegularBlogsDTO> dtos)
        {
            try
            {
                var defaultLanguage = await _dbContext.Languages.FirstAsync(x => x.IsDefault);
                var defaultModel = dtos.FirstOrDefault(x => x.LangId == defaultLanguage.Id);
                var regularBlog = _mapper.Map<RegularBlogsDTO, RegularBlogs>(defaultModel);

                if (defaultModel.Id > 0)
                {
                    await _genericService.UpdateAsync(regularBlog);
                }
                else
                {
                    await _genericService.CreateAsync(regularBlog);
                }

                // Add Translations for RegularBlog
                foreach (var item in dtos)
                {
                    var translations = new List<(string ColumnName, string ColumnValue)>
                    {
                        ("Title", item.Title),
                        ("Description", item.Description)
                    };

                    if (item.Id > 0)
                    {
                        await _genericService.UpdateTranslationsAsync(StringResourceEnums.RegularBlogs, translations, regularBlog.Id, item.LangId);
                    }
                    else
                    {
                        await _genericService.AddTranslationsAsync(StringResourceEnums.RegularBlogs, translations, regularBlog.Id, item.LangId);
                    }

                    // Add Translations for RegularBlogItems
                    if (item.RegularBlogsItems.Any())
                    {
                        foreach (var subItem in item.RegularBlogsItems)
                        {
                            var subBlog = await _dbContext.RegularBlogsItems.FirstOrDefaultAsync(x => x.Id == regularBlog.Id);
                            var subTranslations = new List<(string ColumnName, string ColumnValue)>
                            {
                                ("Title", subItem.Title),
                                ("Content", subItem.Content),
                            };

                            if (subItem.Id > 0)
                            {
                                await _genericService.UpdateTranslationsAsync(StringResourceEnums.RegularBlogsItem, subTranslations, subBlog.Id, subItem.LangId);
                            }
                            else
                            {
                                await _genericService.AddTranslationsAsync(StringResourceEnums.RegularBlogsItem, subTranslations, subBlog.Id, subItem.LangId);
                            }
                        }
                    }
                }

                return Success(dtos);
            }
            catch (Exception ex)
            {
                return Error<List<RegularBlogsDTO>>(ex);
            }
        }



        
    }

}
