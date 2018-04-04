using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Witcherius.BL.DataTransferObjects.Character;
using Witcherius.BL.DataTransferObjects.Item;
using Witcherius.BL.Facades;
using Witcherius.PL.Models;

namespace Witcherius.PL.Controllers
{
    [CustomAuthorize(Roles = "User")]
    public class ShopController : Controller
    {
        public ShopFacade ShopFacade { get; set; }
        public UserFacade UserFacade { get; set; }
        public EquipmentFacade EquipmentFacade { get; set; }
        public CharacterFacade CharacterFacade { get; set; }

        // GET: Shop
        public async Task<ActionResult> Index()
        {
            var u = await UserFacade.GetUserAccordingToUsernameAsync(User.Identity.Name);
            if (u.Character == null)
            {
                return RedirectToAction("Detail", "User");
            }

            var list = await ShopFacade.GetAllAsync();
            return View(list);
        }

        public async Task<ActionResult> Detail(Guid? id, int page=1)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            var u = await UserFacade.GetUserAccordingToUsernameAsync(User.Identity.Name);
            var lowerBound = u.Character.Level-2;
            var upperBound = lowerBound + 4;
            var shop = await ShopFacade.GetAsync(id.GetValueOrDefault());
            var list = new List<ItemDto>(shop.Items.Where(i=>i.RequiredLevel>=lowerBound && i.RequiredLevel <=upperBound).ToList());
            
            ViewBag.Count = list.Count;
            ViewBag.Current = page;

            shop.Items = list.OrderByDescending(a=>a.Quality).ThenByDescending(a=>a.RequiredLevel).Skip((page - 1) * 12).Take(12).ToList();
            var model = new ShopCharacterModel() {Character = u.Character, Shop = shop};
            return View(model);
        }

        public async Task<ActionResult> BuyItem(Guid? id, Guid? charId, Guid? shopId)
        {
            if (id == null || charId == null || shopId == null)
            {
                return RedirectToAction("Index");
            }
            var item = await EquipmentFacade.GetItemAsync(id.GetValueOrDefault());
            var character = await CharacterFacade.GetAsync(charId.GetValueOrDefault());
            if (await EquipmentFacade.AddToInventoryAsync(character.Inventory, item))
            {
                var updatedUser = new CharacterUpdateDto()
                {
                    Name = character.Name,
                    CurrentHp = character.CurrentHp,
                    Experience = character.Experience,
                    Gold = character.Gold- (int)(item.Price*1.5),
                    Id = character.Id,
                    SkillPoints = character.SkillPoints,
                    Sickness = character.Sickness
                };
                await CharacterFacade.EditAsync(updatedUser);
            }

            return RedirectToAction("Detail", new {id = shopId});
        }


    }
}