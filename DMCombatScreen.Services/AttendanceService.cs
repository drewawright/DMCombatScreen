using DMCombatScreen.Data;
using DMCombatScreen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMCombatScreen.Services
{
    public class AttendanceService
    {
        private readonly Guid _userID;

        public AttendanceService(Guid userID)
        {
            _userID = userID;
        }

       public IEnumerable<AttendanceListItem> GetAttendances()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Attendances
                        .Where(e => e.OwnerID == _userID)
                        .Select(
                        e =>
                            new AttendanceListItem
                            {
                                ID = e.ID,
                                Character = e.Character,
                                Combat = e.Combat
                            });
                return query.ToArray();
            }
        }
    }
}
