using HrMan.Models.Entities;
using System;

namespace HrMan.Controllers.EmployeeLeaves.Dto
{
    public class LeaveRequestDto
    {
        public Guid EmployeeId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public LeaveType LeaveType { get; set; }

        public DateTime DateSubmitted { get; set; }
    }
}
