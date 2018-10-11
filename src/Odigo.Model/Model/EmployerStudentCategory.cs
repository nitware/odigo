using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odigo.Model.Model
{
    public class EmployerStudentCategory
    {
        public long Id { get; set; }
        public Person Person { get; set; }
        public StudentCategory StudentCategory { get; set; }
        public TeacherType TeacherType { get; set; }
        public int NoOfStudent { get; set; }

        public bool IsSelected { get; set; }
        public List<EmployerDesiredTime> DesiredTimes { get; set; }
    }




}
