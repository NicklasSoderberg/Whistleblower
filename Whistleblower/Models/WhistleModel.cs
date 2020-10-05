using DB;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Whistleblower.Models
{
    public class WhistleModel
    {
        [Required(ErrorMessage = "Var vänlig välj ett alternativ")]
        [Display(Name = "Vad gäller ärendet?")]
        public string About { get; set; }

        [Required(ErrorMessage = "Var vänlig fyll i fältet")]
        [Display(Name = "När inträffade händelsen?")]
        [StringLength(280, ErrorMessage = "Max 280 tecken")]
        public string When { get; set; }

        [Required(ErrorMessage = "Var vänlig fyll i fältet")]
        [Display(Name = "Vart inträffade händelsen?")]
        [StringLength(280, ErrorMessage = "Max 280 tecken")]
        public string Where { get; set; }

        [Required(ErrorMessage = "Var vänlig fyll i fältet")]
        [Display(Name = "Detaljer om ärendet")]
        [StringLength(280, ErrorMessage = "Max 280 tecken")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Var vänlig fyll i fältet")]
        [Display(Name = "Är andra anställda medvetna om detta?")]
        [StringLength(280, ErrorMessage = "Max 280 tecken")]
        public string Description_OtherEmployees { get; set; }

        public List<string> Subjects;

        public User user { get; set; }
        
        public WhistleModel()
        {
            Subjects = new List<string> {
            "Mutor, korruption & förfalskning",
            "Dataskydd och brott mot IT-säkerhet",
            "Diskriminering, trakasserier och andra arbetsrelaterade lagproblem",
            "Bedrägeri, missbruk och stöld",
            "Hälsa, säkerhet & miljö",
            "Penningtvätt",
            "Personal",
            "Annat" };
            user = new User();
        }
    }
}