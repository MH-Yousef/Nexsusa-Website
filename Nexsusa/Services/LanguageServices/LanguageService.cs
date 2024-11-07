using Data.Context;
using Data.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Services._Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.LanguageServices
{
    public class LanguageService : BaseService<LanguageService>, ILanguageService
    {
        public LanguageService(AppDbContext dbContext, IHttpContextAccessor httpContextAccessor) : base(dbContext, httpContextAccessor)
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

        public Task<ResponseResult<LanguageDTO>> Create(LanguageDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseResult<LanguageDTO>> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseResult<LanguageDTO>> Update(LanguageDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
