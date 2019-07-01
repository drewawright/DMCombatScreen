using DMCombatScreen.Data;
using DMCombatScreen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMCombatScreen.Services
{
    public class CombatService
    {
        private readonly Guid _userID;

        public CombatService(Guid userID)
        {
            _userID = userID;
        }

        public bool CreateCombat(CombatCreate model)
        {
            var entity =
                new Combat()
                {
                    OwnerID = _userID,
                    Name = model.Name
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Combats.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public CombatDetail GetCombatByID(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Combats
                        .Single(e => e.CombatID == id && e.OwnerID == _userID);
                return
                    new CombatDetail
                    {
                        CombatID = entity.CombatID,
                        Name = entity.Name
                    };
            }
        }

        public IEnumerable<CombatListItem> GetCombats()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Combats
                        .Where(e => e.OwnerID == _userID)
                        .Select(
                        e =>
                            new CombatListItem
                            {
                                CombatID = e.CombatID,
                                Name = e.Name
                            }
                        );
                return query.ToArray();
            }
        }
    }
}
