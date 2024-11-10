using Services._Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services._GenericServices.Test
{
    public interface ITestGeneric<T>
    {
        Task<ResponseResult<T>> Create(T dto);
    }
}
