using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odigo.Model.Model
{
    public class Employer
    {
        public Person Person { get; set; }
        //public EmployerType Type { get; set; }

        [Required]
        [Display(Name = "School Name")]
        public string Name { get; set; }

        [Url]
        public string Website { get; set; }
        public Sex Sex { get; set; }

        public LoginDetail LoginDetail { get; set; }
        public List<EmployerStudentCategory> StudentCategories { get; set; }
        public List<Payment> Payments { get; set; }
        public PaymentSlip PaymentSlip { get; set; }

    }


}
