using AutoMapper;
using Core.Domains;
using Core.Domains.Enums;
using Core.Domains.Languages;
using Data.Context;
using Microsoft.EntityFrameworkCore;
using Services._Base;
using System.Linq.Expressions;
using System.Reflection;

namespace Services._GenericServices
{
    public class GenericService<C> : BaseService where C : class
    {
        public GenericService(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<ResponseResult<T>> CreateAsync<T>(T dto)
        {
            var entity = _mapper.Map<T>(dto);
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return Success(dto);
        }
        public async Task<ResponseResult<T>> UpdateAsync<T>(T dto)
        {
            var entity = _mapper.Map<T>(dto);
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();
            return Success(dto);
        }
        public async Task AddTranslationsAsync(StringResourceEnums GroupKey, List<(string ColumnName, string ColumnValue)> translations, int resourceId, int langId)
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
                        GroupKey = GroupKey,
                        IsDeleted = false
                    };
                    await _dbContext.StringResources.AddAsync(translation);
                }

            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateTranslationsAsync(StringResourceEnums GroupKey, List<(string ColumnName, string ColumnValue)> translations, int resourceId, int langId)
        {
            foreach (var (columnName, columnValue) in translations)
            {
                // Çeviriyi bul ve güncelle
                var existingTranslation = await _dbContext.StringResources
                    .FirstOrDefaultAsync(x => x.ResourceId == resourceId
                                              && x.LanguageId == langId
                                              && x.GroupKey == GroupKey
                                              && x.Key == columnName);

                if (existingTranslation != null)
                {
                    if (!string.IsNullOrEmpty(columnValue))
                    {
                        existingTranslation.Value = columnValue; // Yeni değeri ayarla
                        existingTranslation.UpdatedDate = DateTime.Now;   // Güncelleme tarihini ayarla
                    }
                    else
                    {
                        // Eğer yeni değer boşsa, çeviri silinebilir
                        _dbContext.StringResources.Remove(existingTranslation);
                    }
                }
                else
                {
                    throw new Exception($"Translation not found for column {columnName} in language {langId}.");
                }
            }

            await _dbContext.SaveChangesAsync(); // Tüm değişiklikleri kaydet
        }


        public async Task<C> GetByIdAsync(int id, int langId, StringResourceEnums groupKey, params Expression<Func<C, object>>[] includes)
        {
            var query = _dbContext.Set<C>().AsNoTracking().AsQueryable();
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            var entity = await query.FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);
            if (entity != null)
            {
                entity = ApplyTranslations(entity, langId, id, groupKey);
            }

            return entity;
        }
        // Get List 
        public async Task<ResponseResult<List<C>>> GetListAsync(int langId, StringResourceEnums groupKey, params Expression<Func<C, object>>[] includes)
        {
            var query = _dbContext.Set<C>().AsNoTracking().AsQueryable();

            // Apply includes if provided
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            var entities = await query.ToListAsync();

            // Filter entities that do not have translations
            var filteredEntities = entities.Where(entity => HasTranslations(entity, langId, (int)entity.GetType().GetProperty("Id").GetValue(entity), groupKey)).ToList();

            return Success(filteredEntities);
        }

        public T ApplyTranslations<T>(T entity, int langId, int resourceId, StringResourceEnums groupKey)
        {
            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                // Check if the property has the Translatable attribute and is a string
                if (property.IsDefined(typeof(TranslatableAttribute)) &&
                    property.PropertyType == typeof(string) &&
                    property.CanRead &&
                    property.CanWrite)
                {
                    var originalValue = (string)property.GetValue(entity);
                    var translatedValue = _dbContext.StringResources
                        .Where(t => t.ResourceId == resourceId && t.Key == property.Name && t.LanguageId == langId && t.GroupKey == groupKey)
                        .FirstOrDefault()?.Value;

                    // Apply the translation if available, otherwise keep the original value
                    property.SetValue(entity, translatedValue ?? originalValue);
                }
            }

            return entity;
        }

        public bool HasTranslations<T>(T entity, int langId, int resourceId, StringResourceEnums groupKey)
        {
            var properties = typeof(T).GetProperties();
            bool hasTranslation = true;

            foreach (var property in properties)
            {
                // Check if the property has the Translatable attribute and is a string
                if (property.IsDefined(typeof(TranslatableAttribute)) &&
                    property.PropertyType == typeof(string) &&
                    property.CanRead &&
                    property.CanWrite)
                {
                    var originalValue = (string)property.GetValue(entity);
                    var translatedValue = _dbContext.StringResources
                        .Where(t => t.ResourceId == resourceId && t.Key == property.Name && t.LanguageId == langId && t.GroupKey == groupKey)
                        .FirstOrDefault()?.Value;

                    // If no translation is found, mark the entity as untranslated
                    if (translatedValue == null)
                    {
                        hasTranslation = false;
                    }
                    //else
                    //{
                    //    property.SetValue(entity, translatedValue);
                    //}
                }
            }

            return hasTranslation;
        }

    }
}
