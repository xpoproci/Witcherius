using System.Web.Mvc;
using System.Web.Routing;

namespace Witcherius.PL
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}/{slug}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional, slug="" }
            );

            routes.MapRoute(
                name: "Admin",
                url: "Admin/{controller}/{action}/{id}/{slug}"
            );
        }
    }
}

