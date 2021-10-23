using HrMan.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrMan.Controllers.Salaries.Dto
{
    public class SalariesResponseDto
    {
        public Guid Id { get; set; }

        public string PayGrade { get; set; }

        public decimal StartingSalary { get; set; }

        public decimal EndingSalary { get; set; }

    }
}
