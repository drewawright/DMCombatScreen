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
        public string Name { get; set; }
        public int MaxHP { get; set; }
        [Display(Name = "Current HP")]
        //public int CurrentHP { get; set; }
        public int InitiativeRoll { get; set; }
        public int InitiativeModifier { get; set; }
        public int InitiativeAbilityScore { get; set; }
        [Display(Name = "Initiative")]
        public int TotalInitiative { get { return InitiativeRoll + InitiativeModifier; } }
        [Required]
        public bool IsPlayer { get; set; }

    }
}
