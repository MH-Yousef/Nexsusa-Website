//using AutoMapper;
//using Core.Domains.Enums;
//using Core.HomePage.HomePageItems;
//using Data.Context;
//using Data.Dtos.ContactUsDTOs;
//using Microsoft.EntityFrameworkCore;
//using Services._Base;
//using Services._GenericServices;
//using Services.LanguageServices;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Services.HomePageServices.ContactUsServices
//{
//    public class ContactUsService : BaseService, IContactUsService
//    {
//        private readonly GenericService<ContactUs> _genericService;
//        private readonly ILanguageService _languageService;
//        public ContactUsService(AppDbContext dbContext, IMapper mapper, GenericService<ContactUs> genericService ,ILanguageService languageService) : base(dbContext, mapper)
//        {
//            _genericService = genericService;
//            _languageService = languageService;
//        }

//        public Task<ResponseResult<ContactUsDTO>> Delete(int id)
//        {
//            throw new NotImplementedException();
//        }

//        public async Task<ResponseResult<ContactUsDTO>> GetById(int id, int langId)
//        {
//            try
//            {
//                var entities = await _genericService.GetByIdAsync(id,langId, StringResourceEnums.ContactUs);
//                if (entities == null)
//                {
//                    return Error<ContactUsDTO>("Contact Us List Not Found...");
//                }
//                var dtos = _mapper.Map<ContactUsDTO>(entities);
//                return Success<ContactUsDTO>();
//            }
//            catch (Exception ex)
//            {

//                return Error<ContactUsDTO>(ex);
//            }
//        }

//        public async Task<ResponseResult<List<ContactUsDTO>>> GetList(int langId)
//        {
//            try
//            {
//                var entities = await _genericService.GetListAsync(langId, StringResourceEnums.ContactUs);
//                if (entities == null)
//                {
//                    return Error<List<ContactUsDTO>>("Contact Us List Not Found...");
//                }
//                var dtos = _mapper.Map<ContactUsDTO>(entities);
//                return Success<List<ContactUsDTO>>();
//            }
//            catch (Exception ex)
//            {

//                return Error<List<ContactUsDTO>>(ex);
//            }
            
//        }

//        public async Task<ResponseResult<List<ContactUsDTO>>> Manage(List<ContactUsDTO> dto)
//        {
//            try
//            {
           
//                var defaultLanguage = await _dbContext.Languages.FirstOrDefaultAsync(x => x.IsDefault);
//                var defaultDto= dto.FirstOrDefault(x => x.LangId == defaultLanguage.Id);
//                var defaultEntity = _mapper.Map<ContactUs>(defaultDto);
//                if (defaultDto.Id > 0)
//                {
//                    var result = await _genericService.UpdateAsync(defaultEntity);
//                }
//                else
//                {
//                    var result = await _genericService.CreateAsync(defaultEntity);
//                }
//                foreach (var item in dto)
//                {
                    
//                    var langId= item.LangId;
//                    var entity=_mapper.Map<ContactUs>(item);
//                    var translations = new List<(string ColumnName, string ColumnValue)>
//                            {
//                                ("Title", item.Title),
//                                ("Description", item.Description),
//                            };
//                    if (item.Id > 0)
//                    {
//                        await _genericService.UpdateTranslationsAsync(StringResourceEnums.ContactUs, translations, defaultEntity.Id, item.LangId);
//                    }
//                    else
//                    {
//                        await _genericService.AddTranslationsAsync(StringResourceEnums.ContactUs, translations, defaultEntity.Id, item.LangId);
//                    }

//                }
//                return Success<List<ContactUsDTO>>();
//            }
//            catch (Exception ex)
//            {

//                return Error<List<ContactUsDTO>>(ex);
//            }
//        }
//    }
//}
