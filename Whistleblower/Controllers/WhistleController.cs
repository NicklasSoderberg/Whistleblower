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
                    DBHandler.Post(new DB.Whistle
                    {
                        LawyerID = 0,
                        About = whistleInput.About,
                        C_When = whistleInput.When,
                        C_Where = whistleInput.Where,
                        Description = whistleInput.Description,
                        Description_OtherEmployees = whistleInput.Description_OtherEmployees,
                        isActive = true,
                        UploadID = 2,
                        WhistleID = 0
                    });
                    return RedirectToAction("UserLogin", "Login");

                default:
                    break;
            }
            ViewBag.Message = "Kontrollera din information";
            WhistleModel WM = whistleInput;
            return View(WM);
        }
    }
}