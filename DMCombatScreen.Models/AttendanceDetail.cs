using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMCombatScreen.Models
{
    public class AttendanceDetail
    {
        public int ID { get; set; }
        [Display(Name = "Character ID")]
        public int CharacterID { get; set; }
        [Display(Name = "Character Name")]
        public string CharacterName { get; set; }
        [Display(Name = "Combat ID")]
        public int CombatID { get; set; }
        [Display(Name = "Combat Name")]
        public string CombatName { get; set; }
        public int? CurrentHP { get; set; }

    }
}
