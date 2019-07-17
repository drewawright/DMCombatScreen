using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMCombatScreen.Models
{
    public class RunCombatAttack
    {
        public int ID { get; set; }
        public int CombatID { get; set; }
        public int CharacterID { get; set; }
        [Display(Name = "Character Name")]
        public string CharacterName { get; set; }
        [Display(Name = "Current HP")]
        public int? CurrentHP { get; set; }
        [Display(Name = "Max HP")]
        public int? MaxHP { get; set; }
        public int CurrentTurn { get; set; }
    }
}
