using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odigo.Model.Model
{
    public class RequestForEmploymentCostImplication
    {
        public long Id { get; set; }
        public Request Request { get; set; }
        public EmployerStudentCategory EmployerStudentCategory { get; set; }
        public decimal MonthlyPay { get; set; }

        //public List<TeacherAvailability> TeacherPeriodsAvailable { get; set; }
        public List<RequestForEmploymentTeacherAvailability> TeacherAvailabilities { get; set; }


    }





}
