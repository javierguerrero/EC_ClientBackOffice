using Clients.BackOffice.Proxies.Catalog;
using Clients.BackOffice.Proxies.Catalog.Commands;
using Clients.BackOffice.Proxies.Catalog.DTOs;
using Clients.BackOffice.Proxies.Common;
using Clients.BackOffice.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clients.BackOffice.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class CoursesController : Controller
    {
        private readonly ICatalogProxy _catalogProxy;

        public CoursesController(ICatalogProxy catalogProxy)
        {
            _catalogProxy = catalogProxy;
        }

        public async Task<IActionResult> Index(int page = 1, int take = 10)
        {
            var result = await _catalogProxy.GetCourseOverviewsAsync(page, take);
            return View(result);
        }

        public IActionResult Create(string returnUrl)
        {
            var vm = new CourseCreateViewModel
            {
                Name = string.Empty,
                Description = string.Empty
            };
            return View("Create", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CourseCreateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var command = new CourseCreateCommand
                {
                    Name = vm.Name,
                    Description = vm.Description
                };

                try
                {
                    await _catalogProxy.CreateCourseAsync(command);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View();
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