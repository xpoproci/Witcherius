using System.Web.Mvc;

namespace Witcherius.PL.Controllers.Admin.Shared
{
    [CustomAuthorize(Roles = "Admin")]
    public abstract class AdminController : Controller
    {
    }
}