using System;

namespace HrMan.Controllers.Employees.Dtos
{
    public class AddressResponseDto
    {
        public Guid Id { get; set; }

        public string HouseNumber { get; set; }

        public string BuildingComplexName { get; set; }

        public string StreetName { get; set; }

        public string SuburbTownship { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string Province { get; set; }
    }
}
