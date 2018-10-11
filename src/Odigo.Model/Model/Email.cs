using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odigo.Model.Model
{
    public class Email
    {
        public DateTime MailDate { get; set; }
        public string Addressee { get; set; }
        public string Salutation { get; set; }
        public string MailTitle { get; set; }
        public string MailBody { get; set; }
        public string NameFrom { get; set; }
        public string Subject { get; set; }
        public string Phone { get; set; }
        public string MailTo { get; set; }
    }


}
