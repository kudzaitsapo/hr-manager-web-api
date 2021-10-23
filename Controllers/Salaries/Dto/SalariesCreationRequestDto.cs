using HrMan.Models.Entities;

namespace HrMan.Controllers.Salaries.Dto
{
    public class SalariesCreationRequestDto
    {
        public Grade PayGrade { get; set; }

        public decimal StartingSalary { get; set; }

        public decimal EndingSalary { get; set; }
    }
}
