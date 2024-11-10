using AutoMapper;
using Core.Domains.Languages;
using Data.Context;
using Microsoft.EntityFrameworkCore;
using Services._Base;

namespace Services._GenericServices
{
    public class GenericService : BaseService,IGenericService
    {
        public GenericService(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<ResponseResult<T>> CreateAync<T>(T dto)
        {
            var entity = _mapper.Map<T>(dto);
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return Success(dto);
        }
        public async Task<ResponseResult<List<T>>> CreateWithStringResources<T, U>(List<T> dtos)
             where T : class
             where U : class
        {
            try
            {
                // DTO'ları entity'lere map etme
                var entities = _mapper.Map<List<U>>(dtos);

                // Veritabanına ekleme
                await _dbContext.AddRangeAsync(entities);
                await _dbContext.SaveChangesAsync();

                // Başarılı yanıt döndürme
                return new ResponseResult<List<T>> { Data = dtos, IsSuccess = true };
            }
            catch (Exception ex)
            {
                // Hata yanıtı döndürme
                return new ResponseResult<List<T>> { IsSuccess = false, Errors = new List<string> { ex.Message } };
            }
        }
        public async Task<ResponseResult<List<T>>> GetListWithStringResources<T,U>() where T : class where U : class
        {
            try
            {

                var entities = await _dbContext.Set<U>().ToListAsync();
                var dtos = _mapper.Map<List<T>>(entities);
                return new ResponseResult<List<T>> { Data = dtos, IsSuccess = true };
            }
            catch (Exception ex)
            {
                return new ResponseResult<List<T>> { IsSuccess = false, Errors = new List<string> { ex.Message } };
            }
        }
        // Get List by Language Id
        public async Task<ResponseResult<List<StringResource>>> GetListByLangId(int langId, int resourceId)
        {
            try
            {
                var entities = await _dbContext.StringResources.Where(x => x.LanguageId == langId && x.ResourceId == resourceId).ToListAsync();
                if (entities == null || !entities.Any())
                {
                    return new ResponseResult<List<StringResource>> { IsSuccess = false, Errors = new List<string> { "String Resources not found" } };
                }
                return Success(entities);
            }
            catch (Exception ex)
            {
                return Error<List<StringResource>>(ex);
            }
        }
        public async Task AddTranslationsAsync(List<(string ColumnName, string ColumnValue)> translations, int resourceId, int langId)
        {


            foreach (var (columnName, columnValue) in translations)
            {
                if (!string.IsNullOrEmpty(columnValue))
                {
                    var translation = new StringResource
                    {
                        LanguageId = langId,
                        ResourceId = resourceId,
                        Key = columnName,
                        Value = columnValue,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                        IsDeleted = false
                    };
                    await _dbContext.StringResources.AddAsync(translation);
                }

            }

            await _dbContext.SaveChangesAsync();
        }
    }
}
