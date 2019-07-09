using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMCombatScreen.Models
{
    public class AttendanceAddCharacter
    {
        public int CombatID { get; set; }
        public string CombatName { get; set; }
        public List<AttendanceCharacterInfo> CharacterList { get; set; }
    }
}
