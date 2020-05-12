using Clients.BackOffice.Proxies.Catalog.DTOs;
using Clients.BackOffice.Proxies.Common;
using Clients.BackOffice.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clients.BackOffice.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class CourseController : Controller
    {
        public async Task<IActionResult> Index(int page = 1, int take = 10)
        {
            var courses = new List<CourseOverviewDto>();
            courses.Add(new CourseOverviewDto { CourseId = 1, Name = "Animals", Description = "Something interesting about the animals" });
            courses.Add(new CourseOverviewDto { CourseId = 2, Name = "Animals", Description = "Something interesting about the animals" });
            courses.Add(new CourseOverviewDto { CourseId = 3, Name = "Animals", Description = "Something interesting about the animals" });
            courses.Add(new CourseOverviewDto { CourseId = 4, Name = "Animals", Description = "Something interesting about the animals" });
            courses.Add(new CourseOverviewDto { CourseId = 5, Name = "Animals", Description = "Something interesting about the animals" });

            var result = new DataCollection<CourseOverviewDto>
            {
                Items = courses
            };

            return View(result);
        }

        public async Task<IActionResult> Create(string returnUrl)
        {
            var vm = new CourseCreateViewModel
            {
                ReturnUrl = returnUrl,
                Name = string.Empty,
                Description = string.Empty
            };
            return View("Create", vm);
        }

        public async Task<IActionResult> Details(int id)
        {
            var vm = new CourseDetailsViewModel
            {
                CourseId = id
            };
            return View("Details", vm);
        }

        //personalizar url con "settings"
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var vm = new CourseEditViewModel
            {
                CourseId = id,
                Name = string.Empty,
                Description = string.Empty
            };
            return View("Edit", vm);
        }

        [HttpPost]
        public IActionResult Edit(CourseEditViewModel model)
        {
            return View("Edit", model);
        }

        public async Task<IActionResult> Remove(int id)
        {
            return Ok();
        }
    }
}