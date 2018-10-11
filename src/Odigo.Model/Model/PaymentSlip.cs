using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odigo.Model.Model
{
    public class PaymentSlip
    {
        public string IssuerLogoUrl { get; set; }
        public string IssuerName { get; set; }
        public string IssuerAddress { get; set; }
        public string IssuerEmail { get; set; }
        public string IssuerPhone { get; set; }
        
        public string Message { get; set; }
        public string Disclaimer { get; set; }

        public Payment Payment { get; set; }


        //public bool Paid { get; set; }

        //public string RecipientName { get; set; }
        //public string RecipientAddress { get; set; }
        //public string RecipientEmail { get; set; }
        //public string RecipientPhone { get; set; }

        //[Display(Name = "Invoice No")]
        //public string Number { get; set; }

        //[Display(Name = "Date Paid")]
        //public DateTime? Date { get; set; }

        //[Display(Name = "Payment Channel")]
        //public string Channel { get; set; }


    }



}
