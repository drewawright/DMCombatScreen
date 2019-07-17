using DMCombatScreen.Data;
using DMCombatScreen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMCombatScreen.Services
{
    public class CharacterService
    {
        private readonly Guid _userID;

        public CharacterService(Guid userID)
        {
            _userID = userID;
        }

        public bool CreateCharacter(CharacterCreate model)
        {
            CharacterType charType;
            if (Enum.TryParse(model.CharacterType, out charType))
                {
                Enum.Parse(typeof(CharacterType), model.CharacterType);
                }
            var entity =
                new Character()
                {
                    Name = model.Name,
                    IsPlayer = model.IsPlayer,
                    MaxHP = model.MaxHP,
                    InitiativeAbilityScore = model.InitiativeAbilityScore,
                    InitiativeModifier = model.InitiativeModifier,
                    TypeOfCharacter = charType,
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Characters.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<CharacterListItem> GetCharacters()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Characters
                        .Select(
                        e => new CharacterListItem
                        {
                            CharacterID = e.CharacterID,
                            Name = e.Name,
                            IsPlayer = e.IsPlayer,
                            CharacterTypeValue = (int)e.TypeOfCharacter,
                            CharacterType = e.TypeOfCharacter.ToString()
                        }
                        );
                return query.ToArray();
            }
        }

        public CharacterDetail GetCharacterByID(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx.
                        Characters.
                        Single(e => e.CharacterID == id);
                return
                    new CharacterDetail
                    {
                        CharacterID = entity.CharacterID,
                        Name = entity.Name,
                        MaxHP = entity.MaxHP,
                        InitiativeModifier = entity.InitiativeModifier,
                        InitiativeAbilityScore = entity.InitiativeAbilityScore,
                        IsPlayer = entity.IsPlayer,
                        CharacterTypeValue = (int)entity.TypeOfCharacter,
                        CharacterType = entity.TypeOfCharacter.ToString()
                    };
            }
        }

        public List<AttendanceCharacterInfo> GetAttendanceCharacterInfos()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Characters
                        .Select(
                        e => new AttendanceCharacterInfo
                        {
                            CharacterID = e.CharacterID,
                            CharacterName = e.Name,
                            CharacterTypeName = e.TypeOfCharacter.ToString(),
                            CharacterTypeValue = (int)e.TypeOfCharacter
                        }
                        );
                return query.ToList();
            }
        }

        public bool UpdateCharacter(CharacterEdit model)
        {
            CharacterType charType;
           if (Enum.TryParse(model.CharacterTypeValue.ToString(), out charType))
            {
                Enum.Parse(typeof(CharacterType), model.CharacterTypeValue.ToString());
            }

            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Characters
                        .Single(e => e.CharacterID == model.CharacterID);

                entity.Name = model.Name;
                entity.MaxHP = model.MaxHP;
                entity.InitiativeModifier = model.InitiativeModifier;
                entity.InitiativeAbilityScore = model.InitiativeAbilityScore;
                entity.IsPlayer = model.IsPlayer;
                entity.TypeOfCharacter = charType;

                var actual = ctx.SaveChanges();

                return actual == 1;
            }
        }

        public bool DeleteCharacter(int characterID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Characters
                        .Single(e => e.CharacterID == characterID);
                ctx.Characters.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
