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
    public class SafeboxController : Controller
    {
        
        public ActionResult Safebox(int Id)
        {
            SafeboxViewmodel viewmodel = new SafeboxViewmodel(Id);
            using (var db = new DB.DBEntity())
            {
                if (db.Lawyer.FirstOrDefault(l => l.LawyerID == LawyerViewmodel.LoggedinID) != null)

                {

                    viewmodel.CurrentUser = "Lawyer";

                }
                else
                {

                    viewmodel.CurrentUser = "Whistler";

                }
            }
            viewmodel.WhistleId = Id;

           
            return View(viewmodel);
        }
        [HttpPost]
        public void SelectMail(int id)
        {
            SafeboxViewmodel._TempMailId = id;
       
        }

       
        [HttpPost]
        public ActionResult SendMail(Mail mail, int id)
        {
            //current user
            using (var db = new DB.DBEntity())
            {
                if (Session["LoggedInAsLawyer"].ToString() == "1")

                {

                    mail.MailSenderType = SafeboxViewmodel.MailSenders.Lawyer;

                }

                else

                {

                    mail.MailSenderType = SafeboxViewmodel.MailSenders.Whistler;

                }
            }
            DBHandler.PostMail(mail,id);

            return RedirectToAction($"Safebox/{id}", "Safebox");
        }

        public FileResult DownloadFile(int id)
        {
            if (id > 0)
            {
                using (var db = new DB.DBEntity())
                {

                    DB.File file = db.File.First(f => f.FileID == id);
                    byte[] imageBytes = Convert.FromBase64String(file.Base64);
                    string ext = file.Extension.Substring(file.Extension.IndexOf("/") + 1);
                    return File(imageBytes, file.Extension, file.FileID.ToString() + "." + ext.Trim());
                }
            }
            return null;
        }
    }
}