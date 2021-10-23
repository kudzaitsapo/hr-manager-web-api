using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HrMan.Models.Entities
{
    public class Job : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string JobTitle { get; set; }

        public string JobDescription { get; set; }

        public virtual Department Department { get; set; }

        public virtual Salary SalaryRange { get; set; }
    }
}
