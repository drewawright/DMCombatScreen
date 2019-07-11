using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMCombatScreen.Data
{

    public class Attendance
    {
        public int ID { get; set; }
        public Guid OwnerID { get; set; }
        [ForeignKey(nameof(Character))]
        public int CharacterID { get; set; }
        public virtual Character Character { get; set; }
        [ForeignKey(nameof(Combat))]
        public int CombatID { get; set; }
        public virtual Combat Combat { get; set; }
        [Display(Name = "Current HP")]
        public int? CurrentHP { get; set; }
        public int? CurrentInitiative { get; set; }

        public virtual ICollection<Condition> Conditions { get; set; }
    }
}
