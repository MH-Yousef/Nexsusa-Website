using AutoMapper;
using Core.ContactUsPage;
using Core.Domains.Enums;
using Core.Domains.Languages;
using Core.HomePage.HomePageItems;
using Data.Context;
using Data.Dtos.ClientSaysDTOs;
using Data.Dtos.ClientSaysItemDTOs;
using Data.Dtos.ContactUsDTOs;
using Microsoft.EntityFrameworkCore;
using Services._Base;
using Services._GenericServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ContactUsPageServices
{
    public class ContactUsPageService : BaseService, IContactUsPageService
    {
        private readonly GenericService<ContactUsPage> _genericService;
        public ContactUsPageService(AppDbContext dbContext, IMapper mapper, GenericService<ContactUsPage> genericService) : base(dbContext, mapper)
        {
            _genericService = genericService;
        }

        public async Task<ResponseResult<ContactUsPageDTO>> Get(int langId)
        {
            try
            {
                var entites = await _genericService.GetListAsync(langId, StringResourceEnums.ContactUsPage, x => x.ContactUsItems);
                if (entites == null)
                {
                    return Error<ContactUsPageDTO>("Not Found...");
                }

                var dto = _mapper.Map<List<ContactUsPage>, List<ContactUsPageDTO>>(entites.Data);
                foreach (var item in dto)
                {
                    item.LangId = langId;
                    if (item.ContactUsItems != null)
                    {
                        foreach (var sub in item.ContactUsItems)
                        {
                            sub.LangId = langId;
                            sub.Title = _genericService.ApplyTranslations<ContactUsItemDTO>(sub, langId, sub.Id, StringResourceEnums.ContactUsItem).Title;
                        }
                    }
                }
                //foreach (var item in dto)
                //{
                //    item.ContactUs = _genericService.ApplyTranslations(item.ContactUs, langId, item.ContactUs.Id, StringResourceEnums.ContactUs);
                //}
                return Success(dto.FirstOrDefault());

            }
            catch (Exception ex)
            {
                return Error<ContactUsPageDTO>(ex);


            }
        }

        public async Task<ResponseResult<ContactUsPageDTO>> GetById(int id, int langId)
        {
            try
            {
                var entity = await _genericService.GetByIdAsync(id, langId, StringResourceEnums.ContactUsPage, x => x.ContactUsItems);
                if (entity == null)
                {
                    return Error<ContactUsPageDTO>("Contact Us Page Not Found...");
                }
                var dto = _mapper.Map<ContactUsPage, ContactUsPageDTO>(entity);
                return Success<ContactUsPageDTO>(dto);
            }
            catch (Exception ex)
            {

                return Error<ContactUsPageDTO>(ex);
            }
        }

        public async Task<ResponseResult<ContactUsPageDTO>> GetFirst()
        {
            try
            {
                var entity = await _dbContext.ContactUsPages.Include(x => x.ContactUsItems).FirstOrDefaultAsync();
                if (entity == null)
                {
                    return Error<ContactUsPageDTO>("There is no Contact Us Pages...");
                }
                var dto = _mapper.Map<ContactUsPage, ContactUsPageDTO>(entity);
                return Success<ContactUsPageDTO>(dto);
            }
            catch (Exception ex)
            {

                return Error<ContactUsPageDTO>(ex);
            }
        }

        public async Task<ResponseResult<ContactUsPageDTO>> Manage(ContactUsPageDTO dto)
        {
            try
            {
                // Varsayılan dilin kontrolü
                var defaultLanguage = await _dbContext.Languages.FirstAsync(x => x.IsDefault);
                bool isDefaultExists = await _dbContext.ContactUsPages.AnyAsync();
                bool isDefaultModel = dto.LangId == defaultLanguage.Id;
                bool isTranslationExists = await _dbContext.StringResources
                    .AnyAsync(x => x.ResourceId == dto.Id && x.GroupKey == StringResourceEnums.ContactUsPage && x.LanguageId == dto.LangId);

                // DTO'yu entity'ye dönüştürme
                var mainItem = _mapper.Map<ContactUsPageDTO, ContactUsPage>(dto);

                // Varsayılan dildeki yönetim işlemleri
                if (isDefaultModel)
                {
                    // Varsayılan dilde bir item varsa güncelleme, yoksa oluşturma
                    if (dto.Id > 0)
                    {
                        await _genericService.UpdateAsync(mainItem);
                    }
                    else
                    {
                        await _genericService.CreateAsync(mainItem);
                    }

                    // Çeviriler için gerekli verileri hazırlama
                    var translations = new List<(string ColumnName, string ColumnValue)>
            {
                ("Title", dto.Title),
                ("SubTitle", dto.SubTitle),
                ("Description", dto.Description)
            };

                    // Çeviri ekleme veya güncelleme
                    if (dto.Id > 0)
                    {
                        await _genericService.UpdateTranslationsAsync(StringResourceEnums.ContactUsPage, translations, mainItem.Id, dto.LangId);
                    }
                    else
                    {
                        await _genericService.AddTranslationsAsync(StringResourceEnums.ContactUsPage, translations, mainItem.Id, dto.LangId);
                    }

                    // Contact Us Items (alt öğeler) için de işlemleri tekrarlama
                    foreach (var item in dto.ContactUsItems)
                    {
                        var contactUsItemEntity = _mapper.Map<ContactUsItemDTO, ContactUsItem>(item);
                        contactUsItemEntity.ContactUsPageId = mainItem.Id;

                        // Eğer Contact Us Item id > 0 ise, sadece güncelleme yapılır
                        if (item.Id > 0)
                        {
                            await _genericService.UpdateAsync(contactUsItemEntity);
                        }
                        else
                        {
                            // Eğer Contact Us Item id == 0 ise, yeni oluşturma yapılır
                            await _genericService.CreateAsync(contactUsItemEntity);
                        }

                        // Contact Us Item için çeviri işlemleri
                        var itemTranslations = new List<(string ColumnName, string ColumnValue)>
                {
                    ("Title", item.Title),
                    ("ContactDetail", item.ContactDetail)
                };

                        // Çeviriyi güncelleme veya ekleme
                        if (item.Id > 0 && isTranslationExists)
                        {
                            await _genericService.UpdateTranslationsAsync(StringResourceEnums.ContactUsItem, itemTranslations, contactUsItemEntity.Id, item.LangId);
                        }
                        else
                        {
                            await _genericService.AddTranslationsAsync(StringResourceEnums.ContactUsItem, itemTranslations, contactUsItemEntity.Id, item.LangId);
                        }
                    }
                }
                else
                {
                    // Varsayılan dilde bir içerik yoksa, sadece çeviriler yapılabilir
                    if (isDefaultExists)
                    {
                        var translations = new List<(string ColumnName, string ColumnValue)>
                {
                    ("Title", dto.Title),
                    ("SubTitle", dto.SubTitle),
                    ("Description", dto.Description)
                };

                        if (dto.Id > 0 && isTranslationExists)
                        {
                            await _genericService.UpdateTranslationsAsync(StringResourceEnums.ContactUsPage, translations, dto.Id, dto.LangId);
                        }
                        else
                        {
                            await _genericService.AddTranslationsAsync(StringResourceEnums.ContactUsPage, translations, dto.Id, dto.LangId);
                        }

                        // Contact Us Items için de çeviri işlemleri
                        foreach (var item in dto.ContactUsItems)
                        {
                            var itemTranslations = new List<(string ColumnName, string ColumnValue)>
                    {
                        ("Title", item.Title),
                        ("ContactDetail", item.ContactDetail)
                    };

                            if (item.Id > 0 && isTranslationExists)
                            {
                                await _genericService.UpdateTranslationsAsync(StringResourceEnums.ContactUsItem, itemTranslations, item.Id, item.LangId);
                            }
                            else
                            {
                                await _genericService.AddTranslationsAsync(StringResourceEnums.ContactUsItem, itemTranslations, item.Id, item.LangId);
                            }
                        }
                    }
                    else
                    {
                        return Error<ContactUsPageDTO>("Only default language can be managed first");
                    }
                }

                return Success(dto);
            }
            catch (Exception ex)
            {
                return Error<ContactUsPageDTO>(ex);
            }
        }
        public async Task<ResponseResult<ContactUsItemDTO>> ManageSubItem(ContactUsItemDTO subDto)
        {
            try
            {
                var defaultLanguage = await _dbContext.Languages.FirstAsync(x => x.IsDefault);
                bool isTranslationExists = await _dbContext.StringResources.AnyAsync(x => x.ResourceId == subDto.Id && x.GroupKey == StringResourceEnums.ContactUsItem && x.LanguageId == subDto.LangId);

                // Map DTO to Entity
                var subItem = _mapper.Map<ContactUsItemDTO, ContactUsItem>(subDto);

                // Check if it's the default language to handle base item creation or update
                if (subDto.LangId == defaultLanguage.Id)
                {
                    if (subDto.Id > 0)
                    {
                        await _genericService.UpdateAsync(subItem);
                    }
                    else
                    {
                        await _genericService.CreateAsync(subItem);
                    }
                }

                // Translations for this ContactUsItem
                var subTranslations = new List<(string ColumnName, string ColumnValue)>
        {
            ("Title", subDto.Title),
            ("ContactDetail", subDto.ContactDetail)
        };

                // Add or update translations for the ContactUsItem
                if (subDto.Id > 0 && isTranslationExists)
                {
                    await _genericService.UpdateTranslationsAsync(StringResourceEnums.ContactUsItem, subTranslations, subItem.Id, subDto.LangId);
                }
                else
                {
                    await _genericService.AddTranslationsAsync(StringResourceEnums.ContactUsItem, subTranslations, subItem.Id, subDto.LangId);
                }

                return Success(subDto);
            }
            catch (Exception ex)
            {
                return Error<ContactUsItemDTO>(ex);
            }
        }
        public async Task<ResponseResult<ContactUsItemDTO>> GetSubItem(int id, int contactUsPageId, int languageId)
        {
            try
            {
                // Get the ContactUsItem by Id and LanguageId
                var subItem = await _dbContext.ContactUsItems
                    .Where(x => x.Id == id && x.ContactUsPageId == contactUsPageId)
                    .FirstOrDefaultAsync();

                if (subItem == null)
                {
                    return Error<ContactUsItemDTO>("Sub item not found.");
                }

                // Map the entity to DTO
                var subItemDto = _mapper.Map<ContactUsItem, ContactUsItemDTO>(subItem);

                // Get translations for this sub item
                var translation =  _genericService.ApplyTranslations<ContactUsItemDTO>(subItemDto, languageId, subItemDto.Id, StringResourceEnums.ContactUsItem);

                // Update the DTO with translated values
                subItemDto.Title = translation.Title;
                subItemDto.ContactDetail = translation.ContactDetail;

                return Success(subItemDto);
            }
            catch (Exception ex)
            {
                return Error<ContactUsItemDTO>(ex);
            }
        }



    }
}
