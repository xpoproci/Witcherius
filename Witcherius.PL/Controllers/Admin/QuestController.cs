using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Witcherius.BL.DataTransferObjects.Attributes;
using Witcherius.BL.DataTransferObjects.Item;
using Witcherius.BL.DataTransferObjects.Monster;
using Witcherius.BL.DataTransferObjects.Quest;
using Witcherius.BL.Facades;
using Witcherius.PL.Controllers.Admin.Shared;
using Witcherius.PL.Models.Admin;

namespace Witcherius.PL.Controllers.Admin
{
    public class QuestController : AdminController
    {
        public QuestFacade QuestFacade { get; set; }
        public MonsterFacade MonsterFacade { get; set; }
        public EquipmentFacade EquipmentFacade { get; set; }

        // GET: Admin/Monster
        public async Task<ActionResult> Index(int page = 1)
        {
            var list = (await QuestFacade.GetAllQuestsAsync()).ToList();
            var toShow = list.Skip((page - 1) * 10).Take(10);
            ViewBag.Count = list.Count;
            ViewBag.Current = page;
            return View("~/Views/Admin/Quest/Index.cshtml", toShow);
        }

        public async  Task<ActionResult> Create()
        {
            ViewBag.Items = await EquipmentFacade.GetAllItemsAsync();
            ViewBag.Monsters = await MonsterFacade.GetAllMonstersAsync();

            return View("~/Views/Admin/Quest/Create.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(QuestDto model)
        {
            var monster = new MonsterDto() {Id = model.MonsterId.GetValueOrDefault()};
            var item = new ItemDto() {Id = model.ItemId.GetValueOrDefault()};
            model.ItemId = null;
            model.MonsterId = null;

            model.Id = await QuestFacade.CreateQuest(new QuestDto() {Experience = model.Experience, Gold = model.Gold});
            await QuestFacade.AssignAsync(model, monster, item);

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Delete(Guid id)
        {
            await QuestFacade.DeleteQuestAsync(id);
            return RedirectToAction("Index");
        }
    }
}