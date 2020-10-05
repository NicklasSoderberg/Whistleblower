using DB;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Whistleblower.Models
{
    public class LawyerModel
    {
        [Required(ErrorMessage = "Var vänlig ange ett användarnamn")]
        [Display(Name = "Användarnamn*:")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Var vänlig ange ett lösenord")]
        [DataType(DataType.Password)]
        [Display(Name = "Lösenord*:")]
        public string Password { get; set; }
        public List<string> WhistleStatuses;
         public WhistleModel WhistleModel { get; set; }

        public List<WhistleModel> Whistles { get; set; }
        public WhistleModel SelectedWhistle { get; set; }
        public LawyerModel()
        {
            //test
            Username = "1234";
            Password = "test";

            WhistleStatuses = new List<string>
            {
                "Aktiv",
                "Hanteras",
                "Avslutad"
            };
            Whistles = new List<WhistleModel>
            {
                new WhistleModel{Description="Skanne säljer glass på kontoret", About="Bedrägeri", WhistleID = 1, CurrentStatus = "Aktiv" },
                new WhistleModel{Description="Skanne säljer glass på kontoret", About="Bedrägeri", WhistleID = 5, CurrentStatus = "Hanteras" },
                new WhistleModel{Description="Skanne säljer glass på kontoret", About="Bedrägeri", WhistleID = 4, CurrentStatus = "Aktiv" },
                new WhistleModel{Description="Skanne säljer glass på kontoret", About="Bedrägeri", WhistleID = 3, CurrentStatus = "Hanteras" },
                new WhistleModel{Description="Skanne säljer glass på kontoret", About="Bedrägeri", WhistleID = 2, CurrentStatus = "Avslutad" }
            };
        }
    }
}