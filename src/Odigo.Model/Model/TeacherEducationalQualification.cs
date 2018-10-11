using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odigo.Model.Model
{
    public class TeacherEducationalQualification
    {
        public long Id { get; set; }
        public Person Person { get; set; }
        public string School { get; set; }
        public SchoolType SchoolType { get; set; }
        public int YearOfAdmission { get; set; }
        public int YearOfGraduation { get; set; }
        public Qualification Qualification { get; set; }

        //public bool IsSet { get; set; }

    }


}
