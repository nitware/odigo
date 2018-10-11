using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Odigo.Business.Interfaces;
using Odigo.Data;
using Odigo.Utility.Interfaces;
using Odigo.Utility;
using Odigo.Model.Model;
using System.Collections.Generic;

namespace Odigo.Business.Test
{
    [TestClass]
    public class TeacherSubscriptionServiceTest
    {
        [TestMethod]
        public void TestSave()
        {
            //IRepository da = new Repository();
            //IFileManager fileManager = new FileManager(@"C:\Users\Dan\Documents\Visual Studio 2015\Projects\Odigo\Odigo.Web");
            //IImageManager passportManager = new ImageManager(da, fileManager);
            //TeacherSubscriptionService service = new TeacherSubscriptionService(da, passportManager);
            
            //Teacher teacher = new Teacher();

            ////teacher
            //teacher.Type = new TeacherType() { Id = 1 };

            ////person
            //teacher.Person = new Person();
            //teacher.Person.LastName = "Odigo";
            //teacher.Person.FirstName = "Odigo";
            //teacher.Person.Sex = new Sex() { Id = 2 };
            //teacher.Person.State = new State() { Id = "AB" };
            //teacher.Person.Lga = new Lga() { Id = 2 };
            //teacher.Person.Email = "Odigo@yahoo.com";
            //teacher.Person.ContactAddress = "Oyeagu, Ifitedunu";
            //teacher.Person.DateOfBirth = new DateTime(1988, 2, 14);
            //teacher.Person.MobilePhone = "08097889000";
            //teacher.Person.Country = new Country() { Id = 1 };
            //teacher.Person.DateEntered = DateTime.Now;
            //teacher.Person.Type = new PersonType() { Id = 2 };
            //teacher.Person.ImageFileUrl = "/Content/Junk/0__16042017063407.jpg";

            ////referee
            //teacher.Referee = new Referee();
            //teacher.Referee.Person = teacher.Person;
            //teacher.Referee.Name = "John Alu";
            //teacher.Referee.ContactAddress = "12 Oyibo Street, Igbo";
            //teacher.Referee.Email = "john.alu@yahoo.com";
            //teacher.Referee.MobilePhone = "07035678900";

            ////educational qualification
            //teacher.EducationalQualifications = new List<TeacherEducationalQualification>();
            //TeacherEducationalQualification educationalQualification = new TeacherEducationalQualification();
            //educationalQualification.Person = teacher.Person;
            //educationalQualification.SchoolType = new SchoolType() { Id = 1 };
            //educationalQualification.School = "AUD Primary School";
            //educationalQualification.Qualification = new Qualification() { Id = 1 };
            //educationalQualification.YearOfAdmission = 1980;
            //educationalQualification.YearOfGraduation = 1985;
            //teacher.EducationalQualifications.Add(educationalQualification);

            ////employment history
            //teacher.EmploymentHistories = new List<TeacherEmploymentHistory>();
            //TeacherEmploymentHistory employmentHistory = new TeacherEmploymentHistory();
            //employmentHistory.LastPositionHeld = "Head, Software Services";
            //employmentHistory.Person = teacher.Person;
            //employmentHistory.StartYear = 2005;
            //employmentHistory.EndYear = 2007;
            //employmentHistory.Employer = "Infosoft";
            //teacher.EmploymentHistories.Add(employmentHistory);

            ////student category
            //teacher.StudentCategories = new List<TeacherStudentCategory>();
            //TeacherStudentCategory studentCategory = new TeacherStudentCategory();
            //studentCategory.Person = teacher.Person;
            //studentCategory.StudentCategory = new StudentCategory() { Id = 2 };
            //teacher.StudentCategories.Add(studentCategory);

            ////teacher availability
            //teacher.TeacherAvailabilities = new List<TeacherAvailability>();
            //TeacherAvailability teacherAvailability = new TeacherAvailability();
            //teacherAvailability.Person = teacher.Person;
            //teacherAvailability.WeekDay = new WeekDay() { Id = 3 };
            //teacherAvailability.Period = new Period() { Id = 2 };
            //teacher.TeacherAvailabilities.Add(teacherAvailability);

            ////teacher award
            //teacher.TeacherAwards = new List<TeacherAward>();
            //TeacherAward teacherAward = new TeacherAward();
            //teacherAward.Person = teacher.Person;
            //teacherAward.AwardBody = "UNICEF";
            //teacherAward.AwardName = "Best Philantrophist";
            //teacherAward.YearAwarded = 2000;
            //teacher.TeacherAwards.Add(teacherAward);

            ////teacher olevel result
            //teacher.OLevelResults = new List<TeacherOLevelResult>();
            //TeacherOLevelResult teacherOLevelResult = new TeacherOLevelResult();
            //teacherOLevelResult.Person = teacher.Person;
            //teacherOLevelResult.ExamNumber = "9098878";
            //teacherOLevelResult.ExamYear = 1995;
            //teacherOLevelResult.Sitting = new OLevelExamSitting() { Id = 1 };
            //teacherOLevelResult.Type = new OLevelType() { Id = 2 };

            //teacherOLevelResult.OLevelResultDetails = new List<TeacherOLevelResultDetail>();
            //TeacherOLevelResultDetail teacherOLevelResultDetail = new TeacherOLevelResultDetail();
            //teacherOLevelResultDetail.Result = teacherOLevelResult;
            //teacherOLevelResultDetail.Subject = new OLevelSubject() { Id = 1 };
            //teacherOLevelResultDetail.Grade = new OLevelGrade() { Id = 1 };
            //teacherOLevelResult.OLevelResultDetails.Add(teacherOLevelResultDetail);

            //TeacherOLevelResultDetail teacherOLevelResultDetail2 = new TeacherOLevelResultDetail();
            //teacherOLevelResultDetail2.Result = teacherOLevelResult;
            //teacherOLevelResultDetail2.Subject = new OLevelSubject() { Id = 2 };
            //teacherOLevelResultDetail2.Grade = new OLevelGrade() { Id = 3 };
            //teacherOLevelResult.OLevelResultDetails.Add(teacherOLevelResultDetail2);

            //TeacherOLevelResultDetail teacherOLevelResultDetail3 = new TeacherOLevelResultDetail();
            //teacherOLevelResultDetail3.Result = teacherOLevelResult;
            //teacherOLevelResultDetail3.Subject = new OLevelSubject() { Id = 3 };
            //teacherOLevelResultDetail3.Grade = new OLevelGrade() { Id = 1 };
            //teacherOLevelResult.OLevelResultDetails.Add(teacherOLevelResultDetail3);

            //TeacherOLevelResultDetail teacherOLevelResultDetail4 = new TeacherOLevelResultDetail();
            //teacherOLevelResultDetail4.Result = teacherOLevelResult;
            //teacherOLevelResultDetail4.Subject = new OLevelSubject() { Id = 4 };
            //teacherOLevelResultDetail4.Grade = new OLevelGrade() { Id = 4 };
            //teacherOLevelResult.OLevelResultDetails.Add(teacherOLevelResultDetail4);

            //teacher.OLevelResults.Add(teacherOLevelResult);

            //Teacher newTeacher = service.Save(teacher);

            //Assert.IsNotNull(newTeacher);
            //Assert.IsTrue(newTeacher.Person.Id > 0);

        }



    }
}
