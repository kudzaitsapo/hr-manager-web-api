using System;

namespace HrMan.Models.Entities
{
    public abstract class BaseEntity
    {
        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }
    }
}
