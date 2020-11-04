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
using Ionic.Zip;

namespace Whistleblower.Controllers
{
    public class WhistleController : Controller
    {
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

        public ActionResult ReportStatus()
        {
            if (Session["UserName"] != null && Session["WhistleId"] != null)
            {
                var id = (int)Session["WhistleId"];
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
                return View("Whistle");
            }
        }

        public ActionResult WhistleConfirm(WhistleViewModel formData, string button, IEnumerable<HttpPostedFileBase> fileUpload)
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

        public ActionResult Safebox(int Id)
        {
            SafeboxViewmodel viewmodel = new SafeboxViewmodel(Id);
            using (var db = new DB.DBEntity())
            {
                if (db.Lawyer.FirstOrDefault(l => l.LawyerID == LawyerViewmodel.LoggedinID) != null)
                {
                    viewmodel.CurrentUser = "Lawyer";
                }
                else
                {
                    viewmodel.CurrentUser = "Whistler";
                }
            }
            viewmodel.WhistleId = Id;

            return View(viewmodel);
        }

        [HttpPost]
        public ActionResult SendMail(Mail mail, int id, HttpPostedFileBase fileUpload)
        {
            SafeboxViewmodel viewmodel = new SafeboxViewmodel(id);
            viewmodel.SendMail(mail, id, fileUpload, Session["LoggedInAsLawyer"].ToString());

            return RedirectToAction($"Safebox/{id}", "Whistle");
        }

      
    }
}