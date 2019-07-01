using DMCombatScreen.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMCombatScreen.Models
{
    public class AttendanceListItem
    {
        public int ID { get; set; }
        public Character Character { get; set; }
        public Combat Combat { get; set; }
    }
}
