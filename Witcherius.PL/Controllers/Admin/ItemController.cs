using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Web.Mvc;
using Witcherius.BL.DataTransferObjects.Attributes;
using Witcherius.BL.DataTransferObjects.Item;
using Witcherius.BL.Facades;
using Witcherius.PL.Controllers.Admin.Shared;
using Witcherius.PL.Models.Admin;

namespace Witcherius.PL.Controllers.Admin
{
    public class ItemController : AdminController
    {
        public EquipmentFacade EquipmentFacade { get; set; }
        public AttributesFacade AttributesFacade { get; set; }
        public QuestFacade QuestFacade { get; set; }

        // GET: Admin/Item
        public async Task<ActionResult> Index(int page = 1)
        {
            var list = (await EquipmentFacade.GetAllItemsAsync()).Where(a=>a.ShopId==null).ToList();
            var toShow = list.Skip((page-1)*10).Take(10);
            ViewBag.Count = list.Count;
            ViewBag.Current = page;
            return View("~/Views/Admin/Item/Index.cshtml", toShow);
        }

        public ActionResult Create()
        {
            var images = Directory.EnumerateFiles(HostingEnvironment.MapPath("~/Content/images/items/"))
                .Select(fn => new {Path = Path.GetFileName(fn)});
            ViewBag.Images = images;
            return View("~/Views/Admin/Item/Create.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ItemDto model)
        {
            if (!ModelState.IsValid) return RedirectToAction("Create");
            var attr = model.Attributes;
            model.Attributes = null;

            await EquipmentFacade.CreateItem(model, attr);

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Delete(Guid id)
        {
            var item = await EquipmentFacade.GetItemAsync(id);                
            await EquipmentFacade.DeleteItemAsync(item.Id);
            if (item.Attributes != null)
            {
                var attr = await AttributesFacade.GetAttributesAsync(item.Attributes.Id);
                await AttributesFacade.DeleteAttributesAsync(attr.Id);
            }

            return RedirectToAction("Index", "DashBoard");
        }
    }
}