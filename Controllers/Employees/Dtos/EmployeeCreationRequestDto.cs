using System;

namespace HrMan.Controllers.Employees.Dtos
{
    public class EmployeeCreationRequestDto
    {
        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Middlename { get; set; }

        public string Email { get; set; }

        public string Gender { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string MobileNumber { get; set; }

        public Guid JobId { get; set; }

        public AddressRequestDto HomeAddress { get; set; }

        public DateTime HireDate { get; set; }

    }
}
