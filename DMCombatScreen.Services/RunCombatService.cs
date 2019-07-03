using DMCombatScreen.Data;
using DMCombatScreen.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMCombatScreen.Services
{
    public class RunCombatService
    {
        private readonly Guid _userID;

        public RunCombatService(Guid userID)
        {
            _userID = userID;
        }

        public IEnumerable<RunCombatListItem> GetCombatList()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Attendances
                        .Where(e => e.OwnerID == _userID)
                        .Select(
                        e =>
                            new RunCombatListItem
                            {
                                //ID = e.ID,
                                CombatID = e.CombatID,
                                CombatName = e.Combat.Name
                            })
                        .Distinct();

                return query.ToArray();
            }
        }

        public IEnumerable<RunCombatDetail> GetCombatantsList(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Attendances
                    .Where(e => e.CombatID == id && e.OwnerID == _userID)
                    .Select(
                        e =>
                            new RunCombatDetail
                            {
                                ID = e.ID,
                                CharacterID = e.CharacterID,
                                CharacterName = e.Character.Name,
                                CombatID = e.CombatID,
                                CombatName = e.Combat.Name,
                                CurrentHP = e.CurrentHP,
                                IsPlayer = e.Character.IsPlayer,
                                CurrentInitiative = e.CurrentInitiative
                            });
                return query.ToArray();
            }
        }

        public List<RunCombatInitiative> GetInitiativeList(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Attendances
                        .Where(e => e.CombatID == id && e.OwnerID == _userID)
                        .Select(
                        e =>
                        new RunCombatInitiative
                        {
                            ID = e.ID,
                            CombatID = e.CombatID,
                            CombatName = e.Combat.Name,
                            CharacterID = e.CharacterID,
                            CharacterName = e.Character.Name,
                            InitiativeModifier = e.Character.InitiativeModifier,
                            InitiativeAbilityScore = e.Character.InitiativeAbilityScore,
                            IsPlayer = e.Character.IsPlayer,
                        });
                return query.ToList();
            }
        }

        public bool SetInitiatives(List<RunCombatInitiative> model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                foreach (var character in model)
                {
                    var existing = ctx.Attendances.Find(character.ID);
                    if (existing.Character.IsPlayer)
                    {
                        existing.CurrentInitiative = character.InitiativeEntry;
                    }
                    else
                    {
                        existing.Character.InitiativeRoll = character.InitiativeEntry;
                        existing.CurrentInitiative = existing.Character.InitiativeRoll + existing.Character.InitiativeModifier;
                        if (character.CurrentHP == null)
                        {
                            existing.CurrentHP = (int)existing.Character.MaxHP;
                        }
                        else existing.CurrentHP = character.CurrentHP;
                    }
                }

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
