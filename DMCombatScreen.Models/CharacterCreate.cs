using DMCombatScreen.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMCombatScreen.Models
{
    public class CharacterCreate
    {
        [Required]
        [Display(Name = "Character Name")]
        [MaxLength(100, ErrorMessage = "Character Name cannot be more than 100 characters")]
        public string Name { get; set; }
        [Display(Name = "Max HP")]
        public int? MaxHP { get; set; }
        [Display(Name = "Initiative Modifier")]
        public int? InitiativeModifier { get; set; }
        [Display(Name = "Initiative Ability Score")]
        public int? InitiativeAbilityScore { get; set; }
        [Display(Name = "Check if Player Character")]
        public bool IsPlayer { get; set; }
        [Required]
        [Display(Name = "Character Type")]
        public string CharacterType { get; set; }
    }
}
