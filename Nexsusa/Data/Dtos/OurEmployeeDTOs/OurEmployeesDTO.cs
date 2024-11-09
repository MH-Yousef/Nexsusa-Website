using Data.Dtos.BaseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dtos.OurEmployeeDTOs
{
    public class OurEmployeesDTO : BaseDTO<int>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public List<OurEmployeesItemDTO> OurEmployeesItems { get; set; }
    }
}
