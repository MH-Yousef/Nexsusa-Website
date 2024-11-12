using Core.Domains;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.HomePage.HomePageItems
{
    public class WorkingProcessItem : _Base<int>
    {
        public string Icon { get; set; }
        [Translatable]
        public string Title { get; set; }
        [Translatable]
        public string Description { get; set; }

        [ForeignKey(nameof(WorkingProcessId))]
        public int WorkingProcessId { get; set; }
    }
}
