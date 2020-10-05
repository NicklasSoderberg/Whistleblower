using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Whistleblower.Models;

namespace Whistleblower.Controllers
{
    public class LawyerController : Controller
    {
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
        public ActionResult test(string id)
        {

            return RedirectToAction("Whistle" + "/" + id);
        }
    }
}