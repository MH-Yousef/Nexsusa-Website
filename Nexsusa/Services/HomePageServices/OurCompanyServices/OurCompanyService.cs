using AutoMapper;
using Core.Domains.Enums;
using Core.HomePage.HomePageItems;
using Data.Context;
using Data.Dtos.OurCompanyDTOs;
using Data.Dtos.ServiceDTOs;
using Microsoft.EntityFrameworkCore;
using Services._Base;
using Services._GenericServices;
using Services.ImageServices;

namespace Services.HomePageServices.OurCompanyServices
{
    public class OurCompanyService : BaseService, IOurCompanyService
    {
        private readonly GenericService<OurCompany> _genericService;
        private readonly IImageService _imageService;
        public OurCompanyService(AppDbContext dbContext, IMapper mapper, GenericService<OurCompany> genericService, IImageService imageService) : base(dbContext, mapper)
        {
            this._genericService = genericService;
            _imageService = imageService;
        }
        public async Task<ResponseResult<List<OurCompanyDTO>>> GetList(int LanguageId)
        {
            try
            {
                var items = await _genericService.GetListAsync(LanguageId, StringResourceEnums.OurCompany);
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
                var model = await _genericService.GetByIdAsync(id, LanguageId, StringResourceEnums.OurCompany);
                var dto = _mapper.Map<OurCompany, OurCompanyDTO>(model);
                return Success(dto);
            }
            catch (Exception ex)
            {
                return Error<OurCompanyDTO>(ex);
            }
        }

        public async Task<ResponseResult<OurCompanyDTO>> Manage(OurCompanyDTO dto)
        {
            try
            {
                var defaultLanguage = await _dbContext.Languages.FirstAsync(x => x.IsDefault);
                bool isDefualtExists = await _dbContext.OurCompanys.AnyAsync();
                bool IsDefultModel = dto.LangId == defaultLanguage.Id;
                bool IsTranslateionExixts = await _dbContext.StringResources.AnyAsync(x => x.ResourceId == dto.Id && x.GroupKey == StringResourceEnums.OurCompany && x.LanguageId == dto.LangId);
                var mainItem = _mapper.Map<OurCompanyDTO, OurCompany>(dto);
                if (dto.File != null)
                {
                    var imageResult = await _imageService.UploadImage(dto.File);
                    mainItem.ImageUrl = imageResult;
                }
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
                        await _genericService.UpdateTranslationsAsync(StringResourceEnums.OurCompany, translations, mainItem.Id, dto.LangId);
                    }
                    else
                    {
                        await _genericService.AddTranslationsAsync(StringResourceEnums.OurCompany, translations, mainItem.Id, dto.LangId);
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
                            await _genericService.UpdateTranslationsAsync(StringResourceEnums.OurCompany, translations, dto.Id, dto.LangId);
                        }
                        else
                        {
                            await _genericService.AddTranslationsAsync(StringResourceEnums.OurCompany, translations, dto.Id, dto.LangId);
                        }


                    }
                    else
                    {
                        return Error<OurCompanyDTO>("Only default language can be managed first");
                    }
                }


                return Success(dto);
            }
            catch (Exception ex)
            {
                return Error<OurCompanyDTO>(ex);
            }
        }

        public Task<ResponseResult<OurCompanyDTO>> Delete(int id)
        {
            throw new NotImplementedException();
        }


        public async Task<ResponseResult<OurCompanyDTO>> GetFirst()
        {
            try
            {
                var result = await _dbContext.OurCompanys.FirstOrDefaultAsync();
                var sliderDto = _mapper.Map<OurCompany, OurCompanyDTO>(result);
                return Success(sliderDto);

            }
            catch (Exception ex)
            {
                return Error<OurCompanyDTO>(ex);
            }
        }
    }
}
