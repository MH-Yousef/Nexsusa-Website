using Core.Domains;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.HomePage.HomePageItems
{
    public class WorkShowCaseNavBar : _Base<int>
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
        public bool HasSubItem { get; set; }
        public bool IsVisible { get; set; }
        public List<WorkShowCaseNavBarItem> WorkShowCaseNavBarItems { get; set; }
        [ForeignKey(nameof(WorkShowCaseId))]
        public int WorkShowCaseId { get; set; }
        public WorkShowCase WorkShowCase { get; set; }

    }
}
