using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrMan.Models.Dtos
{
    public class ErrorResponseDto
    {
        public int ErrorCode { get; set; }

        public string Message { get; set; }

    }
}
