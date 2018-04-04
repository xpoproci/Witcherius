using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Web.Mvc;
using Witcherius.BL.DataTransferObjects.Attributes;
using Witcherius.BL.DataTransferObjects.Monster;
using Witcherius.BL.Facades;
using Witcherius.PL.Controllers.Admin.Shared;
using Witcherius.PL.Models.Admin;

namespace Witcherius.PL.Controllers.Admin
{
    public class MonsterController : AdminController
    {
        public MonsterFacade MonsterFacade { get; set; }

        // GET: Admin/Monster
        public async Task<ActionResult> Index(int page = 1)
        {
            var list = (await MonsterFacade.GetAllMonstersAsync()).ToList();
            var toShow = list.Skip((page - 1) * 10).Take(10);
            ViewBag.Count = list.Count;
            ViewBag.Current = page;
            return View("~/Views/Admin/Monster/Index.cshtml", toShow);
        }

        public ActionResult Create()
        {
            var images = Directory.EnumerateFiles(HostingEnvironment.MapPath("~/Content/images/monsters/"))
                .Select(fn => new { Path = Path.GetFileName(fn) });
            ViewBag.Images = images;
            return View("~/Views/Admin/Monster/Create.cshtml");
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(MonsterDto model)
        {
            var attr = model.Attributes;
            model.Attributes = null;
            await MonsterFacade.CreateMonster(model, attr);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Delete(Guid id)
        {
            await MonsterFacade.DeleteMonsetrAsync(id);
            return RedirectToAction("Index");
        }
    }
}