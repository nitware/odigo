using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odigo.Model.Model
{
    public class RequestForEmploymentTeacherAvailability
    {
        public long Id { get; set; }
        public RequestForEmploymentCostImplication CostImplication { get; set; }
        public TeacherAvailability TeacherAvailability { get; set; }
        public TeachingCost TeachingCost { get; set; }
    }


}
