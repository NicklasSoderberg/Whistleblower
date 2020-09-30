using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Whistleblower.Models;

namespace Whistleblower.ViewModels
{
    public class SafeboxViewmodel
    {
        public List<Mail> MailList { get; set; }
        public static Mail SelectedMail { get; set; }
        public SafeboxViewmodel()
        {
            MailList = new List<Mail>()
            {
                new Mail{ReportId=1,SentBool=false,LawyerId=1,Message="Hello my friend what happened?"},
            };
        }
    }
}