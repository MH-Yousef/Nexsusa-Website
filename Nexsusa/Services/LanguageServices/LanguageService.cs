﻿using AutoMapper;
using Core.Domains.Languages;
using Data.Context;
using Data.Dtos.LanguageDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Services._Base;

namespace Services.LanguageServices
{
    public class LanguageService : BaseService, ILanguageService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public LanguageService(AppDbContext dbContext, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(dbContext, mapper)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ResponseResult<List<Language>>> Get()
        {
            try
            {
                var languages = await _dbContext.Languages.AsNoTracking()
                    .Where(x => !x.IsDeleted).ToListAsync();
                if(languages == null || !languages.Any())
                {
                    return Error<List<Language>>("Languages not found");
                }
            return Success(languages);
            }
            catch (Exception ex)
            {
                return Error<List<Language>>(ex);
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

        public async Task<ResponseResult<Language>> Create(Language language)
        {
            try
            {
                bool IsDefultExixts = await _dbContext.Languages.AnyAsync();
                await _dbContext.Languages.AddAsync(language);
                await _dbContext.SaveChangesAsync();
                return Success(language);
            }
            catch (Exception ex)
            {
                return Error<Language>(ex);
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

        public async Task<ResponseResult<LanguageDTO>> Update(LanguageDTO dto)
        {
            try
            {
                var language = await _dbContext.Languages.FirstOrDefaultAsync(x => x.Id == dto.Id && !x.IsDeleted);
                if (language == null)
                {
                    return Error<LanguageDTO>("Language is not found...");
                }
                language = _mapper.Map(dto, language);
                _dbContext.Update(language);

                await _dbContext.SaveChangesAsync();
                return Success<LanguageDTO>();

            }
            catch (Exception ex)
            {

                return Error<LanguageDTO>(ex);

            }
        }


        public async void SetLanguage(string culture)
        {

            var languages = await Get();
            var validCulture = languages.Data.FirstOrDefault(x => x.Culture == culture);

            if (validCulture != null)
            {
                var httpContext = _httpContextAccessor.HttpContext;

                // Cookie'ye dil ayarını kaydet
                httpContext.Response.Cookies.Append(
                    CookieRequestCultureProvider.DefaultCookieName,
                    CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                    new CookieOptions { Expires = DateTimeOffset.UtcNow.AddMonths(1) }
                );
            }
        }
    }
}
