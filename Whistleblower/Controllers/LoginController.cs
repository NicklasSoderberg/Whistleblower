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
            
            reportStatusViewModel.Whistle = DBHandler.GetWhistles().FirstOrDefault(x => x.WhistleID == id);
            
            reportStatusViewModel.Conversation = DBHandler.GetConversation().FirstOrDefault(x => x.WhistleID == id);
            if (reportStatusViewModel.Conversation == null)
            {
                reportStatusViewModel.Conversation = new Conversation()
                {
                    ConversationID = 0,
                    WhistleID = 0
                };
            }
            return View(reportStatusViewModel);
        }
    }
}