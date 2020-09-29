using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Whistleblower.Models
{
    public class WhistleModel
    {
        [Required(ErrorMessage = "Var vänlig fyll i fältet")]
        [Display(Name = "Vad handlar din rapport om?")]
        public string About { get; set; }

        [Required(ErrorMessage = "Var vänlig fyll i fältet")]
        [Display(Name = "När inträffade detta?")]
        [StringLength(280, ErrorMessage = "Max 280 tecken")]
        public string When { get; set; }

        [Required(ErrorMessage = "Var vänlig fyll i fältet")]
        [Display(Name = "Var inträffade detta?")]
        public string Where { get; set; }

        [Required(ErrorMessage = "Var vänlig fyll i fältet")]
        [Display(Name = "Beskriv vad som har hänt.")]
        public string Description { get; set; }
    }
}