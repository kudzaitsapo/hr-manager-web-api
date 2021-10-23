using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrMan.Models.Dtos
{
    public class GenericResponseDto<T>
    {
        public int? StatusCode { get; set; }

        public T Result { get; set; }

        public ErrorResponseDto Error { get; set; }
    }
}
