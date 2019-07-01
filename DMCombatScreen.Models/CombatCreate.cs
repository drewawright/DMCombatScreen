using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMCombatScreen.Models
{
    public class CombatCreate
    {
        [Required]
        [Display(Name = "Combat Name")]
        [MaxLength(100, ErrorMessage = "Combat Name cannot be more than 100 characters")]
        public string Name { get; set; }
    }
}
