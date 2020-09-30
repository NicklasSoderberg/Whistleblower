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
        public string UserName { get; set; }

        [Required(ErrorMessage = "Lösenord är obligatoriskt")]
        [DataType(DataType.Password)]
        [Display(Name = "Ange lösenord*")]
        public string Password { get; set; }

        public LoginModel()
        {
            UserName = "1234";
            Password = "test";
        }
    }
}