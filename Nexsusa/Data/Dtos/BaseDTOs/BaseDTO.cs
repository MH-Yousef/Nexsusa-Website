namespace Data.Dtos.BaseDTOs
{
    public class BaseDTO<T>
    {
        public T Id { get; set; }
        //====================================================================================================
        public int LangId { get; set; }
        //====================================================================================================
        public DateTime CreatedDate { get; set; }=DateTime.Now;
        //====================================================================================================
        public DateTime UpdatedDate { get; set; }
        //====================================================================================================
        public bool IsDeleted { get; set; }
        //====================================================================================================
        //public List<KeyLisTModel> Keys { get; set; }
    }
    public class KeyLisTModel
    {
        public int DtoId { get; set; }
        public int LangId { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
