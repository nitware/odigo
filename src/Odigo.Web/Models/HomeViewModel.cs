using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Odigo.Model.Model;
using System.Web.Mvc;

namespace Odigo.Web.Models
{
    public class HomeViewModel //: BaseSearchViewModel
    {
        //public State State { get; set; }
        //public TeacherType TeacherType { get; set; }
        //public Qualification Qualification { get; set; }
        //public StudentCategory StudentCategory { get; set; }

        //public List<State> States { get; set; }
        //public List<TeacherType> TeacherTypes { get; set; }
        //public List<QualificationCategory> Qualifications { get; set; }
        //public List<StudentCategory> StudentCategories { get; set; }

        public List<News> News { get; set; }
        public List<QuickLink> QuickLinks { get; set; }
        
        public BaseSearchViewModel BaseSearchViewModel { get; set; }


        //public List<Teacher> Teachers { get; set; }

        //public List<SelectListItem> TeacherTypeSelectList { get; set; }
        //public List<SelectListItem> QualificationSelectList { get; set; }
        //public List<SelectListItem> StudentCategorySelectList { get; set; }
        //public List<SelectListItem> StateSelectList { get; set; }

    }






}