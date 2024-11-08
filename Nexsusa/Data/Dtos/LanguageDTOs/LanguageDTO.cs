using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Dtos.BaseDTOs;

namespace Data.Dtos.LanguageDTOs
{
    public class LanguageDTO : BaseDTO<int>
    {
        public string Name { get; set; }
        //====================================================================================================
        public string Shortcut { get; set; }
        //====================================================================================================
        public string Culture { get; set; }
        //====================================================================================================
        public bool IsRtl { get; set; }
        //====================================================================================================
        public bool IsDefault { get; set; }
        //====================================================================================================
        public bool IsActive { get; set; }
        //====================================================================================================
    }
}
