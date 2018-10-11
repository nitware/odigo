using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odigo.Model.Model
{
    public class State
    {
        [Required]
        [Display(Name = "State")]
        public string Id { get; set; }
        
        [Display(Name = "State")]
        public string Name { get; set; }

        public Country Country { get; set; }
    }


}
