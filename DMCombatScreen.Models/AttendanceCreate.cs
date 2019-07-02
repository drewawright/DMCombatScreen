using DMCombatScreen.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMCombatScreen.Models
{
    public class AttendanceCreate
    {
        public int CharacterID { get; set; }
        public Character Character { get; set; }
        public int CombatID { get; set; }
        public Combat Combat { get; set; }
    }
}
