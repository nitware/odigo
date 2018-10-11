using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odigo.Model.Model
{
    public class Request
    {
        public long Id { get; set; }
        public Person FromPerson { get; set; }
        public Person ToPerson { get; set; }
        public Message RequestMessage { get; set; }
        public Message ReplyMessage { get; set; }
        public ServiceCharge ServiceCharge { get; set; }
        public RequestStatus Status { get; set; }
        public DateTime Date { get; set; }
        public DateTime? ReplyDate { get; set; }
        
        public List<RequestForEmploymentCostImplication> ForEmploymentCostImplications { get; set; }


    }
}
