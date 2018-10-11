using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odigo.Model.Model
{
    public class TeacherType : Setup
    {
        [Display(Name = "Mode of Teaching")]
        public override int Id { get; set; }

        [Display(Name = "Mode of Teaching")]
        public override string Name { get; set; }
    }


}
