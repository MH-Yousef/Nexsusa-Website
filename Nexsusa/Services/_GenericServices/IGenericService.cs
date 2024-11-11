using Core.Domains.Enums;
using Services._Base;

namespace Services._GenericServices
{
    public interface IGenericService
    {
        Task<ResponseResult<T>> CreateAync<T>(T dto);
        Task AddTranslationsAsync(StringResourceEnums GroupKey, List<(string ColumnName, string ColumnValue)> translations, int resourceId, int langId);
        Task UpdateTranslationsAsync(StringResourceEnums GroupKey, List<(string ColumnName, string ColumnValue)> translations, int resourceId, int langId);
    }
}
