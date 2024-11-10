using Services._Base;

namespace Services._GenericServices
{
    public interface IGenericService
    {
        Task<ResponseResult<T>> CreateAync<T>(T dto);
        Task AddTranslationsAsync(List<(string ColumnName, string ColumnValue)> translations, int resourceId, int langId);
    }
}
