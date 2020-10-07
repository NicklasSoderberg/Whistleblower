using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Whistleblower.Custom;
using Whistleblower.Models;

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
            LawyerModel loginlawyer = new LawyerModel();
            if (ModelState.IsValid)
            {
                if (loginlawyer.Username == formLawyer.Username && loginlawyer.Password == formLawyer.Password)
                {
                    currentUser = "Lawyer";
                    return RedirectToAction("WhistleHandler");
                }
                else
                {
                    ModelState.AddModelError("LogOnError", "Användarnamn och/eller lösenord matchar inte");
                }
            }
            return View(formLawyer);
        }

        public ActionResult WhistleHandler()
        {
            LawyerModel model = new LawyerModel();

            return View(model);
        }
        
        public ActionResult Whistle(string id)
        {
            LawyerModel model = new LawyerModel();

        model.SelectedWhistle = model.Whistles.FirstOrDefault(m => m.WhistleID == int.Parse(id));
            return View(model);
        }
        [HttpPost]
        public ActionResult test(string id, LawyerModel model)
        {
            model.SelectedWhistle.WhistleID = int.Parse(id);
            DBHandler.Put(model.SelectedWhistle);
            return RedirectToAction("Whistle" + "/" + id);
        }
    }
}