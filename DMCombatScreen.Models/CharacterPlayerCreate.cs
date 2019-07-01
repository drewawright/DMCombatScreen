using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMCombatScreen.Models
{
    public class CharacterPlayerCreate
    {
        [Required]
        [Display(Name = "Character Name")]
        public string Name { get; set; }

    }
}
