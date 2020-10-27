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
using Ionic.Zip;

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
        public ActionResult SendMail(Mail mail, int id, HttpPostedFileBase fileUpload)
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
            DB.File newFile;
            Mail tempMail = mail;
            if (fileUpload != null)
            {
                newFile = DBHandler.PostFile(new DB.File
                {
                    Base64 = Base64Handler.FileToBase64(fileUpload),
                    Extension = fileUpload.ContentType,
                    WhistleID = id
                });
                string fileID = newFile.FileID.ToString();
                DBHandler.PostMail(new Mail {DateSent = tempMail.DateSent, MailId = tempMail.MailId, MailSenderType = tempMail.MailSenderType, Message = fileID }, id, true);
            }
            DBHandler.PostMail(tempMail, id);

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

        public FileResult DownloadZip(int id)
        {
            using (var db = new DB.DBEntity())
            {
                List<DB.File> files = db.File.Where(f => f.WhistleID == id).ToList();
                using (ZipFile zip = new ZipFile())
                {
                    foreach (DB.File f in files)
                    {
                        string ext = f.Extension.Substring(f.Extension.IndexOf("/") + 1);
                        zip.AddEntry(f.FileID.ToString() + "." + f.Extension.Substring(f.Extension.IndexOf("/") + 1).Trim(), Convert.FromBase64String(f.Base64));
                    }
                    using (MemoryStream output = new MemoryStream())
                    {
                        zip.Save(output);
                        return File(output.ToArray(), "application/zip", id.ToString() + ".zip");
                    }
                }
            }
        }
    }
}