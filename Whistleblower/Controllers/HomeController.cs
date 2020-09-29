using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Whistleblower.Models;

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

            return View();
        }
        public ActionResult popuptemp()
        {

            return View();
        }
    }
}