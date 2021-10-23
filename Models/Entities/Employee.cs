using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HrMan.Models.Entities
{
    public class Employee : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Gender { get; set; }

        public DateTime HireDate { get; set; }

        public virtual AppUser User { get; set; }

        public virtual Address HomeAddress { get; set; }

        public virtual Job Job { get; set; }
    }
}
