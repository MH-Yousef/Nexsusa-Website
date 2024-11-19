using AutoMapper;
using Core.Domains.Enums;
using Core.HomePage.HomePageItems;
using Data.Context;
using Data.Dtos.ServiceDTOs;
using Microsoft.EntityFrameworkCore;
using Services._Base;
using Services._GenericServices;
using Services.ImageServices;

namespace Services.HomePageServices.ServiceServices
{
    public class ServiceService : BaseService, IServiceService
    {
        private readonly GenericService<Service> _genericService;
        private readonly IImageService _imageService;
        public ServiceService(AppDbContext dbContext, IMapper mapper, GenericService<Service> genericService, IImageService imageService) : base(dbContext, mapper)
        {
            _genericService = genericService;
            _imageService = imageService;
        }

        public async Task<ResponseResult<List<ServiceDTO>>> GetList(int langId)
        {
            try
            {
                var services = await _genericService.GetListAsync(langId, StringResourceEnums.Service, x => x.ServiceItems);
                var dto = _mapper.Map<List<Service>, List<ServiceDTO>>(services.Data);

                // Iterate through the services and their items for translations
                foreach (var service in dto)
                {
                    service.LangId = langId;

                    if (service.ServiceItems != null)
                    {
                        foreach (var item in service.ServiceItems)
                        {
                            item.LangId = langId;
                            item.Title = _genericService.ApplyTranslations<ServiceItemDTO>(item, langId, item.Id, StringResourceEnums.ServiceItem).Title;
                            item.Description = _genericService.ApplyTranslations<ServiceItemDTO>(item, langId, item.Id, StringResourceEnums.ServiceItem).Description;
                        }
                    }
                }

                return Success(dto);
            }
            catch (Exception ex)
            {
                return Error<List<ServiceDTO>>(ex);
            }
        }

        public async Task<ResponseResult<ServiceDTO>> GetById(int id, int langId)
        {
            try
            {
                var service = await _genericService.GetByIdAsync(id, langId, StringResourceEnums.Service, x => x.ServiceItems);
                var dto = _mapper.Map<Service, ServiceDTO>(service);

                if (dto.ServiceItems != null)
                {
                    foreach (var item in dto.ServiceItems)
                    {
                        item.LangId = langId;
                        item.Title = _genericService.ApplyTranslations<ServiceItemDTO>(item, langId, item.Id, StringResourceEnums.ServiceItem).Title;
                        item.Description = _genericService.ApplyTranslations<ServiceItemDTO>(item, langId, item.Id, StringResourceEnums.ServiceItem).Description;
                    }
                }

                return Success(dto);
            }
            catch (Exception ex)
            {
                return Error<ServiceDTO>(ex);
            }
        }

        public async Task<ResponseResult<ServiceDTO>> Manage(ServiceDTO dto)
        {
            try
            {
                var defaultLanguage = await _dbContext.Languages.FirstAsync(x => x.IsDefault);
                bool isDefualtExists = await _dbContext.Services.AnyAsync();
                bool IsDefultModel = dto.LangId == defaultLanguage.Id;
                bool IsTranslateionExixts = await _dbContext.StringResources.AnyAsync(x => x.ResourceId == dto.Id && x.GroupKey == StringResourceEnums.Service && x.LanguageId == dto.LangId);
                var service = _mapper.Map<ServiceDTO, Service>(dto);
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
                        await _genericService.UpdateTranslationsAsync(StringResourceEnums.Service, translations, service.Id, dto.LangId);
                    }
                    else
                    {
                        await _genericService.AddTranslationsAsync(StringResourceEnums.Service, translations, service.Id, dto.LangId);
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
                            await _genericService.UpdateTranslationsAsync(StringResourceEnums.Service, translations, dto.Id, dto.LangId);
                        }
                        else
                        {
                            await _genericService.AddTranslationsAsync(StringResourceEnums.Service, translations, dto.Id, dto.LangId);
                        }


                    }
                    else
                    {
                        return Error<ServiceDTO>("Only default language can be managed first");
                    }
                }

                return Success(dto);
            }
            catch (Exception ex)
            {
                return Error<ServiceDTO>(ex);
            }
        }
        // Mabnage SubItem
        public async Task<ResponseResult<ServiceItemDTO>> ManageSubItem(ServiceItemDTO subDto)
        {
            try
            {
                var defaultLanguage = await _dbContext.Languages.FirstAsync(x => x.IsDefault);
                bool IsTranslateionExixts = await _dbContext.StringResources.AnyAsync(x => x.ResourceId == subDto.Id && x.GroupKey == StringResourceEnums.ServiceItem && x.LanguageId == subDto.LangId);
                var subService = _mapper.Map<ServiceItemDTO, ServiceItem>(subDto);
                if (subDto.File != null)
                {
                    var imageResult = await _imageService.UploadImage(subDto.File);
                    subService.IconUrl = imageResult;
                }
                if (subDto.LangId == defaultLanguage.Id)
                {
                    if (subDto.Id > 0)
                    {
                        await _genericService.UpdateAsync(subService);
                    }
                    else
                    {
                        await _genericService.CreateAsync(subService);
                    }
                }
                var subTranslations = new List<(string ColumnName, string ColumnValue)>
                            {
                                ("Title", subDto.Title),
                                ("Description", subDto.Description)
                             };
                if (subDto.Id > 0 && IsTranslateionExixts)
                {
                    await _genericService.UpdateTranslationsAsync(StringResourceEnums.ServiceItem, subTranslations, subService.Id, subDto.LangId);
                }
                else
                {
                    await _genericService.AddTranslationsAsync(StringResourceEnums.ServiceItem, subTranslations, subService.Id, subDto.LangId);
                }
                return Success(subDto);
            }
            catch (Exception ex)
            {
                return Error<ServiceItemDTO>(ex);
            }
        }
        public async Task<ResponseResult<ServiceDTO>> GetFirst()
        {
            try
            {
                var result = await _dbContext.Services.Include(x => x.ServiceItems).FirstOrDefaultAsync();
                var serviceDto = _mapper.Map<ServiceDTO>(result);
                return Success(serviceDto);
            }
            catch (Exception ex)
            {
                return Error<ServiceDTO>(ex);
            }
        }

    }

}
