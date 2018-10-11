using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel.DataAnnotations;

namespace Odigo.Model.Model
{
    public class Referee
    {
        public Person Person { get; set; }

        [Required]
        [Display(Name = "Referee's Name")]
        public string Name { get; set; }
               
        [Required]
        [Display(Name = "Referee's Address")]
        public string ContactAddress { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Mobile Phone")]
        public string MobilePhone { get; set; }

       
    }





}
