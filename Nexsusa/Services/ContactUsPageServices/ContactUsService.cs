using AutoMapper;
using Core.ContactUsPage;
using Core.Domains.Enums;
using Data.Context;
using Data.Dtos.ContactUsDTOs;
using Microsoft.EntityFrameworkCore;
using Services._Base;
using Services._GenericServices;

namespace Services.ContactUsPageServices
{
    public class ContactUsService: BaseService, IContactUsService
    {
        private readonly GenericService<ContactUs> _genericService;
        public ContactUsService(AppDbContext dbContext, IMapper mapper, GenericService<ContactUs> genericService) : base(dbContext, mapper)
        {
            _genericService = genericService;
        }

        public async Task<ResponseResult<ContactUsDTO>> GetList(int languageId)
        {
            try
            {
                var Items = await _genericService.GetListAsync(languageId, StringResourceEnums.ContactUs);

                var ContactUsDTOs = _mapper.Map<List<ContactUs>, List<ContactUsDTO>>(Items.Data);

                foreach (var item in ContactUsDTOs)
                {
                    item.LangId = languageId;
                    item.Title = _genericService.ApplyTranslations<ContactUsDTO>(item, languageId, item.Id, StringResourceEnums.ContactUs).Title;
                    item.Description = _genericService.ApplyTranslations<ContactUsDTO>(item, languageId, item.Id, StringResourceEnums.ContactUs).Description;
                }

                return Success(ContactUsDTOs.FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Error<ContactUsDTO>(ex);
            }
        }

        public async Task<ResponseResult<ContactUsDTO>> GetById(int id, int languageId)
        {
            try
            {
                var ContactUs = await _genericService.GetByIdAsync(id, languageId, StringResourceEnums.ContactUs);

                var ContactUsDTO = _mapper.Map<ContactUs, ContactUsDTO>(ContactUs);

                // Apply translations for the ContactUs
                
                var translate = _genericService.ApplyTranslations<ContactUsDTO>(ContactUsDTO, languageId, ContactUsDTO.Id, StringResourceEnums.ContactUs);
                ContactUsDTO.LangId = languageId;
                ContactUsDTO.Title = translate.Title;
                ContactUsDTO.Description = translate.Description;

                return Success(ContactUsDTO);
            }
            catch (Exception ex)
            {
                return Error<ContactUsDTO>(ex);
            }
        }

        public async Task<ResponseResult<ContactUsDTO>> Manage(ContactUsDTO dto)
        {
            try
            {
                var defaultLanguage = await _dbContext.Languages.FirstAsync(x => x.IsDefault);
                bool isDefualtExists = await _dbContext.ContactUss.AnyAsync();
                bool IsDefultModel = dto.LangId == defaultLanguage.Id;
                bool IsTranslateionExixts = await _dbContext.StringResources.AnyAsync(x => x.ResourceId == dto.Id && x.GroupKey == StringResourceEnums.ContactUs && x.LanguageId == dto.LangId);
                var mainItem = _mapper.Map<ContactUsDTO, ContactUs>(dto);

                if (IsDefultModel)
                {
                    if (dto.Id > 0)
                    {
                        await _genericService.UpdateAsync(mainItem);
                    }
                    else
                    {
                        await _genericService.CreateAsync(mainItem);
                    }
                    var translations = new List<(string ColumnName, string ColumnValue)>
                            {
                                ("Title", dto.Title),
                                ("Description", dto.Description)
                            };

                    if (dto.Id > 0)
                    {
                        await _genericService.UpdateTranslationsAsync(StringResourceEnums.ContactUs, translations, mainItem.Id, dto.LangId);
                    }
                    else
                    {
                        await _genericService.AddTranslationsAsync(StringResourceEnums.ContactUs, translations, mainItem.Id, dto.LangId);
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
                            await _genericService.UpdateTranslationsAsync(StringResourceEnums.ContactUs, translations, dto.Id, dto.LangId);
                        }
                        else
                        {
                            await _genericService.AddTranslationsAsync(StringResourceEnums.ContactUs, translations, dto.Id, dto.LangId);
                        }


                    }
                    else
                    {
                        return Error<ContactUsDTO>("Only default language can be managed first");
                    }
                }
                return Success(dto);
            }
            catch (Exception ex)
            {
                return Error<ContactUsDTO>(ex);
            }
        }

        public async Task<ResponseResult<ContactUsDTO>> GetFirst()
        {
            try
            {
                var result = await _dbContext.ContactUss.FirstOrDefaultAsync();
                var ContactUsDTO = _mapper.Map<ContactUs, ContactUsDTO>(result);
                return Success(ContactUsDTO);

            }
            catch (Exception ex)
            {
                return Error<ContactUsDTO>(ex);
            }
        }
    }
}
