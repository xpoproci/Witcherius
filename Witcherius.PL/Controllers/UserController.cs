using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Witcherius.BL.DataTransferObjects.Messages;
using Witcherius.BL.DataTransferObjects.User;
using Witcherius.BL.Facades;

namespace Witcherius.PL.Controllers
{
    [CustomAuthorize(Roles = "User")]
    public class UserController : Controller
    {
        public UserFacade UserFacade { get; set; }
        public CharacterFacade CharacterFacade { get; set; }
        public AttributesFacade AttributesFacade { get; set; }
        public EquipmentFacade EquipmentFacade { get; set; }

        // GET: Home
        public async Task<ActionResult> Index()
        {
            var u = await UserFacade.GetUserAccordingToUsernameAsync(User.Identity.Name);
            if (u.Character == null)
            {
                return RedirectToAction("Create", "Character");
            }
            var list = await UserFacade.GetAllAsync();
            var toShow = list.Where(a => a.Character != null && a.Score>=u.Score).OrderByDescending(a=>a.Score).Take(9).ToList();
            if (!toShow.Contains(u))
            {
                toShow.Add(u);
            }
            return View(toShow);
        }

        public async Task<ActionResult> Messages(int page = 1)
        {
            var u = await UserFacade.GetUserAccordingToUsernameAsync(User.Identity.Name);
            if (u.Character == null)
            {
                return RedirectToAction("Create", "Character");
            }
            var list = u.Messages.OrderByDescending(a => a.Arrived)
                .ThenBy(a => a.IsRead)
                .Skip((page - 1) * 10)
                .Take(10)
                .ToList();

            foreach (var item in list)
            {
                if (item.MessageText.Length > 60)
                {
                    item.MessageText = item.MessageText.Substring(0, 60) + "...";
                }
            }

            var toShow = list.Skip((page - 1) * 10).Take(10);
            ViewBag.Count = list.Count;
            ViewBag.Current = page;

            return View(toShow);
        }

        public async Task<ActionResult> MessageDetail(Guid? id)
        {
            if (id == null)
            {
                return RedirectToAction("Messages");
            }

            var model = await UserFacade.MessageGetAsync(id.GetValueOrDefault());
            if (!model.IsRead)
            {
                var updated = new MessageEditDto() {Id = model.Id, IsRead = true};
                await UserFacade.MessageEditAsync(updated);
            }
            return View(model);
        }

        public async Task<ActionResult> Detail(Guid? id)
        {
            if((id==Guid.Empty || id == null))
            {
                var u = await UserFacade.GetUserAccordingToUsernameAsync(User.Identity.Name);
                return View(await UserFacade.GetAsync(u.Id));
            }
            var nonNullId = id.GetValueOrDefault(); 
            var model = await UserFacade.GetAsync(nonNullId);
            return View(model);
        }

        public async Task<ActionResult> BuyCredit(Guid id)
        {
            var u = await UserFacade.GetUserAccordingToUsernameAsync(User.Identity.Name);
            var update = new UserEditDto() {Credits = u.Credits+20, CharacterId = u.CharacterId, Id = u.Id, Score = u.Score};
            await UserFacade.EditAsync(update);
            return RedirectToAction("Detail", new {id = u.Id});
        }

    }
}