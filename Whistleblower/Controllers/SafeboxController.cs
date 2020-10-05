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
        public ActionResult Safebox(SafeboxViewmodel viewmodel)
        {
            if(viewmodel == null)
            {
                 viewmodel = new SafeboxViewmodel();
            }

            if(SafeboxViewmodel.MailList.Count == 0)
            {
                Mail m1 = new Mail {  MailId = 1,MailSenderType = SafeboxViewmodel.MailSenders.Lawyer,  Message = "Hello my friend what happened?" };       
                SafeboxViewmodel.MailList.Add(m1);
            }

           
            return View(viewmodel);
        }
        [HttpPost]
        public void SelectMail(int id)
        {
            SafeboxViewmodel._TempMailId = id;
       
        }
        [HttpPost]
        public ActionResult SendMail(Mail mail)
        {
            //current user
            mail.MailSenderType = SafeboxViewmodel.MailSenders.Whistler;
            //SafeboxViewmodel.MailList.FirstOrDefault(m => m.MailId == SafeboxViewmodel._TempMailId).ResponedToMail = true;
            SafeboxViewmodel.MailList.Add(mail);

            return RedirectToAction("Safebox", "Safebox");
        }

        public ActionResult MailSent()
        {
            return View();
        }
    }
}