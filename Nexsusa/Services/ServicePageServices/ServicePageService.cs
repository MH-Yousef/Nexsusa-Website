using AutoMapper;
using Core.AboutPage;
using Core.Domains.Enums;
using Core.HomePage.HomePageItems;
using Core.ServicesPage;
using Data.Context;
using Data.Dtos.ClientSaysDTOs;
using Data.Dtos.ClientSaysItemDTOs;
using Data.Dtos.ServicePageDTOs;
using Microsoft.EntityFrameworkCore;
using Services._Base;
using Services._GenericServices;

namespace Services.ServicePageServices
{
    public class ServicePageService : BaseService, IServicePageService
    {
        private readonly GenericService<ServicePage> genericService;
        public ServicePageService(AppDbContext dbContext, IMapper mapper, GenericService<ServicePage> genericService) : base(dbContext, mapper)
        {
            this.genericService = genericService;
        }

        public async Task<ResponseResult<ServicePageDTO>> Get(int languageId)
        {
            try
            {
                //var firstItam = await _serviceService.GetFirst();
                var items = await genericService.GetListAsync(languageId, StringResourceEnums.ServicePage);
                var dto = _mapper.Map<List<ServicePage>, List<ServicePageDTO>>(items.Data);
                return Success(dto.FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Error<ServicePageDTO>(ex);
            }
        }

        public async Task<ResponseResult<List<ServicePageDTO>>> Manage(List<ServicePageDTO> dtos)
        {
            try
            {
                var defultLanguage = await _dbContext.Languages.FirstAsync(x => x.IsDefault);
                var defultModel = dtos.Where(x => x.LangId == defultLanguage.Id).FirstOrDefault();
                var mainItem = _mapper.Map<ServicePageDTO, ServicePage>(defultModel);
                if (defultModel.Id > 0)
                {
                    var result = await genericService.UpdateAsync(mainItem);
                }
                else
                {
                    var result = await genericService.CreateAsync(mainItem);
                }

                foreach (var item in dtos)
                {
                    var translations = new List<(string ColumnName, string ColumnValue)>
                            {
                                ("Title", item.Title),
                            };
                    if (item.Id > 0)
                    {
                        await genericService.UpdateTranslationsAsync(StringResourceEnums.ServicePage, translations, mainItem.Id, item.LangId);
                    }
                    else
                    {
                        await genericService.AddTranslationsAsync(StringResourceEnums.ServicePage, translations, mainItem.Id, item.LangId);
                    }
                }

                return Success(dtos);
            }
            catch (Exception ex)
            {
                return Error<List<ServicePageDTO>>(ex);
            }
        }

        public async Task<ResponseResult<ServicePageDTO>> GetById(int id, int LanguageId)
        {
            try
            {
                var model = await genericService.GetByIdAsync(id, LanguageId, StringResourceEnums.ServicePage);
                var dto = _mapper.Map<ServicePage, ServicePageDTO>(model);
                return Success(dto);
            }
            catch (Exception ex)
            {
                return Error<ServicePageDTO>(ex);
            }
        }
    }
}
