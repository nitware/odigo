using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odigo.Model.Model
{
    public class ServiceCharge
    {
        public enum Services
        {
            TeacherSubscription = 1,
            ParentSubscription = 2,
            SchoolSubscription = 3,
            RequestToEmployTeacherWithCertificate = 4,
            RequestToEmployTeacherWithFirstDegree = 5,
            RequestToEmployTeacherWithSecondDegree = 6,
            RequestToEmployTeacherWithPhDDegree = 7
        }

        public int Id { get; set; }
        public Service Service { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateEntered { get; set; }
    }


}
