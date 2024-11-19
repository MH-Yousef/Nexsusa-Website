using Core.ContactUsPage;
using Core.Domains;
using Core.HomePage.HomePageItems;
using Data.Dtos.BaseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dtos.ContactUsDTOs
{
    public class ContactUsPageDTO:BaseDTO<int>  
    {
        [Translatable]
        public string Title { get; set; }
        [Translatable]
        public string SubTitle { get; set; }
        [Translatable]
        public string Description { get; set; }
        
        public List<ContactUsItemDTO> ContactUsItems { get; set; }
    }
}
