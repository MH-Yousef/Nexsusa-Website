using AutoMapper;
using Core.Domains.Languages;
using Data.Context;
using Data.Dtos.LanguageDTOs;
using Microsoft.EntityFrameworkCore;
using Services._Base;

namespace Services.LanguageServices
{
    public class LanguageService : BaseService, ILanguageService
    {

        public LanguageService(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<IEnumerable<Language>> Get()
        {
            try
            {
                return await _dbContext.Languages.AsNoTracking()
                    .Where(x => !x.IsDeleted).ToListAsync();
            }
            catch
            {
                return null;
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
    }
}
