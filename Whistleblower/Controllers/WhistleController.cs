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
            WhistleModel WM = new WhistleModel();
            return View(WM);
        }

        [HttpPost]
        public ActionResult Whistle(WhistleModel formData)
        {
            return RedirectToAction("WhistleConfirm", "Whistle", formData);
        }

        public ActionResult WhistleBack(WhistleModel formData)
        {
            ViewBag.Message = "Fyll i formulï¿½ret";
            return View("Whistle", formData);
        }

        [HttpPost]
        public ActionResult WhistleBack(WhistleModel formData, string button)
        {
            return RedirectToAction("WhistleConfirm", "Whistle", formData);
        }

        public ActionResult WhistleConfirm(WhistleModel whistleInput, string button)
        {
            switch (button?.ToLower())
            {
                case "tillbaka":
                    return RedirectToAction("WhistleBack", "Whistle", whistleInput);

                case "skicka":
                    WhistleModel UWM = whistleInput;
                    var result = DBHandler.PostWhistle(new DB.Whistle
                    {
                        LawyerID = 0,
                        About = whistleInput.About,
                        C_When = whistleInput.When,
                        C_Where = whistleInput.Where,
                        Description = whistleInput.Description,
                        Description_OtherEmployees = whistleInput.Description_OtherEmployees,
                        isActive = true,
                        CurrentStatus = "Aktiv",
                        DateCreated = DateTime.Now,
                        UploadID = 2
                    });

                    DBHandler.CreateConversation(
                        new DB.Conversation
                        {
                            WhistleID = result.WhistleID

                        });
                    UWM.user = DBHandler.PostUser(new DB.User
                    {                        
                        UniqueID = AutoGenerateID(false),
                        Password = AutoGenerateID(true),
                        WhistleID = result.WhistleID
                    });
                    
                    return View(UWM);

                default:
                    break;
            }
            ViewBag.Message = "Kontrollera din information";
            WhistleModel WM = whistleInput;
            return View(WM);
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
                    var test = db.User.Where(X => X.UniqueID == generatedID);
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