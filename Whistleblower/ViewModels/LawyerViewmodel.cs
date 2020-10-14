﻿using DB;
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
        public static int LoggedinID { get; set; }
        public LawyerViewmodel()
        {
            WhistleStatuses = new List<string>
            {
                "Aktiv",
                "Hanteras",
                "Avslutad"
            };
            if (LoggedinID > 0)
            {
                Whistles = DBHandler.GetWhistles(true);
            }
            else
            {
                Whistles = new List<WhistleModel>();

            }
        }
        public LawyerViewmodel(string sortBy)
        {
            WhistleStatuses = new List<string>
            {
                "Aktiv",
                "Hanteras",
                "Avslutad"
            };
            if(LoggedinID > 0) { 
            Whistles = DBHandler.GetWhistles(true);
                switch (sortBy?.ToLower())
                {
                    case "currentstatus":
                            Whistles = Whistles.OrderBy(l => l.CurrentStatus).ToList();
                        break;
                    case "description":
                        Whistles = Whistles.OrderBy(l => l.Description).ToList();
                        break;
                    case "about":
                        Whistles = Whistles.OrderBy(l => l.About).ToList();              
                        break;
                    default:
                        break;
                }
            }
            else
            {
                Whistles = new List<WhistleModel>();

            }
        }
    }
}