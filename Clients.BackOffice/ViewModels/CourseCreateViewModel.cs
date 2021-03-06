﻿using Clients.BackOffice.Proxies.Catalog.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Clients.BackOffice.ViewModels
{
    public class CourseCreateViewModel
    {
        public int CourseId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public IEnumerable<LevelDto> Levels { get; set; }
    }
}
