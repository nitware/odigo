using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Odigo.Model.Model;

namespace Odigo.Web.Areas.Transaction.Models
{
    public class PaymentViewModel
    {
        public PaymentViewModel()
        {
            //Person = new Person();
            //ServiceCharge = new ServiceCharge();
            PaymentSlip = new PaymentSlip();
        }

        //public Person Person { get; set; }
        public PaymentSlip PaymentSlip { get; set; }
        //public ServiceCharge ServiceCharge { get; set; }
    }



}