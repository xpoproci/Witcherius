using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Witcherius.BL.DataTransferObjects.Character;
using Witcherius.BL.DataTransferObjects.Item;
using Witcherius.BL.DataTransferObjects.User;
using Witcherius.BL.Facades;

namespace Witcherius.PL.Controllers
{
    [CustomAuthorize(Roles = "User")]
    public class CharacterController : Controller
    {
        public CharacterFacade CharacterFacade { get; set; }
        public EquipmentFacade EquipmentFacade { get; set; }
        public UserFacade UserFacade { get; set; }

        // GET: Character
        public async Task<ActionResult> Index()
        {
            var u = await UserFacade.GetUserAccordingToUsernameAsync(User.Identity.Name);
            if (u.Character == null)
            {
                return RedirectToAction("Create", new {id= u.Id});
            }
            return RedirectToAction("Detail", new { id = u.Character.Id });
        }

        [HttpGet]
        public async Task<ActionResult> Create(Guid? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            var user = await UserFacade.GetAsync(id.GetValueOrDefault());
            if (user.Character != null)
            {
                return RedirectToAction("Detail", new {id = user.Character.Id});
            }
            ViewBag.UserId = user.Id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CharacterCreateDto model)
        {
            model.Id = Guid.Empty;
            var u = new UserDto { Id = model.UserId };
            model.Id = await CharacterFacade.CreateAsync(model);
            await UserFacade.AssignAsync(u, model);
            return RedirectToAction("Index", "Home");
        }

        
        public async Task<ActionResult> Detail(Guid? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            var model = await CharacterFacade.GetAsync(id.GetValueOrDefault());
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            var u = await UserFacade.GetUserAccordingToUsernameAsync(User.Identity.Name);
            ViewBag.isMine = u.Character.Equals(model);
            return View(model);
        }
        
        public async Task<ActionResult> EquipItem(Guid itemId, Guid charId)
        {
            var item = await EquipmentFacade.GetItemAsync(itemId);
            var model = await CharacterFacade.GetAsync(charId);
            var charUpdate = new CharacterUpdateDto()
            {
                Id = model.Id,
                Name = model.Name,
                Experience = model.Experience,
                Gold = model.Gold,
                CurrentHp = model.CurrentHp + (item.Attributes.Hp*10),
                Sickness = model.Sickness,
                SkillPoints = model.SkillPoints
            };

            ItemDto equipped = null;
            foreach (var itemDto in model.Equipment.ListItems())
            {
                if (itemDto != null && itemDto.ArmorClass==item.ArmorClass)
                {
                    equipped = itemDto;
                }
            }

            await EquipmentFacade.DeleteFromInventoryAsync(model.Inventory, item);
            if (equipped != null)
            {
                charUpdate.CurrentHp -= equipped.Attributes.Hp * 10;
                await EquipmentFacade.AddToInventoryAsync(model.Inventory, equipped);
            }
            await EquipmentFacade.EquipItemAsync(model.Equipment, item);
            await CharacterFacade.EditAsync(charUpdate);
            return RedirectToAction("Detail");
        }
        

        public async Task<ActionResult> SellItem(Guid itemId, Guid charId)
        {
            var item = await EquipmentFacade.GetItemAsync(itemId);
            var model = await CharacterFacade.GetAsync(charId);


            var charUpdate = new CharacterUpdateDto() {
                Id = model.Id, Name = model.Name, Experience = model.Experience,
                Gold = model.Gold + item.Price, CurrentHp = model.CurrentHp, Sickness = model.Sickness, SkillPoints = model.SkillPoints};
            await EquipmentFacade.DeleteFromInventoryAsync(model.Inventory, item);
            await CharacterFacade.EditAsync(charUpdate);

            return RedirectToAction("Detail");
        }

        public async Task<ActionResult> Rest(Guid charId)
        {
            return await EatItem(null, charId);
        } 

        public async Task<ActionResult> EatItem(Guid? itemId, Guid charId)
        {
            var model = await CharacterFacade.GetAsync(charId);
            if (model.CurrentHp == model.CalculateAttributes().Hp * 10)
            {
                return RedirectToAction("Detail");
            }
            var toHeal = model.CalculateAttributes().Hp * 10 - model.CurrentHp;
            DateTime? sickness = DateTime.Now.AddHours(1);
            if(itemId!=null)
            {
                sickness = model.Sickness;
                var item = await EquipmentFacade.GetItemAsync(itemId.GetValueOrDefault());
                toHeal = (item.Attributes.Hp * 10) >= toHeal ? toHeal : (item.Attributes.Hp * 10);
                await EquipmentFacade.DeleteFromInventoryAsync(model.Inventory, item);
            }

            var charUpdate = new CharacterUpdateDto()
            {
                Id = model.Id,
                Name = model.Name,
                Experience = model.Experience,
                Gold = model.Gold,
                CurrentHp = model.CurrentHp + toHeal,
                Sickness = sickness,
                SkillPoints = model.SkillPoints
            };
            await CharacterFacade.EditAsync(charUpdate);

            return RedirectToAction("Detail");
        }
    }
}