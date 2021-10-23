using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HrMan.Models.Entities
{
    public class Salary : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Grade PayGrade { get; set; }

        public decimal StartingSalary { get; set; }

        public decimal EndingSalary { get; set; }

    }
}
