using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odigo.Model.Model
{
    public class EmployerDesiredTime
    {
        public long Id { get; set; }
        public EmployerStudentCategory EmployerStudentCategory { get; set; }
        public WeekDay WeekDay { get; set; }
        public Period Period { get; set; }

        public bool IsAvailable { get; set; }
    }


}
