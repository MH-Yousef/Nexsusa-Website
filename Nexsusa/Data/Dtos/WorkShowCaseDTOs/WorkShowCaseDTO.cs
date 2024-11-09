using Data.Dtos.BaseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dtos.WorkShowCaseDTOs
{
    public class WorkShowCaseDTO : BaseDTO<int>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public WorkShowCaseNavBarDTO WorkShowCaseNavBar { get; set; }
    }

}
