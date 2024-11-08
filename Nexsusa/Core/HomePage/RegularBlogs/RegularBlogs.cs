using Core.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.HomePage.RegularBlogs
{
    public class RegularBlogs:_Base<int>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public List<RegularBlogsItem> RegularBlogsItems { get; set; }
    }
}
