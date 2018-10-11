using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Odigo.Model.Model;
using Odigo.Business.Interfaces;

namespace Odigo.Business.Factories
{
    public class RegistrationFactory : IRegistrationFactory
    {
        private static IRepository _da;
        private static IGateService _gateService;
        private static IPaymentService _paymentService;
        private static IPersonEditorService _personEditorService;
        private static IService _serviceCharge;

        public RegistrationFactory(IRepository da, IGateService gateService, IService serviceCharge, IPaymentService paymentService, IPersonEditorService personEditorService)
        {
            if (da == null)
            {
                throw new ArgumentNullException("da");
            }
            if (gateService == null)
            {
                throw new ArgumentNullException("gateService");
            }
            if (paymentService == null)
            {
                throw new ArgumentNullException("paymentService");
            }
            if (personEditorService == null)
            {
                throw new ArgumentNullException("personEditorService");
            }
            if (serviceCharge == null)
            {
                throw new ArgumentNullException("serviceCharge");
            }

            _da = da;
            _gateService = gateService;
            _paymentService = paymentService;
            _personEditorService = personEditorService;
            _serviceCharge = serviceCharge;
        }

        public IRegistration Create(Subscription subscription)
        {
            try
            {
                switch (subscription)
                {
                    case Subscription.SubmitEmployer:
                        {
                            return new EmployerRegistrationService(_da, _gateService, _serviceCharge, _paymentService);
                        }
                    case Subscription.EditEmployer:
                        {
                            return new EmployerRegistrationEditorService(_da, _personEditorService, _paymentService);
                        }
                    default:
                        {
                            throw new NotImplementedException(subscription.ToString() + " not implemented!");
                        }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }





    }
}
