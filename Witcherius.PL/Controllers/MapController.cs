using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using Witcherius.BL.DataTransferObjects.Item;
using Witcherius.BL.DataTransferObjects.Location;
using Witcherius.BL.DataTransferObjects.MiniLocation;
using Witcherius.BL.DataTransferObjects.Monster;
using Witcherius.BL.DataTransferObjects.Quest;
using Witcherius.BL.Facades;

namespace Witcherius.PL.Controllers
{
    [CustomAuthorize(Roles = "User")]
    public class MapController : Controller
    {

        public LocationFacade LocationFacade { get; set; }
        public MiniLocationFacade MiniLocationFacade { get; set; }
        public QuestFacade QuestFacade { get; set; }
        public UserFacade UserFacade { get; set; }
        public MonsterFacade MonsterFacade { get; set; }

        // GET: Map

        public async Task<ActionResult> Index()
        {
            var u = await UserFacade.GetUserAccordingToUsernameAsync(User.Identity.Name);
            if (u.Character == null)
            {
                return RedirectToAction("Create", "Character");
            }
            var model = await LocationFacade.GetAllAsync();
            return View(model);
        }

        public async Task<ActionResult> DetailLocation(Guid? id)
        {
            if (id == null || (await LocationFacade.GetLocationAsync(id.GetValueOrDefault())) == null)
            {
                return RedirectToAction("Index");
            }
            var model = await LocationFacade.GetLocationAsync(id.GetValueOrDefault());
            return View(model);
        }

        public async Task<ActionResult> Quest(Guid? id)
        {
            if (id == null || (await MiniLocationFacade.GetMiniLocationAsync(id.GetValueOrDefault()))==null)
            {
                return RedirectToAction("Index");
            }
            var inspect = new InspectMiniLocationDto
            {
                MiniLocationDto = await MiniLocationFacade.GetMiniLocationAsync(id.GetValueOrDefault())
            };
            inspect.QuestDto = inspect.MiniLocationDto.Quest;
            inspect.MonsterDto = inspect.QuestDto.Monster;
            var userDto = await UserFacade.GetUserAccordingToUsernameAsync(User.Identity.Name);
            inspect.CharacterDto = userDto.Character;
            return View(inspect);
        }
    }
}