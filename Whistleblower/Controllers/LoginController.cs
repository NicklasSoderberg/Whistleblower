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
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Runtime.Remoting.Messaging;

namespace Whistleblower.Controllers
{
    public class LoginController : Controller
    {
        private static string WebAPIURL = "https://localhost:44332/";
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


        // Test Method for JWTToken
        public async Task<ActionResult> Test()
        {
            var tokenBased = string.Empty;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.BaseAddress = new Uri(WebAPIURL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var responseMessage = await client.GetAsync("Account/ValidLogin?userName=admin&userPassword=admin");
                if (responseMessage.IsSuccessStatusCode)
                {
                    var resultMessage = responseMessage.Content.ReadAsStringAsync().Result;
                    tokenBased = JsonConvert.DeserializeObject<string>(resultMessage);
                    Session["TokenNumber"] = tokenBased;
                    Session["UserName"] = "admin";
                }
            }
            return Content(tokenBased);
        }

        // Test Method for JWTToken
        public async Task<ActionResult> GetEmployee()
        {
            string ReturnMessage = string.Empty;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.BaseAddress = new Uri(WebAPIURL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                    Session["TokenNumber"].ToString() + ":" + Session["UserName"]);
                var responseMessage = await client.GetAsync("Account/GetEmployee");
                if (responseMessage.IsSuccessStatusCode)
                {
                    var resultMessage = responseMessage.Content.ReadAsStringAsync().Result;
                    ReturnMessage = resultMessage;
                }
            }

            return Content(ReturnMessage);
        }
    }
}