using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using Witcherius.BL.DataTransferObjects.MiniLocation;
using Witcherius.BL.DataTransferObjects.Quest;
using Witcherius.BL.Facades;
using Witcherius.PL.Controllers.Admin.Shared;
using Witcherius.PL.Models.Admin;

namespace Witcherius.PL.Controllers.Admin
{
    public class MiniLocationController : AdminController
    {
        public MiniLocationFacade MiniLocationFacade { get; set; }
        public QuestFacade QuestFacade { get; set; }

        public async Task<ActionResult> Index(int page = 1)
        {
            var list = (await MiniLocationFacade.GetAllItemsAsync()).ToList();
            var toShow = list.Skip((page - 1) * 10).Take(10);
            ViewBag.Count = list.Count;
            ViewBag.Current = page;
            return View("~/Views/Admin/MiniLocation/Index.cshtml", toShow);
        }

        public async Task<ActionResult> Create()
        {
            var images = Directory.EnumerateFiles(HostingEnvironment.MapPath("~/Content/images/locations/"))
                .Where(fn => (Path.GetFileName(fn)).StartsWith("mini_"))
                .Select(fn => new { Path = Path.GetFileName(fn) });
            ViewBag.Images = images;
            ViewBag.Quests = await QuestFacade.GetAllQuestsAsync();
            return View("~/Views/Admin/MiniLocation/Create.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(MiniLocationDto model)
        {
            var q = new QuestDto() {Id=model.QuestId.GetValueOrDefault()};
            model.QuestId = null;

            model.Id = await MiniLocationFacade.CreateMiniLocation(model);

            await MiniLocationFacade.AssignAsync(model, q);

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Delete(Guid id)
        {
            await MiniLocationFacade.DeleteMiniLocationAsync(id);
            return RedirectToAction("Index");
        }
    }
}