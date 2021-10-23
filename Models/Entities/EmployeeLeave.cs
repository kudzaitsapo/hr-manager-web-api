using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HrMan.Models.Entities
{
    public class EmployeeLeave : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public virtual Employee Employee { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public LeaveType LeaveType { get; set; }

        public DateTime DateSubmitted { get; set; }

        public ApprovalStatus ApprovalStatus { get; set; }

        public virtual Employee ApprovedBy { get; set; }

    }
}
