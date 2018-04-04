using System.Web.Mvc;
using System.Web.Routing;

namespace Witcherius.PL
{
    public class CustomAuthorize : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = !filterContext.HttpContext.User.Identity.IsAuthenticated ? 
                new RedirectToRouteResult(new RouteValueDictionary(new { area="", controller = "Account", action = "Login" })) : 
                new RedirectToRouteResult(new RouteValueDictionary(new { area = "", controller = "Home", action = "Index" }));
        }
    }
}