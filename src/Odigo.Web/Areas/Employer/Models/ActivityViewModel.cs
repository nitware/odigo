using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Odigo.Web.Areas.Employer.Models
{
    public class ActivityViewModel
    {
        public ActivityViewModel()
        {
            RequestViewModel = new RequestViewModel();
        }

        public int PageId { get; set; }

        public long TeacherId { get; set; }
        public long EmployerId { get; set; }

        public RequestViewModel RequestViewModel { get; set; }


    }





}