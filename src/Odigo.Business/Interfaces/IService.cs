using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Odigo.Model.Model;

namespace Odigo.Business.Interfaces
{
    public interface IService
    {
        ServiceCharge GetChargesBy(ServiceCharge serviceCharge);
        ServiceCharge GetRequestCostBy(QualificationCategory qualification);
        ServiceCharge GetChargesBy(ServiceCharge.Services serviceCharge);
        ServiceCharge GetChargesBy(PersonType personType);
    }


}
