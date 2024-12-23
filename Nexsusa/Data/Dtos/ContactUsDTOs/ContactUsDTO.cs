﻿using Core.Domains;
using Data.Dtos.BaseDTOs;

namespace Data.Dtos.ContactUsDTOs
{
    public class ContactUsDTO : BaseDTO<int>
    {
        [Translatable]
        public string Title { get; set; }
        [Translatable]
        public string Description { get; set; }

        
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
