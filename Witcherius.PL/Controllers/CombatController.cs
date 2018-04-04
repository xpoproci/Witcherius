using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Witcherius.BL.DataTransferObjects.Character;
using Witcherius.BL.DataTransferObjects.Messages;
using Witcherius.BL.DataTransferObjects.Monster;
using Witcherius.BL.DataTransferObjects.User;
using Witcherius.BL.Facades;
using Witcherius.PL.Models;

namespace Witcherius.PL.Controllers
{
    [CustomAuthorize(Roles = "User")]
    public class CombatController : Controller
    {
        public UserFacade UserFacade { get; set; }
        public MonsterFacade MonsterFacade { get; set; }
        public QuestFacade QuestFacade { get; set; }
        public CharacterFacade CharacterFacade { get; set; }
        public CombatFacade CombatFacade { get; set; }
        public EquipmentFacade EquipmentFacade { get; set; }

        public ActionResult Index()
        {
            return RedirectToAction("Index", "User");
        }

        public async Task<ActionResult> Fight(Guid? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "User");
            }

            var player1 = await UserFacade.GetUserAccordingToUsernameAsync(User.Identity.Name);
            var player2 = await UserFacade.GetAsync(id.GetValueOrDefault());
            var model = new CombatModel() {Player1 = player1, Player2 = player2};
            return View(model);
        }

        public async Task<ActionResult> GetMessagesMonster(Guid qId)
        {
            var user = await UserFacade.GetUserAccordingToUsernameAsync(User.Identity.Name);

            var quest = await QuestFacade.GetQuestAsync(qId);
            var result = await CombatFacade.FightAsync(user.Character, quest.Monster, quest);

            //update character if won
            if (result.Item1)
            {
                var messageText = $"Congratulations you won the match and your reward is: {quest.Gold} gold, {quest.Experience} experience.";
                if (quest.Item != null)
                {
                    var item = await EquipmentFacade.AddToInventoryAsync(user.Character.Inventory, quest.Item);
                    if (item)
                    {
                        messageText +=
                            $"\nYou also receieved an item called {quest.Item.Name}, which is in your inventory.";
                    }
                }
                
                await UserFacade.MessageCreateAsync(user, new MessageDto() {MessageText = messageText, Arrived = DateTime.Now });
            }

            var list = result.Item2.Select(x => new {Message = x}).ToList();
            return Json(new {Winner = result.Item1, List = list}, JsonRequestBehavior.AllowGet);
        }
        
        public async Task<ActionResult> GetMessagesChar(Guid userId)
        {
            var user = await UserFacade.GetUserAccordingToUsernameAsync(User.Identity.Name);
            var user2 = await UserFacade.GetAsync(userId);
            var result = await CombatFacade.FightAsync(user.Character, user2.Character);
            var list = result.Item2.Select(x => new { Message = x }).ToList();
            if (result.Item1)
            {
                var updateUser = new UserEditDto()
                {
                    Credits = user.Credits,
                    CharacterId = user.CharacterId,
                    Id = user.Id,
                    Score = user.Score + (user2.Character.Level)
                };
                await UserFacade.EditAsync(updateUser);
                var player1Msg = $"Congratulations you won the match and you robbed your enemy for {(int)Math.Floor((float)user2.Character.Gold / 100) * 10} gold.";
                var player2Msg = $"We are sorry, but {user.Character.Name} defeated you and took you {(int)Math.Floor((float)user2.Character.Gold / 100) * 10} gold.";
                await UserFacade.MessageCreateAsync(user, new MessageDto() { MessageText = player1Msg, Arrived = DateTime.Now});
                await UserFacade.MessageCreateAsync(user2, new MessageDto() { MessageText = player2Msg, Arrived = DateTime.Now});
            }
            else
            {
                if (list.Count() > 1)
                {
                    var player2Msg = $"Congratulations you have been able to defend yourself from {user.Character.Name} and you also robbed him for {(int)Math.Floor((float)user.Character.Gold / 100) * 10} gold.";
                    var player1Msg = $"We are sorry, You got devastated and robbed for {(int)Math.Floor((float)user.Character.Gold / 100) * 10} gold.";
                    await UserFacade.MessageCreateAsync(user, new MessageDto() { MessageText = player1Msg, Arrived = DateTime.Now });
                    await UserFacade.MessageCreateAsync(user2, new MessageDto() { MessageText = player2Msg, Arrived = DateTime.Now });
                }
            }
            return Json(new { Winner = result.Item1, List = list }, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> FightForCredits()
        {
            var user = await UserFacade.GetUserAccordingToUsernameAsync(User.Identity.Name);
            if (user.Credits < 10) return Json(new {CanFight = false}, JsonRequestBehavior.AllowGet);
            var charUpdate = new CharacterUpdateDto()
            {
                Id = user.Character.Id,
                Name = user.Character.Name,
                Experience = user.Character.Experience,
                Gold = user.Character.Gold,
                CurrentHp = user.Character.CurrentHp,
                SkillPoints = user.Character.SkillPoints,
                Sickness = null
            };
            await CharacterFacade.EditAsync(charUpdate);
            var userUpdate = new UserEditDto() {Credits = user.Credits-10, CharacterId = user.CharacterId, Id = user.Id, Score = user.Score};
            await UserFacade.EditAsync(userUpdate);
            return Json(new { CanFight = true}, JsonRequestBehavior.AllowGet);
        }

    }
}