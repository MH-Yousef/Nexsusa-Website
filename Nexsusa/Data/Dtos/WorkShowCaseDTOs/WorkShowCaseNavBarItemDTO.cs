using Core.Domains;
using Data.Dtos.BaseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dtos.WorkShowCaseDTOs
{
    public class WorkShowCaseNavBarItemDTO : BaseDTO<int>
    {
        [Translatable]
        public string Name { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
        public bool IsVisible { get; set; }
        public List<WorkShowCaseServiceDTO> WorkShowCaseServices { get; set; }
    }

}
