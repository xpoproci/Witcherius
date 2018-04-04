using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Witcherius.BL.DataTransferObjects.Attributes;
using Witcherius.BL.DataTransferObjects.Character;
using Witcherius.BL.DataTransferObjects.Combat;
using Witcherius.BL.DataTransferObjects.Monster;
using Witcherius.BL.DataTransferObjects.Quest;
using Witcherius.BL.Facades.Common;
using Witcherius.BL.Services.Character;
using Witcherius.BL.Services.Monster;
using Witcherius.Entities;
using Witcherius.Infrastructure.UnitOfWork;

namespace Witcherius.BL.Facades
{
    public class CombatFacade : BaseFacade
    {
        private readonly IMonsterService _monsterService;
        private readonly ICharacterService _characterService;
        private Random _random;

        public CombatFacade(IUnitOfWorkProvider unitOfWorkProvider, ICharacterService characterService, IMonsterService monsterService) : base(unitOfWorkProvider)
        {
            _characterService = characterService;
            _monsterService = monsterService;
        }

        public async Task<Tuple<bool, List<string>>> FightAsync(CharacterDto character, MonsterDto monster, QuestDto quest)
        {
            var log = new List<string>();
            _random = new Random();
            if (character.Sickness > DateTime.Now)
            {
                var diff = Math.Abs(Math.Floor((character.Sickness - DateTime.Now).Value.TotalSeconds));
                log.Add($"You can't fight at the moment. You are too tired and will not be able to fight for {diff} seconds.");
                return new Tuple<bool, List<string>>(false, log);
            }

            if (character.CurrentHp <= 0)
            {
                log.Add("You can't fight at the moment. You should get some rest, because you are too devasteted.");
                return new Tuple<bool, List<string>>(false, log);
            }

            var characterAttr = character.CalculateAttributes();
            characterAttr.Hp = character.CurrentHp / 10; //setting hp to current value
            var combat = Task.Run(()=>Combat(characterAttr, monster.Attributes, log));
            var result = await combat;

            using (var uow = UnitOfWorkProvider.Create())
            {
                var updatedUser = new CharacterUpdateDto()
                {
                    Name = character.Name,
                    CurrentHp = result.Item2,
                    Experience = character.Experience,
                    Gold = character.Gold,
                    Id = character.Id,
                    SkillPoints = character.SkillPoints,
                    Sickness = DateTime.Now.AddMinutes(10)
                };
                if (result.Item1)
                {
                    updatedUser.Experience += quest.Experience;
                    updatedUser.Gold += quest.Gold;
                }

                await _characterService.Update(updatedUser);
                await uow.Commit();
            }
            return new Tuple<bool, List<string>>(result.Item1, log);
        }

        public async Task<Tuple<bool, List<string>>> FightAsync(CharacterDto character, CharacterDto character2)
        {
            var log = new List<string>();
            _random = new Random();
            if (character.Sickness > DateTime.Now)
            {
                var diff = Math.Abs(Math.Floor((character.Sickness - DateTime.Now).Value.TotalSeconds));
                log.Add($"You can't fight at the moment. You are too tired and will not be able to fight for {diff} seconds.");
                return new Tuple<bool, List<string>>(false, log);
            }

            if (character.CurrentHp <= 0)
            {
                log.Add("You can't fight at the moment. You should get some rest, because you are too devasteted.");
                return new Tuple<bool, List<string>>(false, log);
            }

            if (character2.CurrentHp <= 0)
            {
                log.Add("It wouldn't be very nice if you attacked your opponent right now. He needs to lick his wounds first.");
                return new Tuple<bool, List<string>>(false, log);
            }


            var player1 = character.CalculateAttributes();
            var player2 = character2.CalculateAttributes();
            player1.Hp = character.CurrentHp / 10; //setting hp to current value
            player2.Hp = character.CurrentHp / 10; //setting hp to current value
            var combat = Task.Run(() => Combat(player1, player2, log));
            var result = await combat;

            using (var uow = UnitOfWorkProvider.Create())
            {
                var updatedUser1 = new CharacterUpdateDto()
                {
                    Name = character.Name,
                    CurrentHp = result.Item2,
                    Experience = character.Experience,
                    Gold = character.Gold,
                    Id = character.Id,
                    SkillPoints = character.SkillPoints,
                    Sickness = DateTime.Now.AddMinutes(10)
                };

                var updatedUser2 = new CharacterUpdateDto()
                {
                    Name = character2.Name,
                    CurrentHp = result.Item3,
                    Experience = character2.Experience,
                    Gold = character2.Gold,
                    Id = character2.Id,
                    SkillPoints = character2.SkillPoints,
                    Sickness = character2.Sickness
                };

                if (result.Item1)
                {
                    updatedUser2.Gold -= (int) Math.Floor((float) updatedUser2.Gold / 100) * 10;
                    updatedUser1.Gold += (int) Math.Floor((float) updatedUser2.Gold / 100) * 10;
                }
                else
                {
                    updatedUser1.Gold -= (int)Math.Floor((float)updatedUser1.Gold / 100) * 10;
                    updatedUser2.Gold += (int)Math.Floor((float)updatedUser1.Gold / 100) * 10;
                }

                await _characterService.Update(updatedUser1);
                await _characterService.Update(updatedUser2);
                await uow.Commit();
            }
            return new Tuple<bool, List<string>>(result.Item1, log);
        }


        private Tuple<bool, int, int> Combat(AttributesDto p1, AttributesDto p2, ICollection<string> logger)
        {
            var whoseTurn = _random.Next(0, 100);

            var player1 = new CombatDto(p1, true);
            var player2 = new CombatDto(p2);

            logger.Add("You found yourself alone, riding in the green fields with the sun on your face, but you are not be troubled. For you are in Elysium, and you\'re already dead!");

            if (whoseTurn < 50) //50% for each side to start
            {
                player1.First = false;
                logger.Add("Your opponent took his chance and attacked you first.");
            }
            else
            {
                logger.Add("Your instincts awoke you and you took your chance to assault first.");
            }
            var i = 1;

            while (player1.Hp > 0 && player2.Hp > 0)
            {
                logger.Add($"------------Round number {i}------------");
                if (player1.First)
                {
                    var hit = Hit(p1.Damage, p2.Defense, logger, true);
                    player2.Hp -= hit;
                    if (player2.Hp <= 0) break;
                    
                    hit = Hit(p2.Damage, p1.Defense, logger);
                    player1.Hp -= hit;
                    if (player1.Hp <= 0) break;
                }
                else
                {
                    var hit = Hit(p2.Damage, p1.Defense, logger);
                    player1.Hp -= hit;
                    if (player1.Hp <= 0) break;
                    
                    hit = Hit(p1.Damage, p2.Defense, logger, true);
                    player2.Hp -= hit;
                    if (player2.Hp <= 0) break;
                }
                i++;
            }

            if (player1.Hp <= 0)
            {
                logger.Add("You died. (No darksouls pun intended)");
                return new Tuple<bool, int, int>(false, 0, player2.Hp);
            }
            logger.Add("Congratulations, you have defeated the opponent.");
            return new Tuple<bool, int, int>(true, player1.Hp, 0);
        }

        private int Hit(int dmg, int def, ICollection<string> logger, bool myTurn = false)
        {
            var resultString = new StringBuilder();
            var criticalStrike = _random.Next(0, 100);
            if (criticalStrike < 15) //15% chance for crit
            {
                dmg *= 2;
                resultString.Append(myTurn ? "You" : "Opponent");
                resultString.Append(" critically striked. ");
            }

            var superDodge = _random.Next(0, 100);
            if (superDodge < 15) //15% to double the defense
            {
                def *= 2;
                resultString.Append(!myTurn ? "You" : "Opponent");
                resultString.Append(" activated super defence. ");
            }

            var result = dmg - def;
            result = (result < 0 ? 0 : result);
            resultString.Append(myTurn ? "You" : "Opponent");
            resultString.Append($" did {result} damage in total.");

            logger.Add(resultString.ToString());
            return result;
        }
    }
}
