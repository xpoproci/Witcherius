using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Threading.Tasks;
using System.Web.Mvc;
using Witcherius.BL.Facades;
using Witcherius.PL.Models;

namespace Witcherius.PL.Controllers
{
    
    public class ComponentsController : Controller
    {
        public UserFacade UserFacade { get; set; }

        [ChildActionOnly]
        public ActionResult UserProfileHeader(string name)
        {
            var user = Task.Run(()=> UserFacade.GetUserAccordingToUsernameAsync(name));
            user.Wait();
            var userWithChar = Task.Run(() => UserFacade.GetAsync(user.Result.Id));
            userWithChar.Wait();

            var model = new UserProfileHeaderModel(userWithChar.Result);
            return PartialView("_UserProfileHeader", model);
        }

        [ChildActionOnly]
        public ActionResult MenuHeader()
        {
            var menu = new List<MenuItemModel>();
            menu.Add(new MenuItemModel() {Action = "Index", Controller = "Home", Text = "Home"});
            if (!User.Identity.IsAuthenticated)
            {
                menu.Add(new MenuItemModel() {Action = "Login", Controller = "Account", Text = "Login"});
                menu.Add(new MenuItemModel() {Action = "Register", Controller = "Account", Text = "Register"});
            }
            else
            {
                menu.Add(new MenuItemModel() { Action = "Index", Controller = "Map", Text = "Map" });
                menu.Add(new MenuItemModel() { Action = "Index", Controller = "Shop", Text = "Shop" });
                menu.Add(new MenuItemModel() { Action = "Index", Controller = "User", Text = "Hall of Fame" });
                if (User.IsInRole("Admin"))
                {
                    menu.Add(new MenuItemModel() { Action = "Index", Controller = "Dashboard", Text = "Admin" });
                }
            }                

            return PartialView("_MenuHeader", menu);
        }

        [ChildActionOnly]
        public ActionResult Pagination(int count, int current)
        {
            var model = new PaginationModel(current, count);
            return PartialView("_pagination", model);
        }
    }
}