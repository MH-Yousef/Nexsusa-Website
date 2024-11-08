using Core.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.HomePage.HomePageItems
{
    public class WorkShowCase : _Base<int>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public WorkShowCaseNavBar WorkShowCaseNavBar { get; set; }
    }
}
