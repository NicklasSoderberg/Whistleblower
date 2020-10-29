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
using Whistleblower.Encryption;
using System.Security.Cryptography;

namespace Whistleblower.Controllers
{
    public class LoginController : Controller
    {
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
        public ActionResult Logout()
        {
            Session.Remove("UserID");
            return RedirectToAction("LoginAdmin");
        }


        public ActionResult LogOutUser()
        {
            Session.Remove("UserID");
            Session.Remove("LoggedInAsLawyer");
            return RedirectToAction("UserLogin");
        }

        public ActionResult UserLogin()
        {
            if (Session["UserID"] != null)
            {
                return RedirectToAction("ReportStatus");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserLogin(LoginModel formModel)
        {
            if (ModelState.IsValid)
            {
                using (var db = new DB.DBEntity())
                {
                    var obj = db.User.Where(a => a.UniqueID.Equals(formModel.UserName)).FirstOrDefault();
                    if (obj != null)
                    {
                        var hashCode = obj.VCode;

                        var encodingPasswordString = Helper.EncodePassword(formModel.Password, hashCode);

                        var query = db.User.Where(s => s.UniqueID.Equals(formModel.UserName) && s.Password.Equals(encodingPasswordString)).FirstOrDefault();
                        if (query != null)
                        {
                            var whistleobj = db.Whistle.Where(w => w.WhistleID == obj.WhistleID).FirstOrDefault();
                            if (whistleobj.isActive == true)
                            {
                                Session["UserID"] = obj.ID.ToString();
                                Session["UserName"] = obj.UniqueID;
                                Session["WhistleId"] = obj.WhistleID;
                                Session["LoggedInAsLawyer"] = "0";
                                return RedirectToAction("ReportStatus");
                            }
                            else
                            {
                                ModelState.AddModelError("LogOnError", "Ärendet är avslutat.");
                            }
                        }
                    }
                }
            }
            ModelState.AddModelError("LogOnError", "ID eller lösenord är felaktigt");
            return View(formModel);
        }

        public ActionResult ReportStatus()
        {
            if (Session["UserName"] != null && Session["WhistleId"] != null)
            {
                var id = (int)Session["WhistleId"];
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
            else
            {
                return View("UserLogin");
            }
        }

        [HttpPost]
        public JsonResult CreateLawyerLogin(string username, string name)
        {
            var DBhandler = new DBHandler();
            var pass = DBHandler.CreateUser(username, name);

            return Json(pass, "application/json");
        }
    }
}