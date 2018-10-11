using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odigo.Model.Model
{
    public class TeacherOLevelResultDetail
    {
        public long Id { get; set; }
        public TeacherOLevelResult Result { get; set; }
        public OLevelSubject Subject { get; set; }
        public OLevelGrade Grade { get; set; }
    }



}
