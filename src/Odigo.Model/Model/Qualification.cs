﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odigo.Model.Model
{
    public class Qualification : Setup
    {
        public SchoolType SchoolType { get; set; }
        public QualificationCategory Category { get; set; }
    }



}
