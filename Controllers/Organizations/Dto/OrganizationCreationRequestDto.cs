using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrMan.Controllers.Organizations.Dto
{
    public class OrganizationCreationRequestDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string ContactNumber { get; set; }

    }
}
