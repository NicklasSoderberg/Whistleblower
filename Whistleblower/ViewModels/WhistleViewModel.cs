using DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Whistleblower.Custom;
using Whistleblower.Models;

namespace Whistleblower.ViewModels
{
    public class WhistleViewModel
    {
        public WhistleModel Whistle { get; set; }
        public IEnumerable<HttpPostedFileBase> FileUpload { get; set; }
        public WhistleViewModel()
        {
            Whistle = new WhistleModel();
        }
    }
}