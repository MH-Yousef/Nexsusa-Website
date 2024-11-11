using AutoMapper;
using Core.Domains;
using Core.Domains.Enums;
using Core.Domains.Languages;
using Data.Context;
using Microsoft.EntityFrameworkCore;
using Services._Base;
using System.Reflection;

namespace Services._GenericServices
{
    public class GenericService<T>  : BaseService, IGenericService where T : class
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


        public async Task<T> GetByIdAsync(int id, int langId, StringResourceEnums groupKey)
        {
            // Retrieve the entity by ID (assumes an entity with 'Id' property exists)
            var entity = await _dbContext.Set<T>().AsNoTracking().FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);

            if (entity != null)
            {
                entity = ApplyTranslations(entity, langId, id, groupKey);
            }

            return entity;
        }
        private T ApplyTranslations<T>(T entity, int langId, int resourceId, StringResourceEnums groupKey)
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
    }
}
