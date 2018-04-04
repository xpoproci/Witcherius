using System;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using Castle.Windsor;
using Witcherius.BL.Config;

namespace Witcherius.PL
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private readonly IWindsorContainer _container = new WindsorContainer();

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            BootstrapContainer();
        }

        private void BootstrapContainer()
        {
            // configure DI            
            _container.Install(new BlInstaller());
            _container.Install(new ControllersConfig());

            // set controller factory
            var controllerFactory = new WinsdorConfig(_container.Kernel);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
        }

        protected void Application_PostAuthenticateRequest(object sender, EventArgs e)
        {
            var authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];

            if (authCookie == null) return;
            var authTicket = FormsAuthentication.Decrypt(authCookie.Value);
            if (authTicket == null || authTicket.Expired) return;
            var roles = authTicket.UserData.Split(',');
            HttpContext.Current.User = new GenericPrincipal(new FormsIdentity(authTicket), roles);
        }
    }
}
