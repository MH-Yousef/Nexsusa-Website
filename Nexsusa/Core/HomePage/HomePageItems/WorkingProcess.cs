using Core.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.HomePage.HomePageItems
{
    public class WorkingProcess : _Base<int>
    {
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public List<WorkingProcessItem> WorkingProcessItems { get; set; }
    }
}
