﻿using System;
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
        public SafeboxViewmodel.MailSenders MailSenderType { get; set; }
        //public bool RespondedToMail { get; set; }
        [Required]
        [StringLength(180)]
        public string Message { get; set; } 
        public DateTime DateSent { get; set; }
        public Mail()
        {

        }
    }
}