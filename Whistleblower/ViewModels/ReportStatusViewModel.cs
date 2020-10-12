using DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Whistleblower.Models;

namespace Whistleblower.ViewModels
{
    public class ReportStatusViewModel
    {
        public WhistleModel Whistle { get; set; }
        public Conversation Conversation { get; set; }
    }
}