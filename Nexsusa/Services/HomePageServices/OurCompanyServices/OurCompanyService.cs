using AutoMapper;
using Core.Domains.Enums;
using Core.HomePage.HomePageItems;
using Data.Context;
using Data.Dtos.FooterDTOs;
using Data.Dtos.FooterServiceDTOs;
using Data.Dtos.OurCompanyDTOs;
using Data.Dtos.QuickLinkDTOs;
using Microsoft.EntityFrameworkCore;
using Services._Base;
using Services._GenericServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Services.HomePageServices.OurCompanyServices
{
    public class OurCompanyService : BaseService, IOurCompanyService
    {
        private readonly GenericService<OurCompany> genericService;
        public OurCompanyService(AppDbContext dbContext, IMapper mapper, GenericService<OurCompany> genericService) : base(dbContext, mapper)
        {
            this.genericService = genericService;
        }
        public async Task<ResponseResult<List<OurCompanyDTO>>> GetList(int LanguageId)
        {
            try
            {
                var items = await genericService.GetListAsync(LanguageId, StringResourceEnums.OurCompany);
                var dto = _mapper.Map<List<OurCompany>, List<OurCompanyDTO>>(items.Data);
                return Success(dto);
            }
            catch (Exception ex)
            {
                return Error<List<OurCompanyDTO>>(ex);
            }
        }

        public async Task<ResponseResult<OurCompanyDTO>> GetById(int id, int LanguageId)
        {
            try
            {
                var model = await genericService.GetByIdAsync(id, LanguageId, StringResourceEnums.OurCompany);
                var dto = _mapper.Map<OurCompany, OurCompanyDTO>(model);
                return Success(dto);
            }
            catch (Exception ex)
            {
                return Error<OurCompanyDTO>(ex);
            }
        }
        
        public async Task<ResponseResult<List<OurCompanyDTO>>> Manage(List<OurCompanyDTO> dtos)
        {
            try
            {
                var defultLanguage = await _dbContext.Languages.FirstAsync(x => x.IsDefault);
                var defultModel = dtos.Where(x => x.LangId == defultLanguage.Id).FirstOrDefault();
                var mainItem = _mapper.Map<OurCompanyDTO, OurCompany>(defultModel);
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
                        await genericService.UpdateTranslationsAsync(StringResourceEnums.OurCompany, translations, mainItem.Id, item.LangId);
                    }
                    else
                    {
                        await genericService.AddTranslationsAsync(StringResourceEnums.OurCompany, translations, mainItem.Id, item.LangId);
                    }
                }

                return Success(dtos);
            }
            catch (Exception ex)
            {
                return Error<List<OurCompanyDTO>>(ex);
            }
        }

        public Task<ResponseResult<OurCompanyDTO>> Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
