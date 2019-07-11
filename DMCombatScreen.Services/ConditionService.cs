using DMCombatScreen.Data;
using DMCombatScreen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMCombatScreen.Services
{
    public class ConditionService
    {
        private readonly Guid _userID;
        public ConditionService(Guid userID)
        {
            _userID = userID;
        }

        public bool CreateCondition(ConditionCreate model)
        {
            var entity =
                new Condition
                {
                    ConditionName = model.ConditionName,
                    ConditionID = model.ConditionID
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Conditions.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<ConditionListItem> GetConditionList()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Conditions
                        .Select(
                        e => new ConditionListItem
                        {
                            ConditionID = e.ConditionID,
                            ConditionName = e.ConditionName
                        });
                return query.ToArray();
            }
        }

        public ConditionDetail GetConditionByID(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Conditions
                        .Single(e => e.ConditionID == id);
                return
                    new ConditionDetail
                    {
                        ConditionID = entity.ConditionID,
                        ConditionName = entity.ConditionName
                    };
            }
        }

        public bool UpdateCondition(ConditionEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Conditions
                        .Single(e => e.ConditionID == model.ConditionID);
                entity.ConditionName = model.ConditionName;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteCondition(int conditionID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Conditions
                        .Single(e => e.ConditionID == conditionID);

                ctx.Conditions.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }
    }
}
