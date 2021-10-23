using HrMan.Controllers.Jobs.Dto;
using System;

namespace HrMan.Controllers.Employees.Dtos
{
    public class EmployeeResponseDto
    {
        public Guid Id { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Middlename { get; set; }

        public string Email { get; set; }

        public string Gender { get; set; }

        public string MobileNumber { get; set; }

        public AddressResponseDto HomeAddress { get; set; }

        public DateTime HireDate { get; set; }

        public JobResponseDto Job { get; set; }

    }
}
