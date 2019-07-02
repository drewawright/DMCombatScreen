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

        public bool CreateAttendance(AttendanceCreate model)
        {
            var entity =
                new Attendance
                {
                    OwnerID = _userID,
                    CharacterID = model.CharacterID,
                    CombatID = model.CombatID,
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Attendances.Add(entity);
                return ctx.SaveChanges() == 1;
            }
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

        public AttendanceDetail GetAttendanceByID(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Attendances
                        .Single(e => e.ID == id && e.OwnerID == _userID);
                return
                    new AttendanceDetail
                    {
                        CharacterID = entity.CharacterID,
                        CharacterName = entity.Character.Name,
                        CombatID = entity.CombatID,
                        CombatName = entity.Combat.Name,
                        CurrentHP = entity.Character.MaxHP
                    };
            }
        }

        public bool UpdateAttendance(AttendanceEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Attendances
                        .Single(e => e.ID == model.ID && e.OwnerID == _userID);

                entity.CharacterID = model.CharacterID;
                entity.CombatID = model.CombatID;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteAttendance(int attendanceID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Attendances
                        .Single(e => e.ID == attendanceID && e.OwnerID == _userID);

                ctx.Attendances.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
