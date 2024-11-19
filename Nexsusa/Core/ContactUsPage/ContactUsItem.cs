using Core.Domains;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ContactUsPage
{
    public class ContactUsItem:_Base<int>
    {
        public string Icon  { get; set; }
        [Translatable]
        public string Title  { get; set; }

        public string ContactDetail  { get; set; }
        [ForeignKey(nameof(ContactUsPageId))]
        public int ContactUsPageId { get; set; }
        public ContactUsPage ContactUsPage { get; set; }
    }
}
