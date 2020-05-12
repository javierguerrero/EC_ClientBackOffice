using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clients.BackOffice.Proxies.Catalog.DTOs
{
    public class LessonDto
    {
        public int LessonId { get; set; }
        public string Name { get; set; }
        public string VideoUrl { get; set; }
    }
}
