using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Whistleblower.Custom;
using Whistleblower.Models;
using Whistleblower.ViewModels;

namespace Whistleblower.Controllers
{
    public class LawyerController : Controller
    {


        public static string currentUser { get; set; }
        // GET: Lawyer

        public ActionResult Login()
        {
            if (Session["UserID"] != null)
            {
                return RedirectToAction("WhistleHandler");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LawyerModel objUser)
        {
            if (ModelState.IsValid)
            {
                using (var db = new DB.DBEntity())
                {
                    var obj = db.Lawyer.Where(a => a.Username.Equals(objUser.Username) && a.Password.Equals(objUser.Password)).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["UserID"] = obj.LawyerID.ToString();
                        Session["UserName"] = obj.Username;
                        return RedirectToAction("WhistleHandler");
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
            return RedirectToAction("Login");
        }
        //public ActionResult UserDashBoard()
        //{
        //    if (Session["UserID"] != null)
        //    {
        //        return View();
        //    }
        //    else
        //    {
        //        return RedirectToAction("Login");
        //    }
        //}
        //public ActionResult Login(LawyerModel formLawyer)
        //{
        //    LawyerModel loginlawyer = new LawyerModel();
        //    if (ModelState.IsValid)
        //    {
        //        if (loginlawyer.Username == formLawyer.Username && loginlawyer.Password == formLawyer.Password)
        //        {
        //            currentUser = "Lawyer";
        //            return RedirectToAction("WhistleHandler");
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("LogOnError", "Användarnamn och/eller lösenord matchar inte");
        //        }
        //    }
        //    return View(formLawyer);
        //}

        public ActionResult WhistleHandler()
        {
            if(LawyerViewmodel.LoggedinLawyer != null)
            {

            
            LawyerViewmodel model = new LawyerViewmodel();

            return View(model);
            }
            else
            {
              return  RedirectToAction("Login");
            }
        }
        
        public ActionResult Whistle(string id)
        {
            if (LawyerViewmodel.LoggedinLawyer != null && id != null)
            {

                LawyerViewmodel model = new LawyerViewmodel();

                model.SelectedWhistle = model.Whistles.FirstOrDefault(m => m.WhistleID == int.Parse(id));
                return View(model);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        [HttpPost]
        public ActionResult test(string id, LawyerViewmodel model)
        {
            model.SelectedWhistle.WhistleID = int.Parse(id);
            DBHandler.Put(model.SelectedWhistle);
            return RedirectToAction("Whistle" + "/" + id);
        }
    }
}