using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odigo.Model.Model
{
    public class TeacherAward
    {
        public long Id { get; set; }
        public Person Person { get; set; }
        public string AwardBody { get; set; }
        public string AwardName { get; set; }
        public int YearAwarded { get; set; }

    }


}
