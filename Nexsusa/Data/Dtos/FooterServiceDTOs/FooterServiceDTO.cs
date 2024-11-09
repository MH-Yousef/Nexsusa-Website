using Data.Dtos.BaseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dtos.FooterServiceDTOs
{
    public class FooterServiceDTO:BaseDTO<int>
    {
        public string Title { get; set; }
        public string Url { get; set; }
    }
}
