using Core.Domains;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.HomePage.HomePageItems
{
    public class WorkShowCaseNavBarItem : _Base<int>
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
        public bool IsVisible { get; set; }
        [ForeignKey(nameof(WorkShowCaseNavBarId))]
        public int WorkShowCaseNavBarId { get; set; }
        public WorkShowCaseNavBar WorkShowCaseNavBar { get; set; }
        public List<WorkShowCaseService> WorkShowCaseServices { get; set; }
    }
}
