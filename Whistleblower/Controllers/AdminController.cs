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
            List<DB.Whistle> Whistles = DBHandler.GetWhistles();
            switch (SortBy?.ToLower())
            {
                case "lawyer":
                    if ((string)TempData["SortBy"] == "lawyer")
                    {
                        Whistles = Whistles.OrderBy(l => l.LawyerID).ToList();
                        TempData["SortBy"] = "";
                    }
                    else
                    {
                        Whistles = Whistles.OrderByDescending(l => l.LawyerID).ToList();
                        TempData["SortBy"] = SortBy;
                    }
                    break;

                case "status":
                    if ((string)TempData["SortBy"] == "status")
                    {
                        Whistles = Whistles.OrderBy(l => l.isActive).ToList();
                        TempData["SortBy"] = "";
                    }
                    else
                    {
                        Whistles = Whistles.OrderByDescending(l => l.isActive).ToList();
                        TempData["SortBy"] = SortBy;
                    }
                    break;

                case "whistleid":
                    if ((string)TempData["SortBy"] == "whistleid")
                    {
                        Whistles = Whistles.OrderBy(l => l.WhistleID).ToList();
                        TempData["SortBy"] = "";
                    }
                    else
                    {
                        Whistles = Whistles.OrderByDescending(l => l.WhistleID).ToList();
                        TempData["SortBy"] = SortBy;
                    }
                    break;

                case "about":
                    if ((string)TempData["SortBy"] == "about")
                    {
                        Whistles = Whistles.OrderBy(l => l.About).ToList();
                        TempData["SortBy"] = "";
                    }
                    else
                    {
                        Whistles = Whistles.OrderByDescending(l => l.About).ToList();
                        TempData["SortBy"] = SortBy;
                    }
                    break;

                default:
                    break;
            }

            return View(Whistles);
        }
        public ActionResult EditWhistle(DB.Whistle EditWhistle, string button)
        {
            switch (button?.ToLower())
            {
                case "tillbaka":
                    return RedirectToAction("Dashboard", "Admin");

                case "spara":
                    //UPPDATERAR INTE LAWYER ID, GÖR DETTA I VIEWMODEL I SPRINT 2
                    DBHandler.UpdateAdminWhistle(EditWhistle);
                    return RedirectToAction("Dashboard", "Admin");

                default:
                    break;
            }

            return View(EditWhistle);
        }

    }

    
}