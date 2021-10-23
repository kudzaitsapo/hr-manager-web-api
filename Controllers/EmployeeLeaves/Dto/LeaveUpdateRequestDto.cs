using System;

namespace HrMan.Controllers.EmployeeLeaves.Dto
{
    public class LeaveUpdateRequestDto
    {
        public int ApprovalStatus { get; set; }

        public Guid? ApprovedBy { get; set; }

    }
}
