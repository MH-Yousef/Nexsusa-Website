using Core.Domains;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.HomePage.WorkShowCase
{
    public class WorkShowCaseService:_Base<int>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        [ForeignKey(nameof(WorkShowCaseNavBarItemId))]
        public int WorkShowCaseNavBarItemId { get; set; }
        public WorkShowCaseNavBarItem WorkShowCaseNavBarItem { get; set; }
    }
}
