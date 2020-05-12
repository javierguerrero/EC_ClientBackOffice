using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clients.BackOffice.Proxies.Catalog.DTOs
{
    public class CourseDto
    {
        public int CourseId { get; set; }
        public string Name { get; set; }
    }
}
