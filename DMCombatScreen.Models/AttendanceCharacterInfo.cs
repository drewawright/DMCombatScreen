using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMCombatScreen.Models
{
    public class AttendanceCharacterInfo
    {
        public int CharacterID { get; set; }
        public string CharacterName { get; set; }
        public bool IsChecked { get; set; }
        
        public int? NumberOfAttendances { get; set; }
        public int CharacterTypeValue { get; set; }
        [Display(Name = "Character Type")]
        public string CharacterTypeName { get; set; }
    }
}
