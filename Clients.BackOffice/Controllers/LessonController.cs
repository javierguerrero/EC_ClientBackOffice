using Clients.BackOffice.Proxies.Catalog;
using Clients.BackOffice.Proxies.Catalog.Commands;
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
    public class LessonController : Controller
    {
        private readonly ICatalogProxy _catalogProxy;

        public LessonController(ICatalogProxy catalogProxy)
        {
            _catalogProxy = catalogProxy;
        }

        public async Task<IActionResult> Index(int page = 1, int take = 10)
        {
            var result = await _catalogProxy.GetLessonOverviewsAsync(page, take);
            return View(result);
        }

        public async Task<IActionResult> Create(string returnUrl)
        {
            var vm = new LessonViewModel
            {
                ReturnUrl = returnUrl,
                Name = string.Empty,
                Description = string.Empty,
                Characters = await GetCharacters()
            };
            return View("Create", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(LessonViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var command = new LessonCreateCommand
                {
                    Name = vm.Name,
                    Description = vm.Description,
                    VideoUrl = vm.VideoUrl,
                    Dialogues = GetDialogues(vm.Dialogues)
                };
                try
                {
                    await _catalogProxy.CreateLessonAsync(command);
                    return LocalRedirect(vm.ReturnUrl);
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
            await _catalogProxy.RemoveLessonAsync(id);
            return Ok();
        }

        private async Task<IEnumerable<CharacterViewModel>> GetCharacters()
        {
            var result = new List<CharacterViewModel>();
            var characters = await _catalogProxy.GetCharactersAsync();
            foreach (var character in characters)
            {
                result.Add(new CharacterViewModel
                {
                    CharacterId = character.CharacterId,
                    CharacterName = character.Name
                });
            }
            return result;
        }

        private IEnumerable<DialogueCreate> GetDialogues(IEnumerable<DialogueViewModel> dialogues)
        {
            var result = new List<DialogueCreate>();
            foreach (var dialogue in dialogues)
            {
                result.Add(new DialogueCreate
                {
                    CharacterId = dialogue.CharacterId,
                    Text = dialogue.Text
                });
            }

            return result;
        }
    }
}