using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMCombatScreen.Data
{
    public class Character
    {
        [Key]
        public int CharacterID { get; set; }
        [Required]
        [Display(Name = "Character Name")]
        [MaxLength(100, ErrorMessage = "Character Name cannot be more than 100 characters")]
        public string Name { get; set; }
        [Display(Name = "Max HP")]
        public int? MaxHP { get; set; }
        //public int CurrentHP { get; set; }
        [Display(Name = "Initiative Roll")]
        public int? InitiativeRoll { get; set; }
        [Display(Name = "Initiative Modifier")]
        public int? InitiativeModifier { get; set; }
        [Display(Name = "Initiative Ability Score")]
        public int? InitiativeAbilityScore { get; set; }
        [Display(Name = "Initiative")]
        public int? TotalInitiative { get; set; }
        [Required]
        public bool IsPlayer { get; set; }

    }
}
