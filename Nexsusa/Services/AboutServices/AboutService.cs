using AutoMapper;
using Core.AboutPage;
using Core.Domains.Enums;
using Data.Context;
using Data.Dtos.AboutDTOs;
using Data.Dtos.ClientSaysDTOs;
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
        private readonly GenericService<About> genericService;
        private readonly IOurCompanyService _ourCompanyService;
        private readonly IClientSaysService _clientSaysService;
        private readonly IWhoWeAreService _whoWeAreService;
        public AboutService(AppDbContext dbContext, IMapper mapper, GenericService<About> genericService, IOurCompanyService ourCompanyService, IClientSaysService clientSaysService, IWhoWeAreService whoWeAreService) : base(dbContext, mapper)
        {
            this.genericService = genericService;
            _ourCompanyService = ourCompanyService;
            _clientSaysService = clientSaysService;
            _whoWeAreService = whoWeAreService;
        }

        public async Task<ResponseResult<AboutDTO>> Get(int languageId)
        {
            try
            {
                var items = await genericService.GetListAsync(languageId, StringResourceEnums.About);
                var dto = _mapper.Map<List<About>, List<AboutDTO>>(items.Data);

                var OurCompany = (await _ourCompanyService.GetList(languageId)).Data.FirstOrDefault();
                var WhoWeAre = (await _whoWeAreService.GetList(languageId)).Data.FirstOrDefault();
                var ClientSays = (await _clientSaysService.GetList(languageId)).Data.FirstOrDefault();

                dto.FirstOrDefault().ClientSays = ClientSays;
                dto.FirstOrDefault().OurCompany = OurCompany;
                dto.FirstOrDefault().WhoWeAre = WhoWeAre;
                return Success(dto.FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Error<AboutDTO>(ex);
            }
        }

        public async Task<ResponseResult<List<AboutDTO>>> Manage(List<AboutDTO> dtos)
        {
            try
            {
                var defultLanguage = await _dbContext.Languages.FirstAsync(x => x.IsDefault);
                var defultModel = dtos.Where(x => x.LangId == defultLanguage.Id).FirstOrDefault();
                var mainItem = _mapper.Map<AboutDTO, About>(defultModel);
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
                                ("SubTitle", item.SubTitle),
                                ("Description", item.Description),
                            };
                    if (item.Id > 0)
                    {
                        await genericService.UpdateTranslationsAsync(StringResourceEnums.About, translations, mainItem.Id, item.LangId);
                    }
                    else
                    {
                        await genericService.AddTranslationsAsync(StringResourceEnums.About, translations, mainItem.Id, item.LangId);
                    }
                }


                return Success(dtos);
            }
            catch (Exception ex)
            {
                return Error<List<AboutDTO>>(ex);
            }
        }
    }
}
