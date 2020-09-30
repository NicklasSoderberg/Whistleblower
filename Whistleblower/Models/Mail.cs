using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Whistleblower.Models
{
    public class Mail
    {
        public int MailId { get; set; }
        public int ReportId { get; set; }
        public bool SentBool { get; set; }
        public string Message { get; set; }
        public int LawyerId { get; set; }

        public Mail()
        {

        }
    }
}