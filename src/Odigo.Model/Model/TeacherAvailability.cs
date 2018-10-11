using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odigo.Model.Model
{
    public class TeacherAvailability
    {
        public long Id { get; set; }
        public Person Person { get; set; }
        public WeekDay WeekDay { get; set; }
        public Period Period { get; set; }
        public bool IsAvailable { get; set; }
        public decimal Cost { get; set; }

    }
}
