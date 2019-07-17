using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMCombatScreen.Models
{
    public class RunCombatInitiative
    {
        public int ID { get; set; }
        public int CombatID { get; set; }
        [Display(Name = "Combat Name")]
        public string CombatName { get; set; }
        public int CharacterID { get; set; }
        [Display (Name = "Character Name")]
        public string CharacterName { get; set; }
        [Display (Name = "Enter Initiative")]
        public int? InitiativeEntry { get; set; }
        public int? InitiativeModifier { get; set; }
        public int? InitiativeAbilityScore { get; set; }
        [Display(Name ="Current HP")]
        public int? CurrentHP { get; set; }
        public bool IsPlayer { get; set; }
    }
}
