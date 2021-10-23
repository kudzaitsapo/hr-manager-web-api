using HrMan.Controllers.Employees.Dtos;
using HrMan.Models.Entities;
using System;

namespace HrMan.Controllers.EmployeeLeaves.Dto
{
    public class LeaveResponseDto
    {
        public EmployeeResponseDto Employee { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string LeaveType { get; set; }

        public DateTime DateSubmitted { get; set; }

        public string ApprovalStatus { get; set; }

        public EmployeeResponseDto ApprovedBy { get; set; }
    }
}
