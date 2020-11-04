using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Windows;
using System.Web.Services.Description;
using System.Windows.Input;
using Whistleblower.Models;
using Whistleblower.ViewModels;
using System.Web.Services;
using Whistleblower.Custom;
using System.Xml.XPath;
using Whistleblower.Encryption;
using System.Security.Cryptography;
using System.Drawing;
using Whistleblower.App_Start;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using System.Web.Security;

namespace Whistleblower.Controllers
{
    public class WhistleController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public WhistleController()
        {
        }

        public WhistleController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
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



        public ActionResult Index()
        {
            ViewBag.Message = "Whistleblower";
            return View();
        }

        public ActionResult Whistle()
        {
            ViewBag.Message = "Fyll i formulï¿½ret";
            WhistleViewModel WM = new WhistleViewModel();
            if (TempData["Form"] != null)
            {
                WM = (WhistleViewModel)TempData["Form"];
            }
            return View(WM);
        }

        public async Task<ActionResult> WhistleConfirm(WhistleViewModel formData, string button, IEnumerable<HttpPostedFileBase> fileUpload)
        {
            switch (button?.ToLower())
            {
                case "tillbaka":
                    TempData["Form"] = formData;
                    return RedirectToAction("Whistle", "Whistle");

                case "skicka":
                    WhistleViewModel UWM = (WhistleViewModel)TempData["Form"];
                    UWM.FileUpload = fileUpload;

                    var result = DBHandler.PostWhistle(new DB.Whistle
                    {
                        LawyerID = 0,
                        About = UWM.Whistle.About,
                        C_When = UWM.Whistle.When,
                        C_Where = UWM.Whistle.Where,
                        Description = UWM.Whistle.Description,
                        Description_OtherEmployees = UWM.Whistle.Description_OtherEmployees,
                        isActive = true,
                        CurrentStatus = "Aktiv",
                        DateCreated = DateTime.Now,
                        UploadID = 0
                    });

                    foreach (HttpPostedFileBase f in UWM.FileUpload)
                    {
                        if (f == null)
                            break;
                        DBHandler.PostFile(new DB.File
                        {
                            Base64 = Base64Handler.FileToBase64(f),
                            Extension = f.ContentType,
                            WhistleID = result.WhistleID
                        });
                    }

                    DBHandler.CreateConversation(
                        new DB.Conversation
                        {
                            WhistleID = result.WhistleID
                        });

                    var uniqueid = AutoGenerateID(false);
                    var password = AutoGenerateID(true);

                    var user = new ApplicationUser { UserName = uniqueid, WhistleId = result.WhistleID};
                    var result1 = await UserManager.CreateAsync(user, password);
                    var roleresult = UserManager.AddToRole(user.Id, "User");


                    UWM.Whistle.user = DBHandler.PostUser(new DB.User
                    {
                        UniqueID = uniqueid,
                        Password = password,
                        WhistleID = result.WhistleID
                    });
                    UWM.Whistle.user.Password = password;
                    TempData["Form"] = null;
                    return View(UWM);

                default:
                    TempData["Form"] = formData;
                    break;
            }
            ViewBag.Message = "Kontrollera din information";
            return View(formData);
        }
        public string AutoGenerateID(bool isPassword)
        {
            Random r = new Random();
            string generatedID = "";
            for (int i = 0; i < 9; i++)
            {
                if (i == 3 || i == 4 || i == 8)
                {
                    char c = Convert.ToChar(r.Next(65, 90));
                    generatedID += c;
                }
                else
                {
                    generatedID += r.Next(0, 9).ToString();
                }
            }

            if (!isPassword)
            {
                using (var db = new DB.DBEntity())
                {
                    List<DB.User> test = db.User.Where(X => X.UniqueID == generatedID).ToList();
                    if (test.Count() != 0)
                    {
                        AutoGenerateID(false);
                    }
                }
            }
            return generatedID;
        }
    }
}