using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Witcherius.BL.DataTransferObjects.User;
using Witcherius.BL.Facades;
using Witcherius.PL.Models;

namespace Witcherius.PL.Controllers
{
    public class AccountController : Controller
    {
        public UserFacade UserFacade { get; set; }

        // GET: Account
        public ActionResult Index()
        {
            return RedirectToAction("Login");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(UserCreateDto model)
        {
            try
            {
                model.Roles = "User";
                await UserFacade.RegisterUser(model);
                //FormsAuthentication.SetAuthCookie(userCreateDto.Username, false);

                var authTicket = new FormsAuthenticationTicket(1, model.Username, DateTime.Now,
                    DateTime.Now.AddMinutes(30), false, model.Roles);
                var encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                HttpContext.Response.Cookies.Add(authCookie);

                return RedirectToAction("Index", "Character");
            }
            catch (ArgumentException)
            {
                ModelState.AddModelError("Username", "Account with that username already exists!");
                return View();
            }
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model, string returnUrl)
        {
            {
                var result = await UserFacade.Login(model.Username, model.Password);
                if (result.Item1)
                {
                    var authTicket = new FormsAuthenticationTicket(1, model.Username, DateTime.Now,
                        DateTime.Now.AddMinutes(30), false, result.Item2);
                    var encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                    var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                    HttpContext.Response.Cookies.Add(authCookie);

                    var decodedUrl = "";
                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        decodedUrl = Server.UrlDecode(returnUrl);
                    }

                    if (Url.IsLocalUrl(decodedUrl))
                    {
                        return Redirect(decodedUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Wrong username or password!");
                return View();
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}