using Data.Dtos.BaseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dtos.WhoWeAreDTOs
{
    public class WhoWeAreItemDTO : BaseDTO<int>
    {
        public string Description { get; set; }
        public int WhoWeAreId { get; set; }
    }

}
