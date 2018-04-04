using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Witcherius.BL.DataTransferObjects.Item;
using Witcherius.BL.DataTransferObjects.Shop;
using Witcherius.BL.Facades;
using Witcherius.PL.Controllers.Admin.Shared;
using Witcherius.PL.Models.Admin;

namespace Witcherius.PL.Controllers.Admin
{
    public class StoreController : AdminController
    {
        public ShopFacade ShopFacade { get; set; }

        // GET: Store
        public async Task<ActionResult> Index()
        {
            var list = await ShopFacade.GetAllAsync();
            return View(list);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ShopDto model)
        {
            model.Id = await ShopFacade.CreateAsync(model);
            return RedirectToAction("Edit", new { id = model.Id });
        }

        public async Task<ActionResult> Edit(Guid id, int page=1)
        {
            var shop = await ShopFacade.GetAsync(id);
            var list = shop.Items.ToList();
            var toShow = list.Skip((page - 1) * 10).Take(10);
            ViewBag.Count = list.Count;
            ViewBag.Current = page;
            var model = new ShopEditModel() {Id=id, Items = new List<ItemDto>(toShow)};
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ShopEditModel model)
        {
            var shop = await ShopFacade.GetAsync(model.Id);
            await ShopFacade.GenerateItemsAsync(shop, model.Minimum, model.Maximum);
            return RedirectToAction("Index");
        }
    }
}