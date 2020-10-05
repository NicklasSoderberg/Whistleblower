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
    public class LoginController : Controller
    {
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