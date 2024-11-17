﻿using Core.Domains;
using Data.Dtos.BaseDTOs;
using Data.Dtos.ImageDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dtos.ServiceDTOs
{
    public class ServiceItemDTO : BaseDTO<int>
    {
        [Translatable]
        public string Title { get; set; }
        [Translatable]
        public string Description { get; set; }
        public string IconUrl { get; set; }
        public int ServiceId { get; set; }
        public string Images1 { get; set; }
        public string Images2 { get; set; }
        public string Images3 { get; set; }
    }

}
