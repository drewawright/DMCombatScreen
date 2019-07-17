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
                Random random = new Random();
                foreach (var character in model)
                {
                    var existing = ctx.Attendances.Find(character.ID);
                    if (existing.Character.IsPlayer)
                    {
                        existing.CurrentInitiative = character.InitiativeEntry;
                    }
                    else
                    {
                        existing.Character.InitiativeRoll = random.Next(1, 21);
                        existing.CurrentInitiative = existing.Character.InitiativeRoll + existing.Character.InitiativeModifier;
                        if (character.CurrentHP == null)
                        {
                            existing.CurrentHP = (int)existing.Character.MaxHP;
                        }
                        else existing.CurrentHP = character.CurrentHP;
                    }
                }

                var actual = ctx.SaveChanges();
                return actual > 0;
            }
        }

        public List<RunCombatCharacter> GetCombatCharacterList(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                        ctx.Attendances
                        .Where(e => e.CombatID == id && e.OwnerID == _userID)
                        .Select(
                            e => new RunCombatCharacter
                            {
                                ID = e.ID,
                                CharacterID = e.CharacterID,
                                CharacterName = e.Character.Name,
                                CombatID = e.CombatID,
                                CombatName = e.Combat.Name,
                                TotalInitiative = e.CurrentInitiative,
                                InitiativeAbilityScore = e.Character.InitiativeAbilityScore,
                                MaxHP = e.Character.MaxHP,
                                CurrentHP = e.CurrentHP,
                                IsPlayer = e.Character.IsPlayer,
                                Conditions = new string[0] { }.ToList()
                            });

                List<RunCombatCharacter> characterList = query.ToList();

                foreach (var character in characterList)
                {
                    List<string> conditionsList = GetCharacterConditionNames(character);
                }

                return characterList;
            }
        }

        public RunCombatAttack GetOneCombatant(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx.Attendances
                    .Single(e => e.ID == id);

                return
                    new RunCombatAttack
                    {
                        ID = query.ID,
                        CharacterID = query.CharacterID,
                        CharacterName = query.Character.Name,
                        CombatID = query.CombatID,
                        MaxHP = query.Character.MaxHP,
                        CurrentHP = query.CurrentHP,
                    };
            }
        }

        public bool UpdateCharacterHP(RunCombatAttack model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx.Attendances
                    .Single(e => e.ID == model.ID);

                entity.CurrentHP = model.CurrentHP;

                return ctx.SaveChanges() == 1;
            }
        }

        public RunCombatEditCondition GetEditCharacterConditionsModel(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx.Attendances
                    .Include(e => e.Conditions)
                    .Single(e => e.ID == id);

                RunCombatEditCondition model =
                    new RunCombatEditCondition
                    {
                        ID = query.ID,
                        CharacterID = query.CharacterID,
                        CharacterName = query.Character.Name,
                        CombatID = query.CombatID,
                    };
                List<int> conditionIDs = new List<int>();
                foreach(var condition in query.Conditions)
                {
                    conditionIDs.Add(condition.ConditionID);
                }
                model.ConditionIDs = conditionIDs;

                return model;
            }

        }

        public void UpdateCharacterConditions(string[] selectedConditions, RunCombatEditCondition model)
        {
            //Update conditions on the model based on checkboxes
            if (selectedConditions == null)
            {
                selectedConditions = new string[0] { };
                model.ConditionIDs = new List<int>();
            }

            if (model.ConditionIDs == null)
            {
                model.ConditionIDs = new List<int>();
            }

            var selectedConditionsHS = new HashSet<string>(selectedConditions);
            var characterConditions = new HashSet<int>(model.ConditionIDs);
            using (var ctx = new ApplicationDbContext())
            {
                var conditions = ctx.Conditions;
                foreach(var condition in conditions)
                {
                    if (selectedConditionsHS.Contains(condition.ConditionID.ToString()))
                    {
                        if (!characterConditions.Contains(condition.ConditionID))
                        {
                            model.ConditionIDs.Add(condition.ConditionID);
                        }
                    }
                    else
                    {
                        if (characterConditions.Contains(condition.ConditionID))
                        {
                            model.ConditionIDs.Remove(condition.ConditionID);
                        }
                    }
                }
            }
        }
        
        public bool UpdateAttendanceConditions(RunCombatEditCondition model)
        {
            //Change attendances in db based on model
            using(var ctx = new ApplicationDbContext())
            {
                var attendance = ctx
                    .Attendances
                    .Where(e => e.ID == model.ID)
                    .Include(e => e.Conditions)
                    .Single();

                var newConditions = new HashSet<int>(model.ConditionIDs);
                var oldConditions = new HashSet<int>();

                foreach (var condition in attendance.Conditions)
                {
                    oldConditions.Add(condition.ConditionID);
                }

                var conditions = ctx.Conditions;
                foreach(var condition in conditions)
                {
                    if (newConditions.Contains(condition.ConditionID))
                    {
                        if (!oldConditions.Contains(condition.ConditionID))
                        {
                            attendance.Conditions.Add(condition);
                        }
                    }
                    else
                    {
                        if (oldConditions.Contains(condition.ConditionID))
                        {
                            attendance.Conditions.Remove(condition);
                        }
                    }
                }

                var actual = ctx.SaveChanges();

                return actual >= 1;
            }
        }

        private List<string> GetCharacterConditionNames(RunCombatCharacter model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var attendance =
                    ctx
                        .Attendances
                        .Where(e => e.ID == model.ID)
                        .Single();
                foreach(var condition in attendance.Conditions)
                {
                    model.Conditions.Add(condition.ConditionName);
                }
            }
            return model.Conditions;
        }
    }
}