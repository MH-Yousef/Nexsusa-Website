using Core.Domains;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.HomePage.HomePageItems
{
    public class WorkShowCaseService : _Base<int>
    {
        [Translatable]
        public string Title { get; set; }
        [Translatable]
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        [ForeignKey(nameof(WorkShowCaseNavBarItemId))]
        public int WorkShowCaseNavBarItemId { get; set; }
        public WorkShowCaseNavBarItem WorkShowCaseNavBarItem { get; set; }
    }
}
