using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel.DataAnnotations;

namespace Odigo.Model.Model
{
    public class OLevelSubject : Setup
    {
        //public int Id { get; set; }

        [Display(Name = "O-Level Subject")]
        public override string Name { get; set; }

        //public string Description { get; set; }

        public int Rank { get; set; }
    }




}
