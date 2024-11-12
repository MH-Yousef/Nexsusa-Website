using AutoMapper;
using Core.Domains.Enums;
using Core.HomePage.HomePageItems;
using Data.Context;
using Data.Dtos.ClientSaysDTOs;
using Data.Dtos.OurCompanyDTOs;
using Data.Dtos.OurEmployeeDTOs;
using Microsoft.EntityFrameworkCore;
using Services._Base;
using Services._GenericServices;
using System.Net;

namespace Services.HomePageServices.OurEmployeeServices
{
    public class OurEmployeesService : BaseService, IOurEmployeesService
    {
        private readonly GenericService<OurEmployees> genericService;
        public OurEmployeesService(AppDbContext dbContext, IMapper mapper, GenericService<OurEmployees> genericService) : base(dbContext, mapper)
        {
            this.genericService = genericService;
        }

        public async Task<ResponseResult<List<OurEmployeesDTO>>> GetList(int LanguageId)
        {
            try
            {
                var items = await genericService.GetListAsync(LanguageId, StringResourceEnums.OurEmployee, x => x.OurEmployeesItems);
                var dto = _mapper.Map<List<OurEmployees>, List<OurEmployeesDTO>>(items.Data);
                return Success(dto);
            }
            catch (Exception ex)
            {
                return Error<List<OurEmployeesDTO>>(ex);
            }
        }

        public async Task<ResponseResult<OurEmployeesDTO>> GetById(int id, int LanguageId)
        {
            try
            {
                var model = await genericService.GetByIdAsync(id, LanguageId, StringResourceEnums.OurEmployee);
                var dto = _mapper.Map<OurEmployees, OurEmployeesDTO>(model);
                return Success(dto);
            }
            catch (Exception ex)
            {
                return Error<OurEmployeesDTO>(ex);
            }
        }

        public async Task<ResponseResult<List<OurEmployeesDTO>>> Manage(List<OurEmployeesDTO> dtos)
        {
            try
            {
                var defultLanguage = await _dbContext.Languages.FirstAsync(x => x.IsDefault);
                var defultModel = dtos.Where(x => x.LangId == defultLanguage.Id).FirstOrDefault();
                var mainItem = _mapper.Map<OurEmployeesDTO, OurEmployees>(defultModel);
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
                                ("Title", item.Title),
                                ("Description", item.Description),
                            };
                    if (item.Id > 0)
                    {
                        await genericService.UpdateTranslationsAsync(StringResourceEnums.OurEmployee, translations, mainItem.Id, item.LangId);
                    }
                    else
                    {
                        await genericService.AddTranslationsAsync(StringResourceEnums.OurEmployee, translations, mainItem.Id, item.LangId);
                    }
                }

                return Success(dtos);
            }
            catch (Exception ex)
            {
                return Error<List<OurEmployeesDTO>>(ex);
            }
        }

        public Task<ResponseResult<OurEmployeesDTO>> Delete(int id)
        {
            throw new NotImplementedException();
        }
    }

}
