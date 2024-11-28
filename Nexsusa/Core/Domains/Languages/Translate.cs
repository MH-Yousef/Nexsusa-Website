using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Domains.Languages
{
    public class Translate : _Base<int>
    {
        public string Key { get; set; }
        //====================================================================================================
        public string Value { get; set; }
        //====================================================================================================
        public int LanguageId { get; set; }
        //====================================================================================================
    }
}
