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
        public int LawyerID { get; set; }
        [Required(ErrorMessage = "Var vänlig ange ett användarnamn")]
        [Display(Name = "Användarnamn*:")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Var vänlig ange ett lösenord")]
        [DataType(DataType.Password)]
        [Display(Name = "Lösenord*:")]
        public string Password { get; set; }
        public LawyerModel()
        {
            //test
            Username = "1234";
            Password = "test";
        }
    }
}