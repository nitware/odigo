﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odigo.Model.Model
{
    public class Sex
    {
        [Required]
        [Display(Name = "Sex")]
        public byte Id { get; set; }

        [Display(Name = "Sex")]
        public string Name { get; set; }
    }


}
