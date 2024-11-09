﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dtos.BaseDTOs
{
    public class BaseDTO<T>
    {
        public T Id { get; set; }
        //====================================================================================================
        public DateTime CreatedDate { get; set; }=DateTime.Now;
        //====================================================================================================
        public DateTime UpdatedDate { get; set; }
        //====================================================================================================
        public bool IsDeleted { get; set; }
        //====================================================================================================
    }
}
