using System.Threading.Tasks;
using System.Web.Mvc;
using Witcherius.BL.Facades;
using Witcherius.PL.Controllers.Admin.Shared;

namespace Witcherius.PL.Controllers.Admin
{
    public class UserAdminController : AdminController
    {
        public UserFacade UserFacade { get; set; }

        // GET: Admin/User
        public async Task<ActionResult> Index()
        {
            var model = await UserFacade.GetAllAsync();
            return View(model);
        }
    }
}