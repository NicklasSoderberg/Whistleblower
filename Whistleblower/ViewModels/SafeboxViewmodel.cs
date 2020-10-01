using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Whistleblower.Models;

namespace Whistleblower.ViewModels
{
    public class SafeboxViewmodel
    {
        public Mail Mail { get; set; }
        public List<Mail> MailList { get; set; }
        public Mail SelectedMail { get; set; }
        public SafeboxViewmodel()
        {
            SelectedMail = new Mail();
        }
    }
}