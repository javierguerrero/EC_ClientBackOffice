﻿using Clients.BackOffice.Proxies.Catalog;
using Clients.BackOffice.Proxies.Catalog.Commands;
using Clients.BackOffice.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
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

        public async Task<IActionResult> Details(int id)
        {
            var result = await _catalogProxy.GetCourseAsync(id);

            var vm = new CourseDetailsViewModel
            {
                CourseId = result.CourseId,
                Name = result.Name
            };
            return View("Details", vm);
        }

        public IActionResult Create()
        {
            var vm = new CourseCreateViewModel
            {
                Name = string.Empty,
                Description = string.Empty
            };
            return View("Create", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CourseCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var command = new CourseCreateCommand
                {
                    Name = model.Name,
                    Description = model.Description
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

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _catalogProxy.GetCourseAsync(id);

            var vm = new CourseEditViewModel
            {
                CourseId = result.CourseId,
                Name = result.Name,
                Description = result.Description
            };
            return View("Edit", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CourseEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var command = new CourseUpdateCommand
                {
                    CourseId = model.CourseId,
                    Name = model.Name,
                    Description = model.Description
                };

                try
                {
                    await _catalogProxy.UpdateCourseAsync(command);
                    ViewBag.TheResult = true;
                    return View("Edit", model);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View();
        }

        public async Task<IActionResult> Remove(int id)
        {
            await _catalogProxy.RemoveCourseAsync(id);
            return Ok();
        }
    }
}