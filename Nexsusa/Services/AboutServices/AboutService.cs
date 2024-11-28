using AutoMapper;
using Core.AboutPage;
using Core.Domains.Enums;
using Core.HomePage.HomePageItems;
using Data.Context;
using Data.Dtos.AboutDTOs;
using Data.Dtos.ClientSaysDTOs;
using Data.Dtos.AboutDTOs;
using Microsoft.EntityFrameworkCore;
using Services._Base;
using Services._GenericServices;
using Services.HomePageServices.ClientSaysServices;
using Services.HomePageServices.OurCompanyServices;
using Services.HomePageServices.WhoWeAreServices;

namespace Services.AboutServices
{
    public class AboutService : BaseService, IAboutService
    {
        private readonly GenericService<About> _genericService;
        private readonly IOurCompanyService _ourCompanyService;
        private readonly IClientSaysService _clientSaysService;
        private readonly IWhoWeAreService _whoWeAreService;
        public AboutService(AppDbContext dbContext, IMapper mapper, GenericService<About> genericService, IOurCompanyService ourCompanyService, IClientSaysService clientSaysService, IWhoWeAreService whoWeAreService) : base(dbContext, mapper)
        {
            this._genericService = genericService;
            _ourCompanyService = ourCompanyService;
            _clientSaysService = clientSaysService;
            _whoWeAreService = whoWeAreService;
        }

        public async Task<ResponseResult<AboutDTO>> Get(int languageId)
        {
            try
            {
                var items = await _genericService.GetListAsync(languageId, StringResourceEnums.About);
                var dto = _mapper.Map<List<About>, List<AboutDTO>>(items.Data);

                var OurCompany = (await _ourCompanyService.GetList(languageId)).Data.FirstOrDefault();
                var WhoWeAre = (await _whoWeAreService.GetList(languageId)).Data.FirstOrDefault();
                var ClientSays = (await _clientSaysService.GetList(languageId)).Data.FirstOrDefault();

                if (dto.Count > 0)
                {
                    dto.FirstOrDefault().ClientSays = ClientSays;
                    dto.FirstOrDefault().OurCompany = OurCompany;
                    dto.FirstOrDefault().WhoWeAre = WhoWeAre;
                }
                return Success(dto.FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Error<AboutDTO>(ex);
            }
        }

        public async Task<ResponseResult<AboutDTO>> Manage(AboutDTO dto)
        {
            try
            {
                var defaultLanguage = await _dbContext.Languages.FirstAsync(x => x.IsDefault);
                bool isDefualtExists = await _dbContext.Abouts.AnyAsync();
                bool IsDefultModel = dto.LangId == defaultLanguage.Id;
                bool IsTranslateionExixts = await _dbContext.StringResources.AnyAsync(x => x.ResourceId == dto.Id && x.GroupKey == StringResourceEnums.About && x.LanguageId == dto.LangId);
                var mainItem = _mapper.Map<AboutDTO, About>(dto);

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
                        await _genericService.UpdateTranslationsAsync(StringResourceEnums.About, translations, mainItem.Id, dto.LangId);
                    }
                    else
                    {
                        await _genericService.AddTranslationsAsync(StringResourceEnums.About, translations, mainItem.Id, dto.LangId);
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
                            await _genericService.UpdateTranslationsAsync(StringResourceEnums.About, translations, dto.Id, dto.LangId);
                        }
                        else
                        {
                            await _genericService.AddTranslationsAsync(StringResourceEnums.About, translations, dto.Id, dto.LangId);
                        }


                    }
                    else
                    {
                        return Error<AboutDTO>("Only default language can be managed first");
                    }
                }
                return Success(dto);
            }
            catch (Exception ex)
            {
                return Error<AboutDTO>(ex);
            }
        }

        public async Task<ResponseResult<AboutDTO>> GetFirst()
        {
            try
            {
                var result = await _dbContext.Abouts.FirstOrDefaultAsync();
                var AboutDTO = _mapper.Map<About, AboutDTO>(result);
                return Success(AboutDTO);

            }
            catch (Exception ex)
            {
                return Error<AboutDTO>(ex);
            }
        }
    }
}
