using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Odigo.Model.Model;

namespace Odigo.Web.Areas.Employer.Models
{
    public class RequestViewModel
    {
        public List<Period> Periods { get; set; }
        public List<WeekDay> WeekDays { get; set; }
        public List<TeacherAvailability> TeacherAvailabilities { get; set; }
        public List<TeacherStudentCategory> TeacherStudentCategories { get; set; }
        public List<EmployerStudentCategory> EmployerStudentCategories { get; set; }
        public List<RequestForEmploymentCostImplication> RequestCostImplications { get; set; }
        public ServiceCharge ServiceCharge { get; set; }
        public Model.Model.Teacher Teacher { get; set; }
        public Person Employer { get; set; }


        //public List<TeacherAvailability> ExtraTeacherAvailabilities { get; set; }
    }





}