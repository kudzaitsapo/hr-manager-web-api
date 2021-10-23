using HrMan.Controllers.Departments.Dto;
using HrMan.Controllers.Salaries.Dto;
using System;

namespace HrMan.Controllers.Jobs.Dto
{
    public class JobResponseDto
    {
        public Guid Id { get; set; }

        public string JobTitle { get; set; }

        public string JobDescription { get; set; }

        public DepartmentResponseDto Department { get; set; }

        public SalariesResponseDto SalaryRange { get; set; }
    }
}
