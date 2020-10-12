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
using DB;

namespace Whistleblower.Controllers
{
    public class LoginController : Controller
    {
        //public ActionResult LoginAdmin()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult LoginAdmin(LoginAdmin formAdmin)
        //{
        //    LoginAdmin loginadmin = new LoginAdmin();
        //    if (ModelState.IsValid)
        //    {
        //        if (loginadmin.Username == formAdmin.Username && loginadmin.Password == formAdmin.Password)
        //        {
        //            return RedirectToAction("Safebox");
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("LogOnError", "Användarnamn och/eller lösenord matchar inte");
        //        }
        //    }
        //    return View(formAdmin);
        //}
        public ActionResult LoginAdmin()
        {
            if (Session["UserID"] != null)
            {
                return RedirectToAction("Dashboard", "Admin");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoginAdmin(LoginAdmin objUser)
        {
            if (ModelState.IsValid)
            {
                using (var db = new DB.DBEntity())
                {
                    var obj = db.Admin.Where(a => a.Username.Equals(objUser.Username) && a.Password.Equals(objUser.Password)).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["UserID"] = obj.AdminID.ToString();
                        Session["UserName"] = obj.Username;
                        return RedirectToAction("Dashboard", "Admin");
                    }
                }
            }
            ModelState.AddModelError("LogOnError", "Användarnamn och/eller lösenord matchar inte");
            return View(objUser);
        }
        [HttpPost]
        public ActionResult Logout()
        {
            Session.Remove("UserID");
            return RedirectToAction("LoginAdmin");
        }
        public ActionResult UserLogin()
        {
            ViewBag.Message = "Login";
            return View();
        }
        [HttpPost]
        public ActionResult UserLogin(LoginModel formModel)
        {
            var users = DBHandler.GetUser();
            bool correctLogin = false;

            foreach (var s in users)
            {
                if (s.UniqueID == formModel.UserName && s.Password == formModel.Password)
                {
                    correctLogin = true;
                    TempData["whistleId"] = s.WhistleID;
                }
            }

            if (ModelState.IsValid)
            {                
                if (correctLogin)
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

        public ActionResult CheckLogIn()
        {
            return View();
        }

        public ActionResult ReportStatus()
        {
            var id = (int)TempData["whistleId"];
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
    }
}