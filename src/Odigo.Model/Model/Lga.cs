using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odigo.Model.Model
{
    public class Lga : BasicSetup
    {
        [Required]
        [Display(Name = "LGA")]
        public override int Id { get; set; }

        [Display(Name = "LGA")]
        public override string Name { get; set; }

        public string Code { get; set; }
        public State State { get; set; }
    }




}
