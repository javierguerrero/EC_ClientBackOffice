using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Clients.BackOffice.ViewModels
{
    public class LessonViewModel
    {
        public string ReturnUrl { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string VideoUrl { get; set; }

        public IEnumerable<CharacterViewModel> Characters { get; set; } = new List<CharacterViewModel>();

        public IEnumerable<DialogueViewModel> Dialogues { get; set; } = new List<DialogueViewModel>();

    }

    public class CharacterViewModel
    {
        public int CharacterId { get; set; }
        public string CharacterName { get; set; }
    }

    public class DialogueViewModel
    {
        public int CharacterId { get; set; }
        public string Text { get; set; }
        public int Order { get; set; }
    }
}
