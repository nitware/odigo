using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odigo.Model.Model
{
    public class EmployerType : Setup
    {
        [Display(Name = "Employer Type")]
        public override int Id { get; set; }

        [Display(Name = "Employer Type")]
        public override string Name { get; set; }
    }


}
