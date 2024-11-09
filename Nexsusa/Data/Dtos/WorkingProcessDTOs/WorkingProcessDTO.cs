using Data.Dtos.BaseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dtos.WorkingProcessDTOs
{
    public class WorkingProcessDTO : BaseDTO<int>
    {
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public List<WorkingProcessItemDTO> WorkingProcessItems { get; set; }
    }
}
