using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odigo.Model.Model
{
    public class TeachingCost
    {
        public int Id { get; set; }
        public StudentCategory StudentCategory { get; set; }
        public QualificationCategory QualificationCategory { get; set; }
        public int NoOfPeriod { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateEntered { get; set; }

        
    }


}
