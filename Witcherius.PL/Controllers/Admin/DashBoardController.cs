using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Witcherius.BL.Facades;
using Witcherius.PL.Controllers.Admin.Shared;

namespace Witcherius.PL.Controllers.Admin
{
    public class DashBoardController : AdminController
    {
        public EquipmentFacade EquipmentFacade { get; set; }
        public QuestFacade QuestFacade { get; set; }
        public MonsterFacade MonsterFacade { get; set; }
        public MiniLocationFacade MiniLocationFacade { get; set; }
        public LocationFacade LocationFacade { get; set; }


        // GET: DashBoard
        public async Task<ActionResult> Index()
        {
            var itemsList = (await EquipmentFacade.GetAllItemsAsync()).ToList();
            ViewBag.Items = itemsList.Count(a=>a.ShopId==null);
            ViewBag.ShopItems = itemsList.Count(a=>a.ShopId!=null);
            ViewBag.Monsters = (await MonsterFacade.GetAllMonstersAsync()).Count();
            ViewBag.Quests = (await QuestFacade.GetAllQuestsAsync()).Count();
            ViewBag.Mini = (await MiniLocationFacade.GetAllItemsAsync()).Count();
            ViewBag.Locations = (await LocationFacade.GetAllAsync()).Count();
            return View();
        }
    }
}