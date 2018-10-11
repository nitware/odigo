using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Odigo.Business.Interfaces;
using Odigo.Model.Model;
using Odigo.Entities;

namespace Odigo.Business
{
    public class ServiceChargeEngine : IService
    {
        private readonly IRepository _da;

        public ServiceChargeEngine(IRepository da)
        {
            if (da == null)
            {
                throw new ArgumentNullException("da");
            }

            _da = da;
        }

        public ServiceCharge GetRequestCostBy(QualificationCategory qualification)
        {
            try
            {
                if (qualification == null || qualification.Id <= 0)
                {
                    throw new ArgumentNullException("qualification");
                }

                int serviceChargeId = 0;
                switch (qualification.Id)
                {
                    case 3:
                        {
                            serviceChargeId = (int)ServiceCharge.Services.RequestToEmployTeacherWithCertificate;
                            break;
                        }
                    case 6:
                        {
                            serviceChargeId = (int)ServiceCharge.Services.RequestToEmployTeacherWithFirstDegree;
                            break;
                        }
                    case 7:
                        {
                            serviceChargeId = (int)ServiceCharge.Services.RequestToEmployTeacherWithSecondDegree;
                            break;
                        }
                    case 8:
                        {
                            serviceChargeId = (int)ServiceCharge.Services.RequestToEmployTeacherWithPhDDegree;
                            break;
                        }
                    default:
                        {
                            throw new Exception("Invalid qualification specified! Please contact your system administrator");
                        }
                }

                return _da.GetModelBy<ServiceCharge, SERVICE_CHARGE>(tc => tc.Service_Charge_Id == serviceChargeId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ServiceCharge GetChargesBy(ServiceCharge serviceCharge)
        {
            try
            {
                ServiceCharge sc = _da.GetModelBy<ServiceCharge, SERVICE_CHARGE>(s => s.Service_Charge_Id == serviceCharge.Id);
                return sc;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ServiceCharge GetChargesBy(ServiceCharge.Services serviceCharge)
        {
            try
            {
                ServiceCharge sc = _da.GetModelBy<ServiceCharge, SERVICE_CHARGE>(s => s.Service_Charge_Id == (int)serviceCharge);
                return sc;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ServiceCharge GetChargesBy(PersonType personType)
        {
            try
            {
                if (personType == null || personType.Id <= 0)
                {
                    throw new ArgumentNullException("personType");
                }

                int serviceChargeId = 0;
                ServiceCharge serviceCharge = null;

                switch (personType.Id)
                {
                    case (int)Person.Types.Teacher:
                        {
                            serviceChargeId = (int)ServiceCharge.Services.TeacherSubscription;
                            break;
                        }
                    case (int)Person.Types.Parent:
                        {
                            serviceChargeId = (int)ServiceCharge.Services.ParentSubscription;
                            break;
                        }
                    case (int)Person.Types.School:
                        {
                            serviceChargeId = (int)ServiceCharge.Services.SchoolSubscription;
                            break;
                        }
                    default:
                        {
                            throw new Exception("Invalid Person Type specified! Please contact your system administrator");
                        }
                }

                serviceCharge = _da.GetModelBy<ServiceCharge, SERVICE_CHARGE>(sc => sc.Service_Charge_Id == serviceChargeId);

                return serviceCharge;
            }
            catch(Exception)
            {
                throw;
            }
        }



    }




}
