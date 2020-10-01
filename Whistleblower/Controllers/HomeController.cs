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
            ViewBag.Message = "Fyll i formul�ret";
            WhistleModel WM = new WhistleModel();
            return View(WM);
        }
        
        public ActionResult WhistleBack(WhistleModel formData)
        {
            ViewBag.Message = "Fyll i formul�ret";
            return View("Whistle", formData);
        }

        [HttpPost]
        public ActionResult Whistle(WhistleModel formData)
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
                    /*Bygg vidare h�rifr�n n�r vi har DB*/
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
            //l�gg till mailet i databasen
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
                    ModelState.AddModelError("LogOnError", "ID eller l�senord �r felaktigt");                  
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