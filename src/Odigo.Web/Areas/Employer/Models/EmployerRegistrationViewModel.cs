using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Odigo.Model.Model;
using System.Web.Mvc;

namespace Odigo.Web.Areas.Employer.Models
{
    public class EmployerRegistrationViewModel
    {
        public EmployerRegistrationViewModel()
        {
            Employer = new Model.Model.Employer();
            Employer.Person = new Person();
            Employer.Person.Lga = new Lga();
            Employer.Person.State = new State();
            Employer.Person.Country = new Country();
            Employer.Person.Type = new PersonType();

            Employer.LoginDetail = new LoginDetail();
            Employer.LoginDetail.SecurityQuestion = new SecurityQuestion();
            Employer.LoginDetail.Role = new Role();

            Employer.Sex = new Sex();

            Periods = new List<Period>();
            WeekDays = new List<WeekDay>();
            StudentCategories = new List<StudentCategory>();

            InitialiseEmployerStudentCategory();
        }

        public bool PersonAlreadyExist { get; set; }
        public Model.Model.Employer Employer { get; set; }

        public List<WeekDay> WeekDays { get; set; }
        public List<Period> Periods { get; set; }
        public List<StudentCategory> StudentCategories { get; set; }
        public List<SecurityQuestion> SecurityQuestions { get; set; }

        public List<Sex> Sexs { get; set; }
        public List<State> States { get; set; }
        public List<TeacherType> TeacherTypes { get; set; }
        public List<EmployerStudentCategory> EmployerStudentCategories { get; set; }
        public List<EmployerStudentCategory> SelectedEmployerStudentCategories { get; set; }
        public List<EmployerStudentCategory> LoadedEmployerStudentCategories { get; set; }
        public List<Value> NoOfStudents { get; set; }

        public List<SelectListItem> SexSelectList { get; set; }
        public List<SelectListItem> StateSelectList { get; set; }
        public List<SelectListItem> CountrySelectList { get; set; }
        public List<SelectListItem> SecurityQuestionSelectList { get; set; }
        public List<SelectListItem> TeacherTypeSelectList { get; set; }
        public List<SelectListItem> NoOfStudentSelectList { get; set; }

        public void InitialiseEmployerStudentCategory()
        {
            try
            {
                List<EmployerStudentCategory> employerStudentCategories = new List<EmployerStudentCategory>();
                EmployerStudentCategory employerStudentCategory1 = new EmployerStudentCategory() { StudentCategory = new StudentCategory() { Id = 1 }, TeacherType = new TeacherType() { Id = 0 }, NoOfStudent = 0 };
                EmployerStudentCategory employerStudentCategory2 = new EmployerStudentCategory() { StudentCategory = new StudentCategory() { Id = 2 }, TeacherType = new TeacherType() { Id = 0 }, NoOfStudent = 0 };
                EmployerStudentCategory employerStudentCategory3 = new EmployerStudentCategory() { StudentCategory = new StudentCategory() { Id = 3 }, TeacherType = new TeacherType() { Id = 0 }, NoOfStudent = 0 };
                EmployerStudentCategory employerStudentCategory4 = new EmployerStudentCategory() { StudentCategory = new StudentCategory() { Id = 4 }, TeacherType = new TeacherType() { Id = 0 }, NoOfStudent = 0 };

                EmployerStudentCategories = new List<EmployerStudentCategory>();
                EmployerStudentCategories.Add(employerStudentCategory1);
                EmployerStudentCategories.Add(employerStudentCategory2);
                EmployerStudentCategories.Add(employerStudentCategory3);
                EmployerStudentCategories.Add(employerStudentCategory4);
            }
            catch (Exception)
            {
                throw;
            }
        }



    }


}