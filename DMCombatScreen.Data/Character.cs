using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMCombatScreen.Data
{
    public enum CharacterType
    {
        Abberation = 1,
        Beast,
        Celestial,
        Construct,
        Dragon,
        Elemental,
        Fey,
        Fiend,
        Giant,
        Humanoid,
        Monstrosity,
        Ooze,
        Plant,
        Undead,
        Other
    }

    public class Character
    {
        [Key]
        public int CharacterID { get; set; }
        [Required]
        [Display(Name = "Character Name")]
        [MaxLength(100, ErrorMessage = "Character Name cannot be more than 100 characters")]
        public string Name { get; set; }
        [Display(Name = "Max HP")]
        public int? MaxHP { get; set; }
        [Display(Name = "Initiative Roll")]
        public int? InitiativeRoll { get; set; }
        [Display(Name = "Initiative Modifier")]
        public int? InitiativeModifier { get; set; }
        [Display(Name = "Initiative Ability Score")]
        public int? InitiativeAbilityScore { get; set; }
        public CharacterType TypeOfCharacter { get; set; }
        [Required]
        [Display(Name = "Player?")]
        public bool IsPlayer { get; set; }

    }
}
