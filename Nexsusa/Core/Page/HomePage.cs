using Core.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Page
{
    public class HomePage : _Base<int>
    {
        public string Title { get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaAuthor { get; set; }
        public string MetaPublisher { get; set; }
        public List<NavBarItem> NavBarItems { get; set; }
    }
}
