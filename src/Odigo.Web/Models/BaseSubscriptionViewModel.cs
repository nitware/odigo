using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Odigo.Model.Model;

namespace Odigo.Web.Models
{
    public abstract class BaseSubscriptionViewModel
    {
        public BaseSubscriptionViewModel()
        {
            Teacher = new Teacher();
            Teacher.Person = new Person();
            Teacher.Person.Lga = new Lga();
            Teacher.Person.State = new State();
            Teacher.Person.Country = new Country();

            Teacher.LoginDetail = new LoginDetail();
            Teacher.LoginDetail.SecurityQuestion = new SecurityQuestion();
            Teacher.LoginDetail.Role = new Role();

            Teacher.Type = new TeacherType();
            Teacher.YearOfBirth = new Value();
            Teacher.MonthOfBirth = new Value();
            Teacher.DayOfBirth = new Value();
            Teacher.Sex = new Sex();
        }
        
        //public Person Person { get; set; }
        public Sex Sex { get; set; }
        public State State { get; set; }
        public Country Country { get; set; }
        public StudentCategory StudentCategory { get; set; }
        public bool PersonAlreadyExist { get; set; }
        public Teacher Teacher { get; set; }


        //public ApplicationForm ApplicationForm { get; set; }
        //public Model.Model.Applicant Applicant { get; set; }
        //public NextOfKin ApplicantSponsor { get; set; }
        //public OLevelResult ApplicantFirstSittingOLevelResult { get; set; }
        //public OLevelResult ApplicantSecondSittingOLevelResult { get; set; }
        //public PreviousEducation ApplicantPreviousEducation { get; set; }
        //public AppliedCourse AppliedCourse { get; set; }
        //public ApplicantJambDetail ApplicantJambDetail { get; set; }
        //public List<OLevelResultDetail> ApplicantFirstSittingOLevelResultDetails { get; set; }
        //public List<OLevelResultDetail> ApplicantSecondSittingOLevelResultDetails { get; set; }

        //public Student Student { get; set; }
        //public NextOfKin Sponsor { get; set; }
        //public StudentAcademicInformation StudentAcademicInformation { get; set; }
        //public StudentSponsor StudentSponsor { get; set; }
        //public StudentFinanceInformation StudentFinanceInformation { get; set; }
        //public StudentNdResult StudentNdResult { get; set; }
        //public StudentEmploymentInformation StudentEmploymentInformation { get; set; }
        //public OLevelResult FirstSittingOLevelResult { get; set; }
        //public OLevelResult SecondSittingOLevelResult { get; set; }
        //public OLevelResultDetail FirstSittingOLevelResultDetail { get; set; }
        //public OLevelResultDetail SecondSittingOLevelResultDetail { get; set; }
        //public List<OLevelResultDetail> FirstSittingOLevelResultDetails { get; set; }
        //public List<OLevelResultDetail> SecondSittingOLevelResultDetails { get; set; }

        //public void InitialiseOLevelResult()
        //{
        //    try
        //    {
        //        List<OLevelResultDetail> oLevelResultDetails = new List<OLevelResultDetail>();
        //        OLevelResultDetail oLevelResultDetail1 = new OLevelResultDetail() { Grade = new OLevelGrade() { Id = 0 }, Subject = new OLevelSubject() { Id = 0 } };
        //        OLevelResultDetail oLevelResultDetail2 = new OLevelResultDetail() { Grade = new OLevelGrade() { Id = 0 }, Subject = new OLevelSubject() { Id = 0 } };
        //        OLevelResultDetail oLevelResultDetail3 = new OLevelResultDetail() { Grade = new OLevelGrade() { Id = 0 }, Subject = new OLevelSubject() { Id = 0 } };
        //        OLevelResultDetail oLevelResultDetail4 = new OLevelResultDetail() { Grade = new OLevelGrade() { Id = 0 }, Subject = new OLevelSubject() { Id = 0 } };
        //        OLevelResultDetail oLevelResultDetail5 = new OLevelResultDetail() { Grade = new OLevelGrade() { Id = 0 }, Subject = new OLevelSubject() { Id = 0 } };
        //        OLevelResultDetail oLevelResultDetail6 = new OLevelResultDetail() { Grade = new OLevelGrade() { Id = 0 }, Subject = new OLevelSubject() { Id = 0 } };
        //        OLevelResultDetail oLevelResultDetail7 = new OLevelResultDetail() { Grade = new OLevelGrade() { Id = 0 }, Subject = new OLevelSubject() { Id = 0 } };
        //        OLevelResultDetail oLevelResultDetail8 = new OLevelResultDetail() { Grade = new OLevelGrade() { Id = 0 }, Subject = new OLevelSubject() { Id = 0 } };
        //        OLevelResultDetail oLevelResultDetail9 = new OLevelResultDetail() { Grade = new OLevelGrade() { Id = 0 }, Subject = new OLevelSubject() { Id = 0 } };

        //        OLevelResultDetail oLevelResultDetail11 = new OLevelResultDetail() { Grade = new OLevelGrade() { Id = 0 }, Subject = new OLevelSubject() { Id = 0 } };
        //        OLevelResultDetail oLevelResultDetail22 = new OLevelResultDetail() { Grade = new OLevelGrade() { Id = 0 }, Subject = new OLevelSubject() { Id = 0 } };
        //        OLevelResultDetail oLevelResultDetail33 = new OLevelResultDetail() { Grade = new OLevelGrade() { Id = 0 }, Subject = new OLevelSubject() { Id = 0 } };
        //        OLevelResultDetail oLevelResultDetail44 = new OLevelResultDetail() { Grade = new OLevelGrade() { Id = 0 }, Subject = new OLevelSubject() { Id = 0 } };
        //        OLevelResultDetail oLevelResultDetail55 = new OLevelResultDetail() { Grade = new OLevelGrade() { Id = 0 }, Subject = new OLevelSubject() { Id = 0 } };
        //        OLevelResultDetail oLevelResultDetail66 = new OLevelResultDetail() { Grade = new OLevelGrade() { Id = 0 }, Subject = new OLevelSubject() { Id = 0 } };
        //        OLevelResultDetail oLevelResultDetail77 = new OLevelResultDetail() { Grade = new OLevelGrade() { Id = 0 }, Subject = new OLevelSubject() { Id = 0 } };
        //        OLevelResultDetail oLevelResultDetail88 = new OLevelResultDetail() { Grade = new OLevelGrade() { Id = 0 }, Subject = new OLevelSubject() { Id = 0 } };
        //        OLevelResultDetail oLevelResultDetail99 = new OLevelResultDetail() { Grade = new OLevelGrade() { Id = 0 }, Subject = new OLevelSubject() { Id = 0 } };

        //        FirstSittingOLevelResultDetails = new List<OLevelResultDetail>();
        //        FirstSittingOLevelResultDetails.Add(oLevelResultDetail1);
        //        FirstSittingOLevelResultDetails.Add(oLevelResultDetail2);
        //        FirstSittingOLevelResultDetails.Add(oLevelResultDetail3);
        //        FirstSittingOLevelResultDetails.Add(oLevelResultDetail4);
        //        FirstSittingOLevelResultDetails.Add(oLevelResultDetail5);
        //        FirstSittingOLevelResultDetails.Add(oLevelResultDetail6);
        //        FirstSittingOLevelResultDetails.Add(oLevelResultDetail7);
        //        FirstSittingOLevelResultDetails.Add(oLevelResultDetail8);
        //        FirstSittingOLevelResultDetails.Add(oLevelResultDetail9);

        //        SecondSittingOLevelResultDetails = new List<OLevelResultDetail>();
        //        SecondSittingOLevelResultDetails.Add(oLevelResultDetail11);
        //        SecondSittingOLevelResultDetails.Add(oLevelResultDetail22);
        //        SecondSittingOLevelResultDetails.Add(oLevelResultDetail33);
        //        SecondSittingOLevelResultDetails.Add(oLevelResultDetail44);
        //        SecondSittingOLevelResultDetails.Add(oLevelResultDetail55);
        //        SecondSittingOLevelResultDetails.Add(oLevelResultDetail66);
        //        SecondSittingOLevelResultDetails.Add(oLevelResultDetail77);
        //        SecondSittingOLevelResultDetails.Add(oLevelResultDetail88);
        //        SecondSittingOLevelResultDetails.Add(oLevelResultDetail99);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public void LoadApplicantionFormBy(long id)
        //{
        //    try
        //    {
        //        ApplicationForm = GetApplicationFormBy(id);
        //        if (ApplicationForm != null && ApplicationForm.Id > 0)
        //        {
        //            PersonLogic personLogic = new PersonLogic();
        //            NextOfKinLogic nextOfKinLogic = new NextOfKinLogic();
        //            OLevelResultLogic oLevelResultLogic = new OLevelResultLogic();
        //            OLevelResultDetailLogic oLevelResultDetailLogic = new OLevelResultDetailLogic();
        //            PreviousEducationLogic previousEducationLogic = new PreviousEducationLogic();
        //            ApplicantJambDetailLogic studentJambDetailLogic = new ApplicantJambDetailLogic();
        //            ApplicantLogic applicantLogic = new ApplicantLogic();

        //            Person person = ApplicationForm.Person;
        //            Person = personLogic.GetModelBy(p => p.Person_Id == person.Id);
        //            Applicant = applicantLogic.GetModelBy(p => p.Person_Id == person.Id);
        //            ApplicantSponsor = nextOfKinLogic.GetModelBy(p => p.Person_Id == person.Id);
        //            ApplicantFirstSittingOLevelResult = oLevelResultLogic.GetModelBy(p => p.Person_Id == person.Id && p.O_Level_Exam_Sitting_Id == 1);
        //            ApplicantSecondSittingOLevelResult = oLevelResultLogic.GetModelBy(p => p.Person_Id == person.Id && p.O_Level_Exam_Sitting_Id == 2);
        //            ApplicantPreviousEducation = previousEducationLogic.GetModelBy(p => p.Person_Id == person.Id);
        //            ApplicantJambDetail = studentJambDetailLogic.GetModelBy(p => p.Person_Id == person.Id);
        //            AppliedCourse = appliedCourseLogic.GetModelBy(a => a.Person_Id == person.Id);

        //            if (ApplicantFirstSittingOLevelResult != null && ApplicantFirstSittingOLevelResult.Id > 0)
        //            {
        //                ApplicantFirstSittingOLevelResultDetails = oLevelResultDetailLogic.GetModelsBy(p => p.O_Level_Result_Id == ApplicantFirstSittingOLevelResult.Id);
        //            }

        //            if (ApplicantSecondSittingOLevelResult != null && ApplicantSecondSittingOLevelResult.Id > 0)
        //            {
        //                ApplicantSecondSittingOLevelResultDetails = oLevelResultDetailLogic.GetModelsBy(p => p.O_Level_Result_Id == ApplicantSecondSittingOLevelResult.Id);
        //            }

        //            SetApplicant(Applicant);
        //            SetApplicantBioData(Person);
        //            SetApplicantSponsor(ApplicantSponsor);
        //            SetApplicantFirstSittingOLevelResult(ApplicantFirstSittingOLevelResult, ApplicantFirstSittingOLevelResultDetails);
        //            SetApplicantSecondSittingOLevelResult(ApplicantSecondSittingOLevelResult, ApplicantSecondSittingOLevelResultDetails);
        //            SetApplicantPreviousEducation(ApplicantPreviousEducation);
        //            SetAppliedCourse(AppliedCourse);
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public void LoadStudentInformationFormBy(long personId)
        //{
        //    try
        //    {
        //        if (personId > 0)
        //        {
        //            StudentSponsorLogic sponsorLogic = new StudentSponsorLogic();
        //            StudentAcademicInformationLogic academicInformationLogic = new StudentAcademicInformationLogic();
        //            StudentFinanceInformationLogic financeInformationLogic = new StudentFinanceInformationLogic();
        //            StudentEmploymentInformationLogic employmentInformationLogic = new StudentEmploymentInformationLogic();
        //            StudentNdResultLogic ndResultLogic = new StudentNdResultLogic();


        //            Student student = GetStudentBy(personId);
        //            StudentSponsor studentNextofKin = sponsorLogic.GetModelBy(p => p.Person_Id == personId);
        //            StudentAcademicInformation studentAcademicInformation = academicInformationLogic.GetModelBy(p => p.Person_Id == personId);
        //            StudentFinanceInformation studentFinanceInformation = financeInformationLogic.GetModelBy(p => p.Person_Id == personId);
        //            StudentEmploymentInformation studentEmploymentInformation = employmentInformationLogic.GetModelBy(p => p.Person_Id == personId);
        //            StudentNdResult studentNdResult = ndResultLogic.GetModelBy(p => p.Person_Id == personId);


        //            SetStudent(student);
        //            SetStudentSponsor(studentNextofKin);
        //            SetStudentAcademicInformation(studentAcademicInformation);
        //            SetStudentFinanceInformation(studentFinanceInformation);
        //            SetStudentEmploymentInformation(studentEmploymentInformation);
        //            SetStudentNdResult(studentNdResult);
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public Model.Model.Student GetStudentBy(long personId)
        //{
        //    try
        //    {
        //        StudentLogic studentLogic = new StudentLogic();
        //        return studentLogic.GetModelBy(p => p.Person_Id == personId);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public ApplicationForm GetApplicationFormBy(long id)
        //{
        //    try
        //    {
        //        ApplicationFormLogic applicationFormLogic = new ApplicationFormLogic();
        //        return applicationFormLogic.GetModelBy(a => a.Application_Form_Id == id);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public void SetApplicantPreviousEducation(PreviousEducation previousEducation)
        //{
        //    try
        //    {
        //        if (previousEducation != null && previousEducation.Id > 0)
        //        {
        //            ApplicantPreviousEducation.SchoolName = previousEducation.SchoolName;
        //            ApplicantPreviousEducation.Course = previousEducation.Course;

        //            if (previousEducation.Person != null && previousEducation.Person.Id > 0)
        //            {
        //                ApplicantPreviousEducation.Person = previousEducation.Person;
        //            }
        //            else
        //            {
        //                ApplicantPreviousEducation.Person = new Person();
        //            }

        //            if (previousEducation.Qualification != null && previousEducation.Qualification.Id > 0)
        //            {
        //                ApplicantPreviousEducation.Qualification = previousEducation.Qualification;
        //            }
        //            else
        //            {
        //                ApplicantPreviousEducation.Qualification = new EducationalQualification();
        //            }

        //            if (previousEducation.ResultGrade != null && previousEducation.ResultGrade.Id > 0)
        //            {
        //                ApplicantPreviousEducation.ResultGrade = previousEducation.ResultGrade;
        //            }
        //            else
        //            {
        //                ApplicantPreviousEducation.ResultGrade = new ResultGrade();
        //            }

        //            if (previousEducation.ITDuration != null && previousEducation.ITDuration.Id > 0)
        //            {
        //                ApplicantPreviousEducation.ITDuration = previousEducation.ITDuration;
        //            }
        //            else
        //            {
        //                ApplicantPreviousEducation.ITDuration = new ITDuration();
        //            }

        //            if (previousEducation.StartDate != null)
        //            {
        //                ApplicantPreviousEducation.StartDay = previousEducation.StartDay;
        //                ApplicantPreviousEducation.StartMonth = previousEducation.StartMonth;
        //                ApplicantPreviousEducation.StartYear = previousEducation.StartYear;
        //            }
        //            else
        //            {
        //                ApplicantPreviousEducation.StartDay = new Value();
        //                ApplicantPreviousEducation.StartMonth = new Value();
        //                ApplicantPreviousEducation.StartYear = new Value();
        //            }

        //            if (previousEducation.EndDate != null)
        //            {
        //                ApplicantPreviousEducation.EndDay = previousEducation.EndDay;
        //                ApplicantPreviousEducation.EndMonth = previousEducation.EndMonth;
        //                ApplicantPreviousEducation.EndYear = previousEducation.EndYear;
        //            }
        //            else
        //            {
        //                ApplicantPreviousEducation.EndDay = new Value();
        //                ApplicantPreviousEducation.EndMonth = new Value();
        //                ApplicantPreviousEducation.EndYear = new Value();
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public void SetApplicantBioData(Person person)
        //{
        //    try
        //    {
        //        if (person != null && person.Id > 0)
        //        {
        //            Person.DateOfBirth = person.DateOfBirth;
        //            Person.HomeTown = person.HomeTown;
        //            Person.HomeAddress = person.HomeAddress;
        //            Person.ImageFileUrl = person.ImageFileUrl;

        //            if (person.State != null && !string.IsNullOrEmpty(person.State.Id))
        //            {
        //                Person.State = person.State;
        //            }
        //            else
        //            {
        //                Person.State = new State();
        //            }

        //            if (person.Sex != null && person.Sex.Id > 0)
        //            {
        //                Person.Sex = person.Sex;
        //            }
        //            else
        //            {
        //                Person.Sex = new Sex();
        //            }

        //            if (person.LocalGovernment != null && person.LocalGovernment.Id > 0)
        //            {
        //                Person.LocalGovernment = person.LocalGovernment;
        //            }
        //            else
        //            {
        //                Person.LocalGovernment = new LocalGovernment();
        //            }

        //            if (person.Religion != null && person.Religion.Id > 0)
        //            {
        //                Person.Religion = person.Religion;
        //            }
        //            else
        //            {
        //                Person.Religion = new Religion();
        //            }

        //            if (person.DateOfBirth.HasValue)
        //            {
        //                Person.DayOfBirth = person.DayOfBirth;
        //                Person.MonthOfBirth = person.MonthOfBirth;
        //                Person.YearOfBirth = person.YearOfBirth;
        //            }
        //            else
        //            {
        //                Person.DayOfBirth = new Value();
        //                Person.MonthOfBirth = new Value();
        //                Person.YearOfBirth = new Value();
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public void SetApplicant(Model.Model.Applicant applicant)
        //{
        //    try
        //    {
        //        if (applicant != null)
        //        {
        //            Applicant.ExtraCurricullarActivities = applicant.ExtraCurricullarActivities;
        //            Applicant.OtherAbility = applicant.OtherAbility;

        //            if (applicant.Ability != null)
        //            {
        //                Applicant.Ability = applicant.Ability;
        //            }
        //            else
        //            {
        //                Applicant.Ability = new Ability();
        //            }

        //            if (applicant.Status != null)
        //            {
        //                Applicant.Status = applicant.Status;
        //            }
        //            //else
        //            //{
        //            //    Applicant.Status = new Ability();
        //            //}
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public void SetStudent(Model.Model.Student student)
        //{
        //    try
        //    {
        //        if (student != null)
        //        {
        //            Student.MatricNumber = student.MatricNumber;
        //            Student.SchoolContactAddress = student.SchoolContactAddress;

        //            if (student.ApplicationForm != null)
        //            {
        //                Student.ApplicationForm = student.ApplicationForm;
        //            }
        //            else
        //            {
        //                Student.ApplicationForm = new ApplicationForm();
        //            }

        //            if (student.Category != null)
        //            {
        //                Student.Category = student.Category;
        //            }
        //            else
        //            {
        //                Student.Category = new StudentCategory();
        //            }

        //            if (student.Type != null)
        //            {
        //                Student.Type = student.Type;
        //            }
        //            else
        //            {
        //                Student.Type = new StudentType();
        //            }

        //            if (student.Status != null)
        //            {
        //                Student.Status = student.Status;
        //            }
        //            else
        //            {
        //                Student.Status = new StudentStatus();
        //            }

        //            if (student.Title != null)
        //            {
        //                Student.Title = student.Title;
        //            }
        //            else
        //            {
        //                Student.Title = new Title();
        //            }

        //            if (student.MaritalStatus != null)
        //            {
        //                Student.MaritalStatus = student.MaritalStatus;
        //            }
        //            else
        //            {
        //                Student.MaritalStatus = new MaritalStatus();
        //            }

        //            if (student.BloodGroup != null)
        //            {
        //                Student.BloodGroup = student.BloodGroup;
        //            }
        //            else
        //            {
        //                Student.BloodGroup = new BloodGroup();
        //            }

        //            if (student.Genotype != null)
        //            {
        //                Student.Genotype = student.Genotype;
        //            }
        //            else
        //            {
        //                Student.Genotype = new Genotype();
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public void SetAppliedCourse(AppliedCourse appliedCourse)
        //{
        //    try
        //    {
        //        if (appliedCourse != null)
        //        {
        //            if (appliedCourse.Department != null)
        //            {
        //                AppliedCourse.Department = appliedCourse.Department;
        //            }
        //            else
        //            {
        //                AppliedCourse.Department = new Department();
        //            }

        //            if (appliedCourse.Programme != null)
        //            {
        //                AppliedCourse.Programme = appliedCourse.Programme;
        //            }
        //            else
        //            {
        //                AppliedCourse.Programme = new Programme();
        //            }

        //            if (appliedCourse.Option != null)
        //            {
        //                AppliedCourse.Option = appliedCourse.Option;
        //            }
        //            else
        //            {
        //                AppliedCourse.Option = new DepartmentOption();
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public void SetApplicantSponsor(NextOfKin sponsor)
        //{
        //    try
        //    {
        //        if (sponsor != null)
        //        {
        //            Sponsor.Name = sponsor.Name;
        //            Sponsor.ContactAddress = sponsor.ContactAddress;
        //            Sponsor.MobilePhone = sponsor.MobilePhone;

        //            if (sponsor.Relationship != null)
        //            {
        //                Sponsor.Relationship = sponsor.Relationship;
        //            }
        //            else
        //            {
        //                Sponsor.Relationship = new Relationship();
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public void SetStudentSponsor(StudentSponsor sponsor)
        //{
        //    try
        //    {
        //        if (sponsor != null)
        //        {
        //            StudentSponsor.Name = sponsor.Name;
        //            StudentSponsor.ContactAddress = sponsor.ContactAddress;
        //            StudentSponsor.MobilePhone = sponsor.MobilePhone;
        //            StudentSponsor.Email = sponsor.Email;

        //            if (sponsor.Relationship != null)
        //            {
        //                StudentSponsor.Relationship = sponsor.Relationship;
        //            }
        //            else
        //            {
        //                StudentSponsor.Relationship = new Relationship();
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public void SetApplicantFirstSittingOLevelResult(OLevelResult oLevelResult, List<OLevelResultDetail> oLevelResultDetails)
        //{
        //    try
        //    {
        //        if (oLevelResult != null && oLevelResult.Id > 0)
        //        {
        //            if (oLevelResult.Type != null)
        //            {
        //                FirstSittingOLevelResult.Type = oLevelResult.Type;
        //            }
        //            else
        //            {
        //                FirstSittingOLevelResult.Type = new OLevelType();
        //            }

        //            FirstSittingOLevelResult.ExamNumber = oLevelResult.ExamNumber;
        //            FirstSittingOLevelResult.ExamYear = oLevelResult.ExamYear;

        //            if (oLevelResultDetails != null && oLevelResultDetails.Count > 0)
        //            {
        //                for (int i = 0; i < oLevelResultDetails.Count; i++)
        //                {
        //                    FirstSittingOLevelResultDetails[i].Subject = oLevelResultDetails[i].Subject;
        //                    FirstSittingOLevelResultDetails[i].Grade = oLevelResultDetails[i].Grade;
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public void SetApplicantSecondSittingOLevelResult(OLevelResult oLevelResult, List<OLevelResultDetail> oLevelResultDetails)
        //{
        //    try
        //    {
        //        if (oLevelResult != null && oLevelResult.Id > 0)
        //        {
        //            if (oLevelResult.Type != null)
        //            {
        //                SecondSittingOLevelResult.Type = oLevelResult.Type;
        //            }
        //            else
        //            {
        //                SecondSittingOLevelResult.Type = new OLevelType();
        //            }

        //            SecondSittingOLevelResult.ExamNumber = oLevelResult.ExamNumber;
        //            SecondSittingOLevelResult.ExamYear = oLevelResult.ExamYear;

        //            if (oLevelResultDetails != null && oLevelResultDetails.Count > 0)
        //            {
        //                for (int i = 0; i < oLevelResultDetails.Count; i++)
        //                {
        //                    if (oLevelResultDetails[i].Subject != null)
        //                    {
        //                        SecondSittingOLevelResultDetails[i].Subject = oLevelResultDetails[i].Subject;
        //                    }
        //                    else
        //                    {
        //                        SecondSittingOLevelResultDetails[i].Subject = new OLevelSubject();
        //                    }
        //                    if (oLevelResultDetails[i].Grade != null)
        //                    {
        //                        SecondSittingOLevelResultDetails[i].Grade = oLevelResultDetails[i].Grade;
        //                    }
        //                    else
        //                    {
        //                        SecondSittingOLevelResultDetails[i].Grade = new OLevelGrade();
        //                    }

        //                }
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public void SetStudentAcademicInformation(StudentAcademicInformation academicInformation)
        //{
        //    try
        //    {
        //        if (academicInformation != null)
        //        {
        //            StudentAcademicInformation.YearOfAdmission = academicInformation.YearOfAdmission;
        //            StudentAcademicInformation.YearOfGraduation = academicInformation.YearOfGraduation;

        //            if (academicInformation.ModeOfEntry != null)
        //            {
        //                StudentAcademicInformation.ModeOfEntry = academicInformation.ModeOfEntry;
        //            }
        //            else
        //            {
        //                StudentAcademicInformation.ModeOfEntry = new ModeOfEntry();
        //            }

        //            if (academicInformation.ModeOfStudy != null)
        //            {
        //                StudentAcademicInformation.ModeOfStudy = academicInformation.ModeOfStudy;
        //            }
        //            else
        //            {
        //                StudentAcademicInformation.ModeOfStudy = new ModeOfStudy();
        //            }

        //            if (academicInformation.Level != null)
        //            {
        //                StudentAcademicInformation.Level = academicInformation.Level;
        //            }
        //            else
        //            {
        //                StudentAcademicInformation.Level = new Level();
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public void SetStudentFinanceInformation(StudentFinanceInformation financeInformation)
        //{
        //    try
        //    {
        //        if (financeInformation != null)
        //        {
        //            StudentFinanceInformation.ScholarshipTitle = financeInformation.ScholarshipTitle;

        //            if (financeInformation.Mode != null)
        //            {
        //                StudentFinanceInformation.Mode = financeInformation.Mode;
        //            }
        //            else
        //            {
        //                StudentFinanceInformation.Mode = new ModeOfFinance();
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public void SetStudentEmploymentInformation(StudentEmploymentInformation employmentInformation)
        //{
        //    try
        //    {
        //        if (employmentInformation != null && employmentInformation.Id > 0)
        //        {
        //            StudentEmploymentInformation.PlaceOfLastEmployment = employmentInformation.PlaceOfLastEmployment;

        //            if (employmentInformation.Student != null)
        //            {
        //                StudentEmploymentInformation.Student = employmentInformation.Student;
        //            }
        //            else
        //            {
        //                StudentEmploymentInformation.Student = new Model.Model.Student();
        //            }

        //            if (employmentInformation.StartDate != null)
        //            {
        //                StudentEmploymentInformation.StartDay = employmentInformation.StartDay;
        //                StudentEmploymentInformation.StartMonth = employmentInformation.StartMonth;
        //                StudentEmploymentInformation.StartYear = employmentInformation.StartYear;
        //            }
        //            else
        //            {
        //                StudentEmploymentInformation.StartDay = new Value();
        //                StudentEmploymentInformation.StartMonth = new Value();
        //                StudentEmploymentInformation.StartYear = new Value();
        //            }

        //            if (employmentInformation.EndDate != null)
        //            {
        //                StudentEmploymentInformation.EndDay = employmentInformation.EndDay;
        //                StudentEmploymentInformation.EndMonth = employmentInformation.EndMonth;
        //                StudentEmploymentInformation.EndYear = employmentInformation.EndYear;
        //            }
        //            else
        //            {
        //                StudentEmploymentInformation.EndDay = new Value();
        //                StudentEmploymentInformation.EndMonth = new Value();
        //                StudentEmploymentInformation.EndYear = new Value();
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public void SetStudentNdResult(StudentNdResult ndResult)
        //{
        //    try
        //    {
        //        if (ndResult != null)
        //        {
        //            if (ndResult.Student != null)
        //            {
        //                StudentNdResult.Student = ndResult.Student;
        //            }
        //            else
        //            {
        //                StudentNdResult.Student = new Model.Model.Student();
        //            }

        //            if (ndResult.DateAwarded != null)
        //            {
        //                StudentNdResult.DayAwarded = ndResult.DayAwarded;
        //                StudentNdResult.MonthAwarded = ndResult.MonthAwarded;
        //                StudentNdResult.YearAwarded = ndResult.YearAwarded;
        //            }
        //            else
        //            {
        //                StudentNdResult.DayAwarded = new Value();
        //                StudentNdResult.MonthAwarded = new Value();
        //                StudentNdResult.YearAwarded = new Value();
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}




    }
}