using Core.ContactUsPage;
using Core.Domains;
using Data.Dtos.BaseDTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dtos.ContactUsDTOs
{
    public class ContactUsItemDTO:BaseDTO<int>
    {
        public string Icon { get; set; }
        [Translatable]
        public string Title { get; set; }

        public string ContactDetail { get; set; }
        [ForeignKey(nameof(ContactUsPageId))]
        public int ContactUsPageId { get; set; }
        
    }
}
