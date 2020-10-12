using DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Whistleblower.Custom;
using Whistleblower.Models;

namespace Whistleblower.ViewModels
{
    public class LawyerViewmodel
    {
        public List<string> WhistleStatuses;

        public WhistleModel WhistleModel { get; set; }

        public List<WhistleModel> Whistles { get; set; }
        public WhistleModel SelectedWhistle { get; set; }
        public static Lawyer LoggedinLawyer { get; set; }
        public LawyerViewmodel()
        {
            WhistleStatuses = new List<string>
            {
                "Aktiv",
                "Hanteras",
                "Avslutad"
            };
            if(LoggedinLawyer != null) { 
            Whistles = DBHandler.GetWhistles(LoggedinLawyer.LawyerID);
            }
            else
            {
                Whistles = new List<WhistleModel>();

            }
        }
    }
}