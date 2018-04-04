using System.Web.Mvc;

namespace Witcherius.PL.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return !User.Identity.IsAuthenticated ? RedirectToAction("Register", "Account") : RedirectToAction("Detail", "Character");
        }
    }
}