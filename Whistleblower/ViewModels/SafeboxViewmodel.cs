using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Whistleblower.Models;

namespace Whistleblower.ViewModels
{
    public class SafeboxViewmodel
    {
        public string CurrentUser { get; set; }
        public Mail Mail { get; set; }
        public static List<Mail> MailList { get; set; } = new List<Mail>();
        public static int _TempMailId { get; set; }
        public enum MailSenders { Whistler = 0, File = 1, Lawyer = 2 };
        public List<Mail> _MailList { get { return MailList; } set { MailList = value; } }

        public int WhistleId { get; set; }
        public SafeboxViewmodel()
        { 

        }
    }
}