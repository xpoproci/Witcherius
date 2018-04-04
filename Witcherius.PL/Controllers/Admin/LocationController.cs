using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using Witcherius.BL.DataTransferObjects.Location;
using Witcherius.BL.DataTransferObjects.MiniLocation;
using Witcherius.BL.DataTransferObjects.Quest;
using Witcherius.BL.Facades;
using Witcherius.PL.Models.Admin;

namespace Witcherius.PL.Controllers.Admin
{
    public class LocationController : Controller
    {
        public LocationFacade LocationFacade { get; set; }
        public MiniLocationFacade MiniLocationFacade { get; set; }

        public async Task<ActionResult> Index(int page = 1)
        {
            var list = (await LocationFacade.GetAllAsync()).ToList();
            var toShow = list.Skip((page - 1) * 10).Take(10);
            ViewBag.Count = list.Count;
            ViewBag.Current = page;
            return View("~/Views/Admin/Location/Index.cshtml", toShow);
        }

        public ActionResult Create()
        {
            var images = Directory.EnumerateFiles(HostingEnvironment.MapPath("~/Content/images/locations/"))
                .Where(fn=> (Path.GetFileName(fn)).StartsWith("location_"))
                .Select(fn => new { Path = Path.GetFileName(fn) });
            ViewBag.Images = images;
            return View("~/Views/Admin/Location/Create.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(LocationDto model)
        {
            model.Id = await LocationFacade.CreateLocation(model);
            return RedirectToAction("Edit", new {id =  model.Id});
        }

        public async Task<ActionResult> Edit(Guid id)
        {
            ViewBag.MiniLocations = (await MiniLocationFacade.GetAllItemsAsync()).Where(a=>a.LocationId==null).ToList();
            var model = new LocationEditModel() {Location = id};
            return View("~/Views/Admin/Location/Edit.cshtml", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(LocationEditModel model)
        {
            await LocationFacade.AssignAsync(new LocationDto() {Id=model.Location}, new MiniLocationDto() {Id=model.MiniLoaction});
            return RedirectToAction("Edit", model.Location);
        }

        public async Task<ActionResult> Delete(Guid id)
        {
            await LocationFacade.DeleteLocationAsync(id);
            return RedirectToAction("Index");
        }
    }
}
