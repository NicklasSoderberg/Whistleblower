using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
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
            ViewBag.Message = "Your contact page.";
            WhistleModel WM = new WhistleModel();
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

        public ActionResult TempUserLogin()
        {
            ViewBag.Message = "Login";
            return View();
        }

        public ActionResult MailSent()
        {
            return View();
        }


    }
}