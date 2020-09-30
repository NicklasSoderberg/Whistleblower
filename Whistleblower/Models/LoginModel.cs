using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Whistleblower.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Ange ID")]
        [Display(Name = "Ange ID*")]
        [RegularExpression(@"^[0-9]{10}", ErrorMessage = "Please enter a valid email address")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Lösenord är obligatoriskt")]
        [Display(Name = "Ange lösenord*")]
        public string Password { get; set; }
    }
}