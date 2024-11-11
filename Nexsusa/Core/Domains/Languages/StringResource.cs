using Core.Domains.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domains.Languages
{
    public class StringResource : _Base<int>
    {
        private StringResourceEnums _groupKey;
        public StringResourceEnums GroupKey
        {
            get => _groupKey;
            set
            {
                _groupKey = value;
                GroupKeyString = _groupKey.ToString();
            }
        }
        public string GroupKeyString { get; set; }
        //====================================================================================================
        public string Key { get; set; }
        //====================================================================================================
        public int ResourceId { get; set; }
        //====================================================================================================
        public string Value { get; set; }
        //====================================================================================================
        [ForeignKey(nameof(LanguageId))]
        public int LanguageId { get; set; }
        //====================================================================================================
        public Language Language { get; set; }
        //====================================================================================================
    }
}
