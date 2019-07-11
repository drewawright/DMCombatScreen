using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMCombatScreen.Data
{
    public class Condition
    {
        public int ConditionID { get; set; }
        public string ConditionName { get; set; }

        public virtual ICollection<Attendance> Attendances { get; set; }
    }
}
