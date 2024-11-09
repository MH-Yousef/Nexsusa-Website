using Data.Dtos.BaseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dtos.OurEmployeeDTOs
{
    public class OurEmployeesItemDTO : BaseDTO<int>
    {
        public string FullName { get; set; }
        public string Branch { get; set; }
        public string ImageUrl { get; set; }
        public int OurEmployeesId { get; set; }
    }

}
