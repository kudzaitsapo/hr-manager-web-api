using System;

namespace HrMan.Controllers.Jobs.Dto
{
    public class JobCreationRequestDto
    {
        public string JobTitle { get; set; }

        public string JobDescription { get; set; }

        public Guid DepartmentId { get; set; }

        public Guid SalaryRangeId { get; set; }
    }
}
