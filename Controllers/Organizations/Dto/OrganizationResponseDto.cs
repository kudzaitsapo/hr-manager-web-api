using System;

namespace HrMan.Controllers.Organizations.Dto
{
    public class OrganizationResponseDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ContactNumber { get; set; }
    }
}
