using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Odigo.Model.Model;
using System.Web.Mvc;

namespace Odigo.Web.Areas.Common.Models
{
    public class ServiceCostViewModel
    {
        public ServiceCostViewModel()
        {
            PersonType = new PersonType();
        }

        public List<TeacherAvailability> TeacherAvailabilities { get; set; }
        public List<StudentCategory> StudentCategories { get; set; }
        public List<QualificationCategory> QualificationCategories { get; set; }
        public List<TeachingCost> ExistingTeachingCosts { get; set; }
        public List<TeachingCost> TeachingCosts { get; set; }
        public List<WeekDay> WeekDays { get; set; }
        public List<Period> Periods { get; set; }

        public List<PersonType> PersonTypes { get; set; }
        public List<PersonType> Subscribers { get; set; }
        public List<ServiceCharge> ServiceCharges { get; set; }
        public PersonType PersonType { get; set; }
        public List<SelectListItem> PersonTypeSelectList { get; set; }
    }


}