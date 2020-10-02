using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Whistleblower.ViewModels;

namespace Whistleblower.Models
{
    public class Mail
    {
        public int MailId { get; set; }
        public int ConversationId { get; set; }
        public SafeboxViewmodel.MailSenders MailSenderType { get; set; }
        public bool SentBool { get; set; }
        public int SenderMailId { get; set; }
        public bool ResponedToMail { get; set; }
        [Required]
        [StringLength(255)]
        public string Message { get; set; }
        public Mail()
        {

        }
    }
}