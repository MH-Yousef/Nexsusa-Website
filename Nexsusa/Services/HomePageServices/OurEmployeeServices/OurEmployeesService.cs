using AutoMapper;
using Core.Domains.Enums;
using Core.HomePage.HomePageItems;
using Data.Context;
using Data.Dtos.OurEmployeeDTOs;
using Microsoft.EntityFrameworkCore;
using Services._Base;
using Services._GenericServices;
using Services.ImageServices;

namespace Services.HomePageServices.OurEmployeeServices
{
    public class OurEmployeesService : BaseService, IOurEmployeesService
    {
        private readonly GenericService<OurEmployees> _genericService;
        private readonly IImageService _imageService;
        public OurEmployeesService(AppDbContext dbContext, IMapper mapper, GenericService<OurEmployees> genericService, IImageService imageService) : base(dbContext, mapper)
        {
            this._genericService = genericService;
            _imageService = imageService;
        }

        public async Task<ResponseResult<List<OurEmployeesDTO>>> GetList(int LanguageId)
        {
            try
            {
                var items = await _genericService.GetListAsync(LanguageId, StringResourceEnums.OurEmployee, x => x.OurEmployeesItems);
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
                var model = await _genericService.GetByIdAsync(id, LanguageId, StringResourceEnums.OurEmployee, x => x.OurEmployeesItems);
                var dto = _mapper.Map<OurEmployees, OurEmployeesDTO>(model);
                return Success(dto);
            }
            catch (Exception ex)
            {
                return Error<OurEmployeesDTO>(ex);
            }
        }

        public async Task<ResponseResult<OurEmployeesDTO>> Manage(OurEmployeesDTO dto)
        {
            try
            {
                var defaultLanguage = await _dbContext.Languages.FirstAsync(x => x.IsDefault);
                bool isDefualtExists = await _dbContext.OurEmployees.AnyAsync();
                bool IsDefultModel = dto.LangId == defaultLanguage.Id;
                bool IsTranslateionExixts = await _dbContext.StringResources.AnyAsync(x => x.ResourceId == dto.Id && x.GroupKey == StringResourceEnums.OurEmployee && x.LanguageId == dto.LangId);
                var service = _mapper.Map<OurEmployeesDTO, OurEmployees>(dto);
                if (IsDefultModel)
                {
                    if (dto.Id > 0)
                    {
                        await _genericService.UpdateAsync(service);
                    }
                    else
                    {
                        await _genericService.CreateAsync(service);
                    }
                    var translations = new List<(string ColumnName, string ColumnValue)>
                            {
                                ("Title", dto.Title),
                                ("Description", dto.Description)
                            };

                    if (dto.Id > 0)
                    {
                        await _genericService.UpdateTranslationsAsync(StringResourceEnums.OurEmployee, translations, service.Id, dto.LangId);
                    }
                    else
                    {
                        await _genericService.AddTranslationsAsync(StringResourceEnums.OurEmployee, translations, service.Id, dto.LangId);
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
                            await _genericService.UpdateTranslationsAsync(StringResourceEnums.OurEmployee, translations, dto.Id, dto.LangId);
                        }
                        else
                        {
                            await _genericService.AddTranslationsAsync(StringResourceEnums.OurEmployee, translations, dto.Id, dto.LangId);
                        }


                    }
                    else
                    {
                        return Error<OurEmployeesDTO>("Only default language can be managed first");
                    }
                }

                return Success(dto);
            }
            catch (Exception ex)
            {
                return Error<OurEmployeesDTO>(ex);
            }
        }
        // Mabnage SubItem
        public async Task<ResponseResult<OurEmployeesItemDTO>> ManageSubItem(OurEmployeesItemDTO subDto)
        {
            try
            {
                var defaultLanguage = await _dbContext.Languages.FirstAsync(x => x.IsDefault);
                bool IsTranslateionExixts = await _dbContext.StringResources.AnyAsync(x => x.ResourceId == subDto.Id && x.GroupKey == StringResourceEnums.OurEmployeesItem && x.LanguageId == subDto.LangId);
                var subItem = _mapper.Map<OurEmployeesItemDTO, OurEmployeesItem>(subDto);
                if (subDto.File != null)
                {
                    var imageResult = await _imageService.UploadImage(subDto.File);
                    subItem.ImageUrl = imageResult;
                }
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
                return Success(subDto);
            }
            catch (Exception ex)
            {
                return Error<OurEmployeesItemDTO>(ex);
            }
        }
        public async Task<ResponseResult<OurEmployeesDTO>> GetFirst()
        {
            try
            {
                var result = await _dbContext.OurEmployees.Include(x => x.OurEmployeesItems).FirstOrDefaultAsync();
                var serviceDto = _mapper.Map<OurEmployeesDTO>(result);
                return Success(serviceDto);
            }
            catch (Exception ex)
            {
                return Error<OurEmployeesDTO>(ex);
            }
        }
    }

}
