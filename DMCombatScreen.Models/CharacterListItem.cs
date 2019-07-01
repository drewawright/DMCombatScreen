using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMCombatScreen.Models
{
    public class CharacterListItem
    {
        public int CharacterID { get; set; }
        [Display(Name = "Character Name")]
        public string Name { get; set; }
        public bool IsPlayer { get; set; }
    }
}
