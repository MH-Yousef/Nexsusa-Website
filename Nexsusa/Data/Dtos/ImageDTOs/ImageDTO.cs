﻿using Data.Dtos.BaseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dtos.ImageDTOs
{
    public class ImageDTO : BaseDTO<int>
    {
        public string ImageUrl { get; set; }
        public string Url { get; set; }
    }
}
