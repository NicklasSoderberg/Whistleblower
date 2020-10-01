using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using System.Windows.Input;
using Whistleblower.Models;
using Whistleblower.ViewModels;

namespace Whistleblower.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        public ActionResult Whistle()
        {
            ViewBag.Message = "Fyll i formuläret";
            WhistleModel WM = new WhistleModel();
            return View(WM);
        }

        [HttpPost]
        public ActionResult Whistle(WhistleModel formData)
        {
            return RedirectToAction("WhistleConfirm", "Home", formData);
        }

        public ActionResult WhistleBack(WhistleModel formData)
        {
            ViewBag.Message = "Fyll i formuläret";
            return View("Whistle", formData);
        }

        [HttpPost]
        public ActionResult WhistleBack(WhistleModel formData, string button)
        {
            return RedirectToAction("WhistleConfirm", "Home", formData);
        }

        public ActionResult WhistleConfirm(WhistleModel whistleInput, string button)
        {
            switch (button?.ToLower())
            {
                case "tillbaka":
                    return RedirectToAction("WhistleBack", "Home", whistleInput);

                case "skicka":
                    /*Bygg vidare härifrån när vi har DB*/
                    break;

                default:
                    break;
            }
            ViewBag.Message = "Kontrollera din information";
            WhistleModel WM = whistleInput;
            return View(WM);
        }

        public ActionResult LoginAdmin()
        {
            return View();
        }
        public ActionResult LoginLawyer()
        {
            return View();
        }

        public ActionResult Safebox()
        {
            SafeboxViewmodel viewmodel = new SafeboxViewmodel();
            return View(viewmodel);
        }
        public void SelectMail(int MailId)
        {
            SafeboxViewmodel viewmodel = new SafeboxViewmodel();
            Mail SelectedMail = viewmodel.MailList.FirstOrDefault(m => m.MailId == MailId);
            SafeboxViewmodel.SelectedMail = SelectedMail;
        }

        public ActionResult UserLogin()
        {
            ViewBag.Message = "Login";
            return View();
        }
        [HttpPost]
        public ActionResult UserLogin(LoginModel formModel)
        {
            LoginModel loginModel = new LoginModel();
            if (ModelState.IsValid)
            {
                if (loginModel.UserName == formModel.UserName && loginModel.Password == formModel.Password)
                {
                    return RedirectToAction("ReportStatus");
                }
                else
                {
                    ModelState.AddModelError("LogOnError", "ID eller lösenord är felaktigt");                  
                }
            }
            return View(formModel);
        }

        public ActionResult TempUserLogin()
        {
            ViewBag.Message = "Login";
            return View();
        }

        public ActionResult MailSent()
        {
            return View();
        }

        public ActionResult CheckLogIn()
        {
            return View();
        }

        public ActionResult ReportStatus()
        {
            return View();
        }

        
        public ActionResult LogOut()
        {
            return RedirectToAction("Whistle");
        }
    }
}