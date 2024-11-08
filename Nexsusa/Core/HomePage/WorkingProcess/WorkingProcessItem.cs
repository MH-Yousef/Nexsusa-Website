using Core.Domains;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.HomePage.WorkingProcess
{
    public class WorkingProcessItem : _Base<int>
    {
        public string Icon { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        [ForeignKey(nameof(WorkingProcessId))]
        public int WorkingProcessId { get; set; }
    }
}
