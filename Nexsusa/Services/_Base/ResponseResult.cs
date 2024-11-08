﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Services._Base
{
    public class ResponseResult<T>
    {
        public HttpStatusCode StatusCode { get; set; }
        //====================================================================================================
        public bool IsSuccess { get; set; }
        //====================================================================================================
        public List<string> Errors { get; set; }
        //====================================================================================================
        public T Data { get; set; }
        //====================================================================================================
       
    }

}
