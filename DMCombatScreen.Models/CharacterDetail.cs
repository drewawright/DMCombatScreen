using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMCombatScreen.Models
{
    public class CharacterDetail
    {
        public int CharacterID { get; set; }
        [Display(Name = "Character Name")]
        public string Name { get; set; }
        [Display(Name = "Max HP")]
        public int? MaxHP { get; set; }
        [Display(Name = "Initiative Modifier")]
        public int? InitiativeModifier { get; set; }
        [Display(Name = "Initiative Ability Score")]
        public int? InitiativeAbilityScore { get; set; }
        [Display(Name = "Is a Player?")]
        public bool IsPlayer { get; set; }
        [Display(Name = "Character Type")]
        public string CharacterType { get; set; }
    }
}
