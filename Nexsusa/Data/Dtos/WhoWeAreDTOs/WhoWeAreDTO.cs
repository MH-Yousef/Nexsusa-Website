﻿using Data.Dtos.BaseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dtos.WhoWeAreDTOs
{
    public class WhoWeAreDTO : BaseDTO<int>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public List<WhoWeAreItemDTO> WhoWeAreItem { get; set; }
    }

}