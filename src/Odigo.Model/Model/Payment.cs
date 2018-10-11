using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odigo.Model.Model
{
    public class Payment
    {
        public long Id { get; set; }
        public Person Person { get; set; }
        public PaymentMode Mode { get; set; }
        public ServiceCharge ServiceCharge { get; set; }
        public long? SerialNumber { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime DateEntered { get; set; }
        public bool Paid { get; set; }
        public DateTime? DatePaid { get; set; }
    }
}
