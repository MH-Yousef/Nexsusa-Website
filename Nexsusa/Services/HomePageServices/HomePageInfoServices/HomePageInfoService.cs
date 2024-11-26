using AutoMapper;
using Core.Domains.Enums;
using Core.HomePage;
using Data.Context;
using Data.Dtos.HomePageDTOs;
using Microsoft.EntityFrameworkCore;
using Services._Base;
using Services._GenericServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.HomePageServices.HomePageInfoServices
{
    public class HomePageInfoService : BaseService, IHomePageInfoService
    {
        private readonly GenericService<HomePageInfoService> _genericService;
        public HomePageInfoService(AppDbContext dbContext, IMapper mapper, GenericService<HomePageInfoService> genericService = null) : base(dbContext, mapper)
        {
            _genericService = genericService;
        }

        public async Task<ResponseResult<HomePageInfoDTO>> GetHomePageInfoAsync(int langId)
        {
            try
            {
                var entity = await _dbContext.HomePageInfos.AsNoTracking().FirstOrDefaultAsync();
                var defaultLanguage = await _dbContext.Languages.AsNoTracking().FirstOrDefaultAsync(x => x.IsDefault);

                if (entity == null)
                {
                    return Error<HomePageInfoDTO>("Home Page Info Not Found...");

                }
                if (langId != defaultLanguage.Id)
                {

                  entity = _genericService.ApplyTranslations<HomePageInfo>(entity,langId,entity.Id,StringResourceEnums.HomePageInfo);
                }
               
               var dto=_mapper.Map<HomePageInfoDTO>(entity);
               dto.LangId= langId;
               return Success(dto);
            }
            catch (Exception ex)
            {

                return Error<HomePageInfoDTO>(ex);
            }
          
        }

        public async Task<ResponseResult<HomePageInfoDTO>> ManageAsync(HomePageInfoDTO dto)
        {
            try
            {
                var defaultLanguage = await _dbContext.Languages.AsNoTracking().FirstOrDefaultAsync(x => x.IsDefault);
                bool isDefault = defaultLanguage.Id == dto.LangId;
                var entity = _mapper.Map<HomePageInfo>(dto);
                if (isDefault)
                {
                    if (dto.Id == 0)
                        await _genericService.CreateAsync(entity);
                    else
                        await _genericService.UpdateAsync(entity);


                }
                else
                {
                    bool hasTranslation = _genericService.HasTranslations(entity, dto.LangId, dto.Id, StringResourceEnums.HomePageInfo);
                    List<(string ColumnName, string ColumnValue)> transalteValues = new()
                {
                      new()
                        {
                            ColumnName="Title",
                            ColumnValue=entity.Title
                        },
                         new()
                        {
                            ColumnName="MetaDescription",
                            ColumnValue=entity.MetaDescription
                        },
                          new()
                        {
                            ColumnName="MetaKeywords",
                            ColumnValue=entity.MetaKeywords
                        },
                           new()
                        {
                            ColumnName="MetaAuthor",
                            ColumnValue=entity.MetaAuthor
                        },
                            new()
                        {
                            ColumnName="MetaPublisher",
                            ColumnValue=entity.MetaPublisher
                        },
                             new()
                        {
                            ColumnName="PhoneNumber",
                            ColumnValue=entity.PhoneNumber
                        },
                             new()   {
                            ColumnName="Email",
                            ColumnValue=entity.Email
                        },
                };
                    if (hasTranslation)
                    {
                        await _genericService.UpdateTranslationsAsync(StringResourceEnums.HomePageInfo, transalteValues, dto.Id, dto.LangId);
                    }
                    else
                    {
                        await _genericService.AddTranslationsAsync(StringResourceEnums.HomePageInfo, transalteValues, dto.Id, dto.LangId);

                    }
                }
                    return Success(dto);
            }
            catch (Exception ex)
            {

                return Error<HomePageInfoDTO>(ex);
            }
           
              
            
          
            
        }
    }
}
