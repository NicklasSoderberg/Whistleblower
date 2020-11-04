using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Whistleblower.Models;
using Whistleblower.ViewModels;
using Whistleblower.Custom;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Whistleblower.App_Start;
using System.Security.Claims;
using DB;
using System.ComponentModel.Design;

namespace Whistleblower.Controllers
{
    [Authorize]
    public class LoginController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public LoginController()
        {
        }

        public LoginController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }


        [HttpGet]
        public ActionResult Authorize()
        {
            var claims = new ClaimsPrincipal(User).Claims.ToArray();
            var identity = new ClaimsIdentity(claims, "Bearer");
            AuthenticationManager.SignIn(identity);
            return new EmptyResult();
        }





        public ActionResult LoginAdmin()
        {
            if (Session["UserID"] != null)
            {
                return RedirectToAction("Dashboard", "Admin");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoginAdmin(LoginAdmin objUser)
        {
            if (ModelState.IsValid)
            {
                using (var db = new DB.DBEntity())
                {
                    var obj = db.Admin.Where(a => a.Username.Equals(objUser.Username) && a.Password.Equals(objUser.Password)).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["UserID"] = obj.AdminID.ToString();
                        Session["UserName"] = obj.Username;
                        return RedirectToAction("Dashboard", "Admin");
                    }
                }
            }
            ModelState.AddModelError("LogOnError", "Användarnamn och/eller lösenord matchar inte");
            return View(objUser);
        }
        public ActionResult Logout()
        {
            Session.Remove("UserID");
            return RedirectToAction("LoginAdmin");
        }

        [AllowAnonymous]
        public ActionResult LogOutUser()
        {
            Session.Remove("UserID");
            Session.Remove("LoggedInAsLawyer");
            return RedirectToAction("UserLogin");
        }

        [AllowAnonymous]
        public ActionResult UserLogin()
        {
            if (Session["UserID"] != null)
            {
                return RedirectToAction("ReportStatus");
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> UserLogin(LoginModel formModel)
        {
            //if (ModelState.IsValid)
            //{
            //    using (var db = new DB.DBEntity())
            //    {
            //        var obj = db.User.Where(a => a.UniqueID.Equals(formModel.UserName)).FirstOrDefault();
            //        if (obj != null)
            //        {
            //            var hashCode = obj.VCode;

            //            var encodingPasswordString = Helper.EncodePassword(formModel.Password, hashCode);

            //            var query = db.User.Where(s => s.UniqueID.Equals(formModel.UserName) && s.Password.Equals(encodingPasswordString)).FirstOrDefault();
            //            if (query != null)
            //            {
            //                var whistleobj = db.Whistle.Where(w => w.WhistleID == obj.WhistleID).FirstOrDefault();
            //                if (whistleobj.isActive == true)
            //                {
            //                    Session["UserID"] = obj.ID.ToString();
            //                    Session["UserName"] = obj.UniqueID;
            //                    Session["WhistleId"] = obj.WhistleID;
            //                    Session["LoggedInAsLawyer"] = "0";
            //                    return RedirectToAction("ReportStatus");
            //                }
            //                else
            //                {
            //                    ModelState.AddModelError("LogOnError", "Ärendet är avslutat.");
            //                }
            //            }
            //        }
            //    }
            //}
            //ModelState.AddModelError("LogOnError", "ID eller lösenord är felaktigt");
            //return View(formModel);
            //bool active;
            //using (var db = new DB.DBEntity())
            //{
            //    var whistleobj = db.Whistle.Where(w => w.WhistleID == obj.WhistleID).FirstOrDefault();
            //                    if (whistleobj.isActive == true)
            //                    {
            //        bool = active;
            //                    }
            //}

            var result = await SignInManager.PasswordSignInAsync(formModel.UserName, formModel.Password, false, false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToAction("ReportStatus");
                case SignInStatus.LockedOut:
                    return Content("Lockout");
                case SignInStatus.RequiresVerification:
                    return Content("RequiresVerification");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("LogOnError", "ID eller lösenord är felaktigt");
                    return View(formModel);
            }
        }


        [Authorize(Roles = "User")]
        public ActionResult ReportStatus()
        {
            bool active = false;
            int id = 0;
            var user = UserManager.FindById(User.Identity.GetUserId());
            using (var db = new DB.DBEntity())
            {
                var whistleobj = db.Whistle.Where(w => w.WhistleID == user.WhistleId).FirstOrDefault();
                if (whistleobj.isActive == true)
                {
                    id = user.WhistleId;
                    active = true;
                }
            }

            if (active)
            {
                ReportStatusViewModel reportStatusViewModel = new ReportStatusViewModel();

                reportStatusViewModel.Whistle = DBHandler.GetWhistles(false).FirstOrDefault(x => x.WhistleID == id);
                var messages = DBHandler.GetMessages(id);

                if (messages.Count > 0)
                {
                    reportStatusViewModel.SafeBox = true;
                }
                else
                {
                    reportStatusViewModel.SafeBox = false;
                }
                return View(reportStatusViewModel);
            }
            else
            {
                ModelState.AddModelError("LogOnError", "Ärendet är avslutat.");
                return View("UserLogin");
            }
        }

        [HttpPost]
        public JsonResult CreateLawyerLogin(string username, string name)
        {
            var DBhandler = new DBHandler();
            var pass = DBHandler.CreateUser(username, name);

            return Json(pass, "application/json");
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("UserLogin", "Login");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}