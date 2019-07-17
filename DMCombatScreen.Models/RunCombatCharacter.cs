using DMCombatScreen.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMCombatScreen.Models
{
    public class RunCombatCharacter
    {
        public int ID { get; set; }
        public int CombatID { get; set; }
        public string CombatName { get; set; }
        public int CharacterID { get; set; }
        [Display(Name = "Character Name")]
        public string CharacterName { get; set; }
        [Display(Name = "Total Initiative")]
        public int? TotalInitiative { get; set; }
        public int? InitiativeAbilityScore { get; set; }
        public bool IsPlayer { get; set; }
        [Display(Name = "Current HP")]
        public double? CurrentHP { get; set; }
        [Display(Name = "Max HP")]
        public double? MaxHP { get; set; }
        public int TurnOrderNumber { get; set; }
        public List<string> Conditions { get; set; }
    }
}
