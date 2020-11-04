using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Whistleblower.Custom;
using Whistleblower.Models;

namespace Whistleblower.ViewModels
{
    public class SafeboxViewmodel
    {
        public string CurrentUser { get; set; }
        public Mail Mail { get; set; }
        public static List<Mail> MailList { get; set; } = new List<Mail>();
        public static int _TempMailId { get; set; }
        public static int LastMessageType { get; set; }
        public enum MailSenders { Whistler = 0, File = 1, Lawyer = 2 };
        public List<Mail> _MailList { get { return MailList; } set { MailList = value; } }
        public List<DB.File> Files;
        public int WhistleId { get; set; }
        public SafeboxViewmodel(int id)
        {
            Files = DBHandler.GetFilesFromWhistleID(id);
            _MailList = DBHandler.GetMessages(id);
            int temp = _MailList.Count();
            if(temp > 0) { 
            if(_MailList[temp - 1].MailSenderType == MailSenders.Lawyer)
            {
             LastMessageType = 1;
            }
            else if(_MailList[temp - 1].MailSenderType == MailSenders.Whistler)
            {
                LastMessageType = 0;
            }
            }

        }

        public void SendMail(Mail mail, int id, HttpPostedFileBase fileUpload,string session)
        {
            //current user
            using (var db = new DB.DBEntity())
            {
                if (session == "1")
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
                DBHandler.PostMail(new Mail { DateSent = tempMail.DateSent, MailId = tempMail.MailId, MailSenderType = tempMail.MailSenderType, Message = fileID }, id, true);
            }
            DBHandler.PostMail(tempMail, id);

            
        }
    }
}