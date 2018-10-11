using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace Odigo.Model.Model
{
    public class TeacherOLevelResult
    {
        public long Id { get; set; }
        public Person Person { get; set; }
        //public PersonType PersonType { get; set; }
                
        [Display(Name="Exam No")]
        public string ExamNumber { get; set; }

        [Required]
        [Display(Name = "Exam Year")]
        public int ExamYear { get; set; }
        public OLevelExamSitting Sitting { get; set; }
        public OLevelType Type { get; set; }

        //public ApplicationForm ApplicationForm { get; set; }

        public List<TeacherOLevelResultDetail> OLevelResultDetails { get; set; }
    }


}
