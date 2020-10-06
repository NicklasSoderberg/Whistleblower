using DB;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Whistleblower.Custom;

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
            Whistles = DBHandler.GetWhistles();
        }
    }
}