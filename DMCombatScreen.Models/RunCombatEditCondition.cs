using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMCombatScreen.Models
{
    public class RunCombatEditCondition
    {
        public int ID { get; set; }
        public int CharacterID { get; set; }
        public string CharacterName { get; set; }
        public int CombatID { get; set; }
        public int CurrentTurn { get; set; }
        public List<int> ConditionIDs { get; set; }
    }
}
