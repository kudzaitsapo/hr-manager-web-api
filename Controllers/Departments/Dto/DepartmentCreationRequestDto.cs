using System;

namespace HrMan.Controllers.Departments.Dto
{
    public class DepartmentCreationRequestDto
    {
        public string DepartmentName { get; set; }

        public Guid OrganizationId { get; set; }
    }
}
