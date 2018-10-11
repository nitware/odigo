using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Odigo.Web.Models;
using System.Web.Mvc;
using Odigo.Model.Model;

namespace Odigo.Web.Areas.Teacher.Models
{
    public class TeacherSubscriptionViewModel : BaseSubscriptionViewModel
    {
        public TeacherSubscriptionViewModel()
        {
            Referee = new Referee();

            FirstSittingOLevelResult = new TeacherOLevelResult();
            FirstSittingOLevelResult.Type = new OLevelType();
            SecondSittingOLevelResult = new TeacherOLevelResult();
            SecondSittingOLevelResult.Type = new OLevelType();
            
            TeacherEducationalQualification = new TeacherEducationalQualification();
            TeacherEducationalQualification.Qualification = new Qualification();
            TeacherEducationalQualification.SchoolType = new SchoolType();

            Periods = new List<Period>();
            WeekDays = new List<WeekDay>();
            StudentCategories = new List<StudentCategory>();
            
            InitialiseOLevelResult();
            InitialiseEducationalQualification();
            InitialiseEmploymentHistory();
            InitialiseAward();
        }

        public bool AwardExist { get; set; }
        public bool EmploymentHistoryExist { get; set; }
                
        public Referee Referee { get; set; }
        public SchoolType SchoolType { get; set; }
        public Qualification Qualification { get; set; }
        public HttpPostedFileBase PassportFile { get; set; }
        public List<WeekDay> WeekDays { get; set; }
        public List<Period> Periods { get; set; }
        public List<StudentCategory> StudentCategories { get; set; }
        public List<TeacherAvailability> TeacherAvailabilities { get; set; }
        public List<TeacherStudentCategory> TeacherStudentCategories { get; set; }

        public List<Sex> Sexs { get; set; }
        public List<State> States { get; set; }
        public List<Qualification> Qualifications { get; set; }

        public List<OLevelType> OLevelTypes { get; set; }
        public List<OLevelGrade> OLevelGrades { get; set; }
        public List<OLevelSubject> OLevelSubjects { get; set; }
        public TeacherOLevelResult FirstSittingOLevelResult { get; set; }
        public TeacherOLevelResult SecondSittingOLevelResult { get; set; }
        public List<TeacherType> TeacherTypes { get; set; }
        public List<SchoolType> SchoolTypes { get; set; }
        public List<SecurityQuestion> SecurityQuestions { get; set; }

        public List<TeacherOLevelResultDetail> FirstSittingOLevelResultDetails { get; set; }
        public List<TeacherOLevelResultDetail> SecondSittingOLevelResultDetails { get; set; }
        public List<TeacherEducationalQualification> TeacherEducationalQualifications { get; set; }
        public List<TeacherEmploymentHistory> TeacherEmploymentHistories { get; set; }
        public List<TeacherAward> TeacherAwards { get; set; }
        public TeacherEducationalQualification TeacherEducationalQualification { get; set; }

        public List<SelectListItem> FirstSittingExamYearSelectList { get; set; }
        public List<SelectListItem> FirstSittingOLevelTypeSelectList { get; set; }
        public List<SelectListItem> SecondSittingExamYearSelectList { get; set; }
        public List<SelectListItem> SecondSittingOLevelTypeSelectList { get; set; }

        public List<SelectListItem> OLevelGradeSelectList { get; set; }
        public List<SelectListItem> OLevelSubjectSelectList { get; set; }
        public List<SelectListItem> StateSelectList { get; set; }
        public List<SelectListItem> SexSelectList { get; set; }
        public List<SelectListItem> OccupationSelectList { get; set; }
        public List<SelectListItem> CountrySelectList { get; set; }
        public List<SelectListItem> DayOfBirthSelectList { get; set; }
        public List<SelectListItem> MonthOfBirthSelectList { get; set; }
        public List<SelectListItem> YearOfBirthSelectList { get; set; }
        public List<SelectListItem> QualificationSelectList { get; set; }

        //public List<SelectListItem> ModeOfEntrySelectList { get; set; }
        //public List<SelectListItem> ModeOfStudySelectList { get; set; }

        public List<SelectListItem> SchoolTypeSelectList { get; set; }
        public List<SelectListItem> TeacherTypeSelectList { get; set; }
        public List<SelectListItem> YearSelectList { get; set; }
        public List<SelectListItem> SecurityQuestionSelectList { get; set; }

        public void InitialiseOLevelResult()
        {
            try
            {
                List<TeacherOLevelResultDetail> oLevelResultDetails = new List<TeacherOLevelResultDetail>();
                TeacherOLevelResultDetail oLevelResultDetail1 = new TeacherOLevelResultDetail() { Grade = new OLevelGrade() { Id = 0 }, Subject = new OLevelSubject() { Id = 0 } };
                TeacherOLevelResultDetail oLevelResultDetail2 = new TeacherOLevelResultDetail() { Grade = new OLevelGrade() { Id = 0 }, Subject = new OLevelSubject() { Id = 0 } };
                TeacherOLevelResultDetail oLevelResultDetail3 = new TeacherOLevelResultDetail() { Grade = new OLevelGrade() { Id = 0 }, Subject = new OLevelSubject() { Id = 0 } };
                TeacherOLevelResultDetail oLevelResultDetail4 = new TeacherOLevelResultDetail() { Grade = new OLevelGrade() { Id = 0 }, Subject = new OLevelSubject() { Id = 0 } };
                TeacherOLevelResultDetail oLevelResultDetail5 = new TeacherOLevelResultDetail() { Grade = new OLevelGrade() { Id = 0 }, Subject = new OLevelSubject() { Id = 0 } };
                TeacherOLevelResultDetail oLevelResultDetail6 = new TeacherOLevelResultDetail() { Grade = new OLevelGrade() { Id = 0 }, Subject = new OLevelSubject() { Id = 0 } };
                TeacherOLevelResultDetail oLevelResultDetail7 = new TeacherOLevelResultDetail() { Grade = new OLevelGrade() { Id = 0 }, Subject = new OLevelSubject() { Id = 0 } };
                TeacherOLevelResultDetail oLevelResultDetail8 = new TeacherOLevelResultDetail() { Grade = new OLevelGrade() { Id = 0 }, Subject = new OLevelSubject() { Id = 0 } };
                TeacherOLevelResultDetail oLevelResultDetail9 = new TeacherOLevelResultDetail() { Grade = new OLevelGrade() { Id = 0 }, Subject = new OLevelSubject() { Id = 0 } };

                TeacherOLevelResultDetail oLevelResultDetail11 = new TeacherOLevelResultDetail() { Grade = new OLevelGrade() { Id = 0 }, Subject = new OLevelSubject() { Id = 0 } };
                TeacherOLevelResultDetail oLevelResultDetail22 = new TeacherOLevelResultDetail() { Grade = new OLevelGrade() { Id = 0 }, Subject = new OLevelSubject() { Id = 0 } };
                TeacherOLevelResultDetail oLevelResultDetail33 = new TeacherOLevelResultDetail() { Grade = new OLevelGrade() { Id = 0 }, Subject = new OLevelSubject() { Id = 0 } };
                TeacherOLevelResultDetail oLevelResultDetail44 = new TeacherOLevelResultDetail() { Grade = new OLevelGrade() { Id = 0 }, Subject = new OLevelSubject() { Id = 0 } };
                TeacherOLevelResultDetail oLevelResultDetail55 = new TeacherOLevelResultDetail() { Grade = new OLevelGrade() { Id = 0 }, Subject = new OLevelSubject() { Id = 0 } };
                TeacherOLevelResultDetail oLevelResultDetail66 = new TeacherOLevelResultDetail() { Grade = new OLevelGrade() { Id = 0 }, Subject = new OLevelSubject() { Id = 0 } };
                TeacherOLevelResultDetail oLevelResultDetail77 = new TeacherOLevelResultDetail() { Grade = new OLevelGrade() { Id = 0 }, Subject = new OLevelSubject() { Id = 0 } };
                TeacherOLevelResultDetail oLevelResultDetail88 = new TeacherOLevelResultDetail() { Grade = new OLevelGrade() { Id = 0 }, Subject = new OLevelSubject() { Id = 0 } };
                TeacherOLevelResultDetail oLevelResultDetail99 = new TeacherOLevelResultDetail() { Grade = new OLevelGrade() { Id = 0 }, Subject = new OLevelSubject() { Id = 0 } };

                FirstSittingOLevelResultDetails = new List<TeacherOLevelResultDetail>();
                FirstSittingOLevelResultDetails.Add(oLevelResultDetail1);
                FirstSittingOLevelResultDetails.Add(oLevelResultDetail2);
                FirstSittingOLevelResultDetails.Add(oLevelResultDetail3);
                FirstSittingOLevelResultDetails.Add(oLevelResultDetail4);
                FirstSittingOLevelResultDetails.Add(oLevelResultDetail5);
                FirstSittingOLevelResultDetails.Add(oLevelResultDetail6);
                FirstSittingOLevelResultDetails.Add(oLevelResultDetail7);
                FirstSittingOLevelResultDetails.Add(oLevelResultDetail8);
                FirstSittingOLevelResultDetails.Add(oLevelResultDetail9);

                SecondSittingOLevelResultDetails = new List<TeacherOLevelResultDetail>();
                SecondSittingOLevelResultDetails.Add(oLevelResultDetail11);
                SecondSittingOLevelResultDetails.Add(oLevelResultDetail22);
                SecondSittingOLevelResultDetails.Add(oLevelResultDetail33);
                SecondSittingOLevelResultDetails.Add(oLevelResultDetail44);
                SecondSittingOLevelResultDetails.Add(oLevelResultDetail55);
                SecondSittingOLevelResultDetails.Add(oLevelResultDetail66);
                SecondSittingOLevelResultDetails.Add(oLevelResultDetail77);
                SecondSittingOLevelResultDetails.Add(oLevelResultDetail88);
                SecondSittingOLevelResultDetails.Add(oLevelResultDetail99);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void InitialiseEducationalQualification()
        {
            try
            {
                List<TeacherEducationalQualification> educationalQualifications = new List<TeacherEducationalQualification>();
                TeacherEducationalQualification educationalQualifications1 = new TeacherEducationalQualification() { SchoolType = new SchoolType() { Id = 1 }, Qualification = new Qualification() { Id = 0 }, YearOfAdmission = 0, YearOfGraduation = 0 };
                TeacherEducationalQualification educationalQualifications2 = new TeacherEducationalQualification() { SchoolType = new SchoolType() { Id = 2 }, Qualification = new Qualification() { Id = 0 }, YearOfAdmission = 0, YearOfGraduation = 0 };
                //TeacherEducationalQualification educationalQualifications3 = new TeacherEducationalQualification() { SchoolType = new SchoolType() { Id = 2 }, Qualification = new Qualification() { Id = 0 }, YearOfAdmission = 0, YearOfGraduation = 0 };
                TeacherEducationalQualification educationalQualifications4 = new TeacherEducationalQualification() { SchoolType = new SchoolType() { Id = 3 }, Qualification = new Qualification() { Id = 0 }, YearOfAdmission = 0, YearOfGraduation = 0 };
                TeacherEducationalQualification educationalQualifications5 = new TeacherEducationalQualification() { SchoolType = new SchoolType() { Id = 3 }, Qualification = new Qualification() { Id = 0 }, YearOfAdmission = 0, YearOfGraduation = 0 };
                TeacherEducationalQualification educationalQualifications6 = new TeacherEducationalQualification() { SchoolType = new SchoolType() { Id = 3 }, Qualification = new Qualification() { Id = 0 }, YearOfAdmission = 0, YearOfGraduation = 0 };
                
                TeacherEducationalQualifications = new List<TeacherEducationalQualification>();
                TeacherEducationalQualifications.Add(educationalQualifications1);
                TeacherEducationalQualifications.Add(educationalQualifications2);
                //TeacherEducationalQualifications.Add(educationalQualifications3);
                TeacherEducationalQualifications.Add(educationalQualifications4);
                TeacherEducationalQualifications.Add(educationalQualifications5);
                TeacherEducationalQualifications.Add(educationalQualifications6);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void InitialiseEmploymentHistory()
        {
            try
            {
                List<TeacherEmploymentHistory> employmentHistories = new List<TeacherEmploymentHistory>();
                TeacherEmploymentHistory employmentHistory1 = new TeacherEmploymentHistory() { StartYear = 0, EndYear = 0 };
                TeacherEmploymentHistory employmentHistory2 = new TeacherEmploymentHistory() { StartYear = 0, EndYear = 0 };
                TeacherEmploymentHistory employmentHistory3 = new TeacherEmploymentHistory() { StartYear = 0, EndYear = 0 };
                TeacherEmploymentHistory employmentHistory4 = new TeacherEmploymentHistory() { StartYear = 0, EndYear = 0 };

                TeacherEmploymentHistories = new List<TeacherEmploymentHistory>();
                TeacherEmploymentHistories.Add(employmentHistory1);
                TeacherEmploymentHistories.Add(employmentHistory2);
                TeacherEmploymentHistories.Add(employmentHistory3);
                TeacherEmploymentHistories.Add(employmentHistory4);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void InitialiseAward()
        {
            try
            {
                List<TeacherAward> teacherAwards = new List<TeacherAward>();
                TeacherAward teacherAward1 = new TeacherAward() { YearAwarded = 0 };
                TeacherAward teacherAward2 = new TeacherAward() { YearAwarded = 0 };
                TeacherAward teacherAward3 = new TeacherAward() { YearAwarded = 0 };
                TeacherAward teacherAward4 = new TeacherAward() { YearAwarded = 0 };

                TeacherAwards = new List<TeacherAward>();
                TeacherAwards.Add(teacherAward1);
                TeacherAwards.Add(teacherAward2);
                TeacherAwards.Add(teacherAward3);
                TeacherAwards.Add(teacherAward4);
            }
            catch (Exception)
            {
                throw;
            }
        }






    }



}