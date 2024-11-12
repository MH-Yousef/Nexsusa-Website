using AutoMapper;
using Core.Domains.Enums;
using Core.HomePage.HomePageItems;
using Data.Context;
using Data.Dtos.ServiceDTOs;
using Microsoft.EntityFrameworkCore;
using Services._Base;
using Services._GenericServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Services.HomePageServices.ServiceServices
{
    public class ServiceService : BaseService, IServiceService
    {
        private readonly GenericService<Service> _genericService;
        public ServiceService(AppDbContext dbContext, IMapper mapper, GenericService<Service> genericService) : base(dbContext, mapper)
        {
            _genericService = genericService;
        }

        public async Task<ResponseResult<List<ServiceDTO>>> Get(int langId)
        {
            try
            {
                var services = await _genericService.GetListAsync(langId, StringResourceEnums.Service, x => x.ServiceItem);
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

                            // Handle images if there are any
                            if (item.Images != null)
                            {
                                foreach (var image in item.Images)
                                {
                                    image.LangId = langId;
                                    

                                }
                            }
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

        // Get a service by Id
        public async Task<ResponseResult<ServiceDTO>> GetById(int id, int langId)
        {
            try
            {
                var service = await _genericService.GetByIdAsync(id, langId, StringResourceEnums.Service, x => x.ServiceItem);
                var dto = _mapper.Map<Service, ServiceDTO>(service);

                if (dto.ServiceItems != null)
                {
                    foreach (var item in dto.ServiceItems)
                    {
                        item.LangId = langId;
                        item.Title = _genericService.ApplyTranslations<ServiceItemDTO>(item, langId, item.Id, StringResourceEnums.ServiceItem).Title;
                        item.Description = _genericService.ApplyTranslations<ServiceItemDTO>(item, langId, item.Id, StringResourceEnums.ServiceItem).Description;

                        // Handle images if there are any
                        if (item.Images != null)
                        {
                            foreach (var image in item.Images)
                            {
                                image.LangId = langId;
                            }
                        }
                    }
                }

                return Success(dto);
            }
            catch (Exception ex)
            {
                return Error<ServiceDTO>(ex);
            }
        }

        public async Task<ResponseResult<List<ServiceDTO>>> Manage(List<ServiceDTO> dtos)
        {
            try
            {
                var defaultLanguage = await _dbContext.Languages.FirstAsync(x => x.IsDefault);
                var defaultModel = dtos.FirstOrDefault(x => x.LangId == defaultLanguage.Id);
                var service = _mapper.Map<ServiceDTO, Service>(defaultModel);

                if (defaultModel.Id > 0)
                {
                    await _genericService.UpdateAsync(service);
                }
                else
                {
                    await _genericService.CreateAsync(service);
                }

                // Add or Update Translations for Service
                foreach (var item in dtos)
                {
                    var translations = new List<(string ColumnName, string ColumnValue)>
                    {
                        ("Title", item.Title),
                        ("Description", item.Description)
                    };

                    if (item.Id > 0)
                    {
                        await _genericService.UpdateTranslationsAsync(StringResourceEnums.Service, translations, service.Id, item.LangId);
                    }
                    else
                    {
                        await _genericService.AddTranslationsAsync(StringResourceEnums.Service, translations, service.Id, item.LangId);
                    }

                    // Add or Update Translations for ServiceItems
                    if (item.ServiceItems != null && item.ServiceItems.Any())
                    {
                        foreach (var subItem in item.ServiceItems)
                        {
                            var subService = await _dbContext.ServiceItems.FirstOrDefaultAsync(x => x.ServicesId == service.Id);
                            var subTranslations = new List<(string ColumnName, string ColumnValue)>
                            {
                                ("Title", subItem.Title),
                                ("Description", subItem.Description)
                            };

                            if (subItem.Id > 0)
                            {
                                await _genericService.UpdateTranslationsAsync(StringResourceEnums.ServiceItem, subTranslations, subService.Id, subItem.LangId);
                            }
                            else
                            {
                                await _genericService.AddTranslationsAsync(StringResourceEnums.ServiceItem, subTranslations, subService.Id, subItem.LangId);
                            }

                         
                            }
                        }
                    }
                

                return Success(dtos);
            }
            catch (Exception ex)
            {
                return Error<List<ServiceDTO>>(ex);
            }
        }

    }

}
