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
            if (LawyerViewmodel.LoggedinLawyer != null)
            {
                viewmodel.CurrentUser = "Lawyer";
            }else 
            {
                viewmodel.CurrentUser = "Whistler";
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
            if(LawyerViewmodel.LoggedinLawyer == null)
            {
                mail.MailSenderType = SafeboxViewmodel.MailSenders.Whistler;
            }
            else
            {
                mail.MailSenderType = SafeboxViewmodel.MailSenders.Lawyer;
            }
            DBHandler.PostMail(mail,id);

            return RedirectToAction($"Safebox/{id}", "Safebox");
        }
    }
}