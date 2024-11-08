using AutoMapper;
using Data.Context;
using Data.Dtos.LanguageDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Services._Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Services.LanguageServices
{
    public class LanguageService : BaseService<LanguageService>, ILanguageService
    {
        
        public LanguageService(AppDbContext dbContext,IMapper mapper) : base(dbContext,mapper)
        {
        }

        public async Task<ResponseResult<List<LanguageDTO>>> Get()
        {
            try
            {
                var languages = await _dbContext.Languages.AsNoTracking()
                    .Where(x => !x.IsDeleted)
                    .Select(x => new LanguageDTO
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Culture = x.Culture,
                        CreatedDate = x.CreatedDate,
                        UpdatedDate = x.UpdatedDate,
                        IsActive = x.IsActive,
                        IsDefault = x.IsDefault,
                        IsRtl = x.IsRtl,
                        Shortcut = x.Shortcut,
                    }).ToListAsync();
                if (languages == null)
                {
                    return Error<List<LanguageDTO>>("Languages not found", System.Net.HttpStatusCode.NotFound);
                }
                return Success(languages);
            }
            catch (Exception ex)
            {
                return Error<List<LanguageDTO>>(ex);
            }
        }

        public async Task<ResponseResult<LanguageDTO>> GetById(int id)
        {
            try
            {
                var language = await _dbContext.Languages.AsNoTracking()
                    .Select(x => new LanguageDTO
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Culture = x.Culture,
                        CreatedDate = x.CreatedDate,
                        UpdatedDate = x.UpdatedDate,
                        IsActive = x.IsActive,
                        IsDefault = x.IsDefault,
                        IsRtl = x.IsRtl,
                        Shortcut = x.Shortcut,
                    })
                    .FirstOrDefaultAsync(x => x.Id == id);
                if (language == null)
                {
                    return Error<LanguageDTO>("Language not found", System.Net.HttpStatusCode.NotFound);
                }
                return Success(language);
            }
            catch (Exception ex)
            {
                return Error<LanguageDTO>(ex);
            }
        }

        public async Task<ResponseResult<CreateLanguageDTO>> Create(CreateLanguageDTO dto)
        {
            try
            {
                var language = new Core.Domains.Languages.Language
                {
                    Name = dto.Name,
                    Culture = dto.Culture,
                    IsActive = dto.IsActive,
                    IsDefault = dto.IsDefault,
                    IsRtl = dto.IsRtl,
                    Shortcut = dto.Shortcut,
                };
                await _dbContext.Languages.AddAsync(language);
                await _dbContext.SaveChangesAsync();
                return Success(dto);
            }
            catch (Exception ex)
            {
                return Error<CreateLanguageDTO>(ex);
            }
        }

        public async Task<ResponseResult<LanguageDTO>> Delete(int id)
        {
            try
            {
                var language = await _dbContext.Languages.FirstOrDefaultAsync(x => x.Id == id);
                 _dbContext.Remove(language);
                await _dbContext.SaveChangesAsync();
                return Success<LanguageDTO>();

            }
            catch (Exception ex)
            {

                return Error<LanguageDTO>(ex);

            }

        }

        public async Task<ResponseResult<UpdateLanguageDTO>> Update(UpdateLanguageDTO dto)
        {
            try
            {
                var language = await _dbContext.Languages.FirstOrDefaultAsync(x => x.Id == dto.Id && !x.IsDeleted);
                if (language == null)
                {
                    return Error<UpdateLanguageDTO>("Language is not found...");
                }
                language=_mapper.Map(dto, language);
                 _dbContext.Update(language); 

                await _dbContext.SaveChangesAsync();
                return Success<UpdateLanguageDTO>();

            }
            catch (Exception ex)
            {

                return Error<UpdateLanguageDTO>(ex);

            }
        }
    }
}
