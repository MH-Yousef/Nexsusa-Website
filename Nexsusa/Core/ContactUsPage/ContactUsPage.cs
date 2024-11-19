using Core.Domains;
using Core.HomePage.HomePageItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ContactUsPage
{
    public class ContactUsPage:_Base<int>
    {
        [Translatable]
        public string Title { get; set; }
        [Translatable]
        public string SubTitle { get; set; }
        [Translatable]
        public string Description { get; set; }
        
        public List<ContactUsItem> ContactUsItems { get; set; }
    }
}
