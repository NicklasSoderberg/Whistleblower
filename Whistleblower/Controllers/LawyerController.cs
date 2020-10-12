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
            return View();
        }
        [HttpPost]
        public ActionResult Login(LawyerModel formLawyer)
        {
            if (ModelState.IsValid)
            {
                using (var db = new DB.DBEntity())
                {
                    if (db.Lawyer.FirstOrDefault(l => l.Username == formLawyer.Username && l.Password == formLawyer.Password) != null)
                    {
                        currentUser = "Lawyer";

                        LawyerViewmodel.LoggedinLawyer = db.Lawyer.FirstOrDefault(l => l.Username == formLawyer.Username);
                        return RedirectToAction("WhistleHandler");
                    }
                    else
                    {
                        ModelState.AddModelError("LogOnError", "Användarnamn och/eller lösenord matchar inte");
                    }
                }
            }
            return View(formLawyer);
        }
        public ActionResult Logout()
        {
            LawyerViewmodel.LoggedinLawyer = null;
            return RedirectToAction("Login");
        }

        public ActionResult WhistleHandler(string sortBy)
        {
            if(LawyerViewmodel.LoggedinLawyer != null)
            {
            LawyerViewmodel model = new LawyerViewmodel(sortBy);
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

                LawyerViewmodel model = new LawyerViewmodel("");

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