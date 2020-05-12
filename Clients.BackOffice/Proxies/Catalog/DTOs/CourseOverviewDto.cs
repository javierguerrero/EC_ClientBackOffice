using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clients.BackOffice.Proxies.Catalog.DTOs
{
    public class CourseOverviewDto
    {
        public int CourseId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
