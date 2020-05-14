using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clients.BackOffice.Proxies.Catalog.Commands
{
    public class CourseUpdateCommand
    {
        public int CourseId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
