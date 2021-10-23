using HrMan.Controllers.Organizations.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrMan.Controllers.Departments.Dto
{
    public class DepartmentResponseDto
    {
        public Guid Id { get; set; }

        public string DepartmentName { get; set; }

        public OrganizationResponseDto Organization { get; set; }
    }
}
