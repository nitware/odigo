using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odigo.Model.Model
{
    public class Teacher //: Person
    {
        public Person Person { get; set; }
        public TeacherType Type { get; set; }
        public Rating Rating { get; set; }
        public TeacherAvailabilityStatus AvailabilityStatus { get; set; }
        public TeacherVerificationStatus VerificationStatus { get; set; }

        public Sex Sex { get; set; }
        public string ImageFileUrl { get; set; }

        [Display(Name = "Date of Birth")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateOfBirth { get; set; }

        public Value DayOfBirth { get; set; }
        public Value MonthOfBirth { get; set; }
        public Value YearOfBirth { get; set; }
        
        [Display(Name = "Home Town")]
        public string HomeTown { get; set; }

        [Display(Name = "Home Address")]
        public string HomeAddress { get; set; }
        
        public Referee Referee { get; set; }
        public LoginDetail LoginDetail { get; set; }
        public List<TeacherEducationalQualification> EducationalQualifications { get; set; }
        public List<TeacherEmploymentHistory> EmploymentHistories { get; set; }
        public List<TeacherStudentCategory> StudentCategories { get; set; }
        public List<TeacherAvailability> TeacherAvailabilities { get; set; }
        public List<TeacherAward> TeacherAwards { get; set; }

        public string Qualifications { get; set; }
        public string ChildCategories { get; set; }
        public string Experience { get; set; }

        public bool IsSelected { get; set; }
        public List<TeacherOLevelResult> OLevelResults { get; set; }
        public PaymentSlip PaymentSlip { get; set; }
        public List<Payment> Payments { get; set; }
        

    }



}
