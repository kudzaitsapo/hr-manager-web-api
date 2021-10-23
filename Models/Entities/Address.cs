using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HrMan.Models.Entities
{
    public class Address : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string HouseNumber { get; set; }

        public string BuildingComplexName { get; set; }

        public string StreetName { get; set; }

        public string SuburbTownship { get; set; }

        public string City { get; set; }

        public AddressType TypeOfAddress { get; set; }

        public string Country { get; set; }

        public string Province { get; set; }

    }
}
