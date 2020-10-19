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
                Whistles = SortWhistlesLawyerTable(DBHandler.GetWhistles(true).OrderBy(c => c.CurrentStatus).ToList(), WhistleStatuses);
            }
            else
            {
                Whistles = new List<WhistleModel>();

            }
        }

        //Sorts whistles in the order of the List<string> array starting with[0] -> 
        public static List<WhistleModel> SortWhistlesLawyerTable(List<WhistleModel> edit, List<string> Statuses)
        {
            List<WhistleModel> temp = edit;
            List<WhistleModel> returnThis = new List<WhistleModel>();

            foreach (string s in Statuses)
                for (int i = 0; i < temp.Count; i++)
                    if (temp[i].CurrentStatus == s)
                        returnThis.Add(temp[i]);

            return returnThis;
        }
    }
}