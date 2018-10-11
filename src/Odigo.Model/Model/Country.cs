using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odigo.Model.Model
{
    public class Country : BasicSetup
    {
        public string Code { get; set; }

        [Display(Name = "Country")]
        public override string Name { get; set; }
    }


}
