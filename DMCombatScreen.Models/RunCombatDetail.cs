using DMCombatScreen.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMCombatScreen.Models
{
    public class RunCombatDetail
    {
        public int ID { get; set; }
        public int CombatID { get; set; }
        public virtual Combat Combat { get; set; }
        public int CharacterID { get; set; }
        public virtual Character Character { get; set; }
        public int? CurrentHP { get; set; }
        public int InitiativeEntry { get; set; }
        public int? CurrentInitiative { get; set; }
    }
}
