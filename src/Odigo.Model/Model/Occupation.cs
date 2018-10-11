using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odigo.Model.Model
{
    public class Occupation : BasicSetup
    {
        [Display(Name = "Occupation")]
        public override int Id { get; set; }

        [Display(Name = "Occupation")]
        public override string Name { get; set; }
    }



}
