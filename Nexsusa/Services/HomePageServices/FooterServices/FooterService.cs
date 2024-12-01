using AutoMapper;
using Core.Domains.Enums;
using Core.HomePage;
using Core.HomePage.HomePageItems;
using Data.Context;
using Data.Dtos.FooterDTOs;
using Data.Dtos.FooterServiceDTOs;
using Data.Dtos.HomePageDTOs;
using Data.Dtos.QuickLinkDTOs;
using Data.Dtos.SliderDTOs;
using Microsoft.EntityFrameworkCore;
using Services._Base;
using Services._GenericServices;
using Services.HomePageServices.ServiceServices;
using Services.ImageServices;
using System.Net;

namespace Services.HomePageServices.FooterServices
{
    public class FooterService(AppDbContext dbContext, IMapper mapper, GenericService<Footer> genericService, IImageService _imageService, IServiceService _serviceService) : BaseService(dbContext, mapper), IFooterService
    {
        private readonly GenericService<Footer> _genericService = genericService;
        //private readonly IImageService _imageService;
        //private readonly IServiceService _serviceService;
        public async Task<ResponseResult<FooterDTO>> Get(int LanguageId)
        {
            try
            {
                var entity = await _dbContext.Footers.AsNoTracking().FirstOrDefaultAsync();
                var defaultLanguage = await _dbContext.Languages.AsNoTracking().FirstOrDefaultAsync(x => x.IsDefault);

                if (entity == null)
                {
                    return Error<FooterDTO>("Footer Not Found...");

                }
                if (LanguageId != defaultLanguage.Id)
                {

                    entity = _genericService.ApplyTranslations(entity, LanguageId, entity.Id, StringResourceEnums.Footer);
                }

                var dto = _mapper.Map<FooterDTO>(entity);
                dto.LangId = LanguageId;
                dto.Services = (await _serviceService.GetList(LanguageId)).Data;
                return Success(dto);
            }
            catch (Exception ex)
            {

                return Error<FooterDTO>(ex);
            }

        }
        public async Task<ResponseResult<FooterDTO>> Manage(FooterDTO dto)
        {
            try
            {
                var defaultLanguage = await _dbContext.Languages.AsNoTracking().FirstOrDefaultAsync(x => x.IsDefault);
                bool isDefault = defaultLanguage.Id == dto.LangId;
                var entity = _mapper.Map<Footer>(dto);
                if (dto.File != null)
                {
                    var imageResult = await _imageService.UploadImage(dto.File);
                    entity.ImageUrl = imageResult;
                    dto.ImageUrl = imageResult;
                }
                if (isDefault)
                {
                    if (dto.Id == 0)
                        await _genericService.CreateAsync(entity);
                    else
                        await _genericService.UpdateAsync(entity);
                }
                else
                {
                    bool hasTranslation = _genericService.HasTranslations(entity, dto.LangId, dto.Id, StringResourceEnums.Footer);
                    List<(string ColumnName, string ColumnValue)> transalteValues =
                [
                         new()
                        {
                            ColumnName="Description",
                            ColumnValue=entity.Description
                        },
                          new()
                        {
                            ColumnName="Address",
                            ColumnValue=entity.Address
                        }
                ];
                    if (hasTranslation)
                    {
                        await _genericService.UpdateTranslationsAsync(StringResourceEnums.Footer, transalteValues, dto.Id, dto.LangId);
                    }
                    else
                    {
                        await _genericService.AddTranslationsAsync(StringResourceEnums.Footer, transalteValues, dto.Id, dto.LangId);

                    }
                }
                return Success(dto);
            }
            catch (Exception ex)
            {

                return Error<FooterDTO>(ex);
            }
        }

        public async Task<ResponseResult<FooterDTO>> GetFirst()
        {
            try
            {
                var result = await _dbContext.Footers.FirstOrDefaultAsync();
                var Dto = _mapper.Map<Footer, FooterDTO>(result);
                return Success(Dto);

            }
            catch (Exception ex)
            {
                return Error<FooterDTO>(ex);
            }
        }
    }
}
