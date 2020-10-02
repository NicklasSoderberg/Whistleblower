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
            ViewBag.Message = "Fyll i formulï¿½ret";
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
            ViewBag.Message = "Fyll i formulï¿½ret";
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
                    /*Bygg vidare hï¿½rifrï¿½n nï¿½r vi har DB*/
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
        [HttpPost]
        public ActionResult LoginAdmin(LoginAdmin formAdmin)
        {
            LoginAdmin loginadmin = new LoginAdmin();
            if (ModelState.IsValid)
            {
                if (loginadmin.Username == formAdmin.Username && loginadmin.Password == formAdmin.Password)
                {
                    return RedirectToAction("Safebox");
                }
                else
                {
                    ModelState.AddModelError("LogOnError", "Användarnamn och/eller lösenord matchar inte");
                }
            }
            return View(formAdmin);
        }
        public ActionResult LoginLawyer()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginLawyer(LoginLawyer formLawyer)
        {
            LoginLawyer loginlawyer = new LoginLawyer();
            if (ModelState.IsValid)
            {
                if (loginlawyer.Username == formLawyer.Username && loginlawyer.Password == formLawyer.Password)
                {
                    return RedirectToAction("Safebox");
                }
                else
                {
                    ModelState.AddModelError("LogOnError", "Användarnamn och/eller lösenord matchar inte");
                }
            }
            return View(formLawyer);
        }


        public ActionResult Safebox()
        {
            SafeboxViewmodel viewmodel = new SafeboxViewmodel();
           viewmodel.MailList = new List<Mail>()
            {
                new Mail{ReportId=1, MailId= 1,SentBool=false,LawyerId=1,Message="Hello my friend what happened?"},
                new Mail{ReportId=1, MailId= 3,SentBool=true,LawyerId=1,Message="Very good friend."}
            };
            if(viewmodel.MailList.Count != 0) 
            {
                return View(viewmodel);
            }
            else
            {
                return RedirectToAction("");
            }
            
        }
        [HttpPost]
        public ActionResult SendMail(Mail mail)
        {

            SafeboxViewmodel viewmodel = new SafeboxViewmodel();
            mail.SentBool = true;
            //lï¿½gg till mailet i databasen
            return RedirectToAction("Safebox");
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
                    ModelState.AddModelError("LogOnError", "ID eller lï¿½senord ï¿½r felaktigt");                  
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
    }
}