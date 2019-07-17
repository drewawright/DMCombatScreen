using DMCombatScreen.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMCombatScreen.Models
{
    public class RunCombatListItem
    {
        public int ID { get; set; }
        public int CombatID { get; set; }
        [Display(Name = "Combat Name")]
        public string CombatName { get; set; }
        public int? TotalInitiative { get; set; }
        public bool InitiativeRolled { get; set; }
    }
}
