using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odigo.Model.Model
{
    public class TeacherEmploymentHistory
    {
        public long Id { get; set; }
        public Person Person { get; set; }
        public string Employer { get; set; }
        public string LastPositionHeld { get; set; }
        public int StartYear { get; set; }
        public int EndYear { get; set; }

    }


}
