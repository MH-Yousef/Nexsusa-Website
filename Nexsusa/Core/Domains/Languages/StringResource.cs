using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domains.Languages
{
    public class StringResource :_Base<int>
    {
        public string Key { get; set; }
        //====================================================================================================
        public string Value { get; set; }
        //====================================================================================================
        public int LangId { get; set; }
        //====================================================================================================
    }
}
