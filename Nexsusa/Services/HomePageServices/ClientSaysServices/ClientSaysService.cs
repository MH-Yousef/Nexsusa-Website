using AutoMapper;
using Core.Domains.Enums;
using Core.HomePage.HomePageItems;
using Data.Context;
using Data.Dtos.ClientSaysDTOs;
using Data.Dtos.ClientSaysItemDTOs;
using Microsoft.EntityFrameworkCore;
using Services._Base;
using Services._GenericServices;
using System.Net;

namespace Services.HomePageServices.ClientSaysServices
{
    public class ClientSaysService : BaseService, IClientSaysService
    {
        private readonly GenericService<ClientSays> genericService;
        public ClientSaysService(AppDbContext dbContext, IMapper mapper, GenericService<ClientSays> genericService) : base(dbContext, mapper)
        {
            this.genericService = genericService;
        }
        public async Task<ResponseResult<List<ClientSaysDTO>>> GetList(int LanguageId)
        {

            try
            {
                var items = await genericService.GetListAsync(LanguageId, StringResourceEnums.ClientSays, x => x.ClientSaysItems);
                var dto = _mapper.Map<List<ClientSays>, List<ClientSaysDTO>>(items.Data);
                foreach (var item in dto)
                {
                    item.LangId = LanguageId;
                    if (item.ClientSaysItems != null)
                    {
                        foreach (var sub in item.ClientSaysItems)
                        {
                            sub.LangId = LanguageId;
                            sub.Description = genericService.ApplyTranslations<ClientSaysItemDTO>(sub, LanguageId, sub.Id, StringResourceEnums.ClientSaysItem).Description;
                        }
                    }
                }
                return Success(dto);
            }
            catch (Exception ex)
            {
                return Error<List<ClientSaysDTO>>(ex);
            }
        }

        public async Task<ResponseResult<ClientSaysDTO>> GetById(int id, int LanguageId)
        {
            try
            {
                var model = await genericService.GetByIdAsync(id, LanguageId, StringResourceEnums.NavBarItem, x => x.ClientSaysItems);
                var dto = _mapper.Map<ClientSays, ClientSaysDTO>(model);

                if (dto.ClientSaysItems != null)
                {
                    foreach (var item in dto.ClientSaysItems)
                    {
                        item.LangId = LanguageId;
                        item.Description = genericService.ApplyTranslations<ClientSaysItemDTO>(item, LanguageId, item.Id, StringResourceEnums.NavBarSubItem).Description;
                    }
                }

                return Success(dto);
            }
            catch (Exception ex)
            {
                return Error<ClientSaysDTO>(ex);
            }
        }

        public async Task<ResponseResult<List<ClientSaysDTO>>> Manage(List<ClientSaysDTO> dtos)
        {
            try
            {
                var defultLanguage = await _dbContext.Languages.FirstAsync(x => x.IsDefault);
                var defultModel = dtos.Where(x => x.LangId == defultLanguage.Id).FirstOrDefault();
                var mainItem = _mapper.Map<ClientSaysDTO, ClientSays>(defultModel);
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
                        await genericService.UpdateTranslationsAsync(StringResourceEnums.NavBarItem, translations, mainItem.Id, item.LangId);
                    }
                    else
                    {
                        await genericService.AddTranslationsAsync(StringResourceEnums.NavBarItem, translations, mainItem.Id, item.LangId);
                    }
                    if (item.ClientSaysItems.Any())
                    {

                        foreach (var subItem in item.ClientSaysItems)
                        {
                            var subItemModel = await _dbContext.ClientSaysItems.FirstOrDefaultAsync(x => x.ClientSaysId == mainItem.Id);
                            var Subtranslations = new List<(string ColumnName, string ColumnValue)>
                            {
                                ("Description", subItem.Description),
                            };
                            if (subItem.Id > 0)
                            {
                                await genericService.UpdateTranslationsAsync(StringResourceEnums.NavBarSubItem, Subtranslations, subItemModel.Id, subItem.LangId);
                            }
                            else
                            {
                                await genericService.AddTranslationsAsync(StringResourceEnums.NavBarSubItem, Subtranslations, subItemModel.Id, subItem.LangId);
                            }
                        }
                    }
                }

                return Success(dtos);
            }
            catch (Exception ex)
            {
                return Error<List<ClientSaysDTO>>(ex);
            }
        }



        public async Task<ResponseResult<ClientSaysDTO>> Delete(int id)
        {
            try
            {
                var clientSay = await _dbContext.ClientSays.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
                if (clientSay == null)
                {
                    return Error<ClientSaysDTO>("ClientSays not found", HttpStatusCode.NotFound);
                }

                _dbContext.ClientSays.Remove(clientSay);
                await _dbContext.SaveChangesAsync();

                return Success<ClientSaysDTO>();
            }
            catch (Exception ex)
            {
                return Error<ClientSaysDTO>(ex);
            }
        }
    }
}
