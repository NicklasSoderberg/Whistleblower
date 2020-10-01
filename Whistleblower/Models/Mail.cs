using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Whistleblower.Models
{
    public class Mail
    {
        public int MailId { get; set; }
        public int ReportId { get; set; }
        public bool SentBool { get; set; }
        [Required]
        [StringLength(255)]
        public string Message { get; set; }
        public int LawyerId { get; set; }
        public byte FileBinary { get; set; }

        public Mail()
        {

        }
    }
}