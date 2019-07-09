using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMCombatScreen.Models
{
    public class AttendanceAddCharactersFiltersVM
    {
        public int CombatID { get; set; }
        public string CombatName { get; set; }
        public List<AttendanceCharacterInfo> CharacterList { get; set; }
        public IEnumerable<FilterCategoryVM> FilterCategories { get; set; }
    }

    public class FilterCategoryVM
    {

    }
}
