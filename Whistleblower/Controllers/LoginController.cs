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
        public ActionResult Admin()
        {
            if (Session["UserID"] != null)
            {
                return RedirectToAction("Dashboard", "Admin");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Admin(LoginAdmin objUser)
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
                        Session["LoggedInAsLawyer"] = "2";
                        return RedirectToAction("Dashboard", "Admin");
                    }
                }
            }
            ModelState.AddModelError("LogOnError", "Användarnamn och/eller lösenord matchar inte");
            return View(objUser);
        }
        public ActionResult Whistle()
        {
            if (Session["UserID"] != null)
            {
                return RedirectToAction("ReportStatus","Whistle");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Whistle(LoginModel formModel)
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
                                return RedirectToAction("ReportStatus","Whistle");
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
        public ActionResult Lawyer()
        {
            if (Session["UserID"] != null)
            {
                return RedirectToAction("WhistleHandler","Lawyer");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Lawyer(LawyerModel objUser)
        {
            if (ModelState.IsValid)
            {
                using (var db = new DB.DBEntity())
                {
                    var obj = db.Lawyer.Where(a => a.Username.Equals(objUser.Username) && a.Password.Equals(objUser.Password)).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["UserID"] = obj.LawyerID.ToString();
                        Session["LoggedInAsLawyer"] = "1";
                        LawyerViewmodel.LoggedinID = obj.LawyerID;
                        return RedirectToAction("WhistleHandler","Lawyer");
                    }
                }
            }
            ModelState.AddModelError("LogOnError", "Användarnamn och/eller lösenord matchar inte");
            return View(objUser);
        }
        [HttpPost]
        public JsonResult CreateLawyerLogin(string username, string name)
        {
            var DBhandler = new DBHandler();
            var pass = DBHandler.CreateUser(username, name);

            return Json(pass, "application/json");
        }
        public JsonResult CreateSubject(string subject)
        {
            var DBhandler = new DBHandler();
            DBHandler.CreateSubject(subject);
            return Json(subject, "application/json");
        }
        public ActionResult LogOutUser()
        {
            if (Session["LoggedInAsLawyer"].ToString() == "2")
            {
                Session.Remove("UserID");
                Session.Remove("LoggedInAsLawyer");
                return RedirectToAction("Admin");
            }
            else if (Session["LoggedInAsLawyer"].ToString() == "1")
            {
                Session.Remove("UserID");
                Session.Remove("LoggedInAsLawyer");
                LawyerViewmodel.LoggedinID = 0;
                return RedirectToAction("Lawyer");
            }
            else
            {
                Session.Remove("UserID");
                Session.Remove("LoggedInAsLawyer");
                return RedirectToAction("Whistle");
            }
        }
    }
}