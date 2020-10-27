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

    public class AdminController : Controller
    {
        public ActionResult Dashboard(string SortBy)
        {
            if (Session["UserID"] != null)
            {

                List<DB.Whistle> Whistles = DBHandler.GetAllWhistles().OrderBy(l => l.LawyerID).ToList();
                return View(Whistles);
            }
            else
            {
                return RedirectToAction("Loginadmin", "Login");
            }
        }
            public ActionResult EditWhistle(DB.Whistle EditWhistle, string button)
        {
            if (Session["UserID"] != null)
            {
                switch (button?.ToLower())
                {
                    case "tillbaka":
                        return RedirectToAction("Dashboard", "Admin");

                    case "spara":
                        DBHandler.UpdateAdminWhistle(EditWhistle);
                        return RedirectToAction("Dashboard", "Admin");

                    default:
                        break;
                }

                return View(EditWhistle);
            }
            else
            {
                return HttpNotFound();
            }
        }

    }

    
}