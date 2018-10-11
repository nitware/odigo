using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Odigo.Model.Model;
using System.Web.Mvc;

namespace Odigo.Web.Models
{
    public class BaseSearchViewModel
    {
        public BaseSearchViewModel()
        {
            State = new State();
            TeacherType = new TeacherType();
            Qualification = new QualificationCategory();
            StudentCategory = new StudentCategory();
        }

        public State State { get; set; }
        public TeacherType TeacherType { get; set; }
        public QualificationCategory Qualification { get; set; }
        public StudentCategory StudentCategory { get; set; }
        public bool DropdownDataLoaded { get; set; }

        public List<State> States { get; set; }
        public List<TeacherType> TeacherTypes { get; set; }
        public List<QualificationCategory> Qualifications { get; set; }
        public List<StudentCategory> StudentCategories { get; set; }
                
        public List<Teacher> Teachers { get; set; }

        public List<SelectListItem> TeacherTypeSelectList { get; set; }
        public List<SelectListItem> QualificationSelectList { get; set; }
        public List<SelectListItem> StudentCategorySelectList { get; set; }
        public List<SelectListItem> StateSelectList { get; set; }
    }




}