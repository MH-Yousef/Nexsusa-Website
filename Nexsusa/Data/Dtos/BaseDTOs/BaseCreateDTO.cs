﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dtos.BaseDTOs
{
    public class BaseCreateDTO
    {
      
        public DateTime CreatedDate { get; set; }
        //====================================================================================================
        public DateTime UpdatedDate { get; set; }
        //====================================================================================================
        public bool IsDeleted { get; set; }
        //====================================================================================================
    }
}
