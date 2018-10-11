using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Odigo.Model.Model;
using Odigo.Business.Interfaces;

namespace Odigo.Business.Factories
{
    public class SubscriptionFactory : ISubscriptionFactory
    {
        private static IRepository _da;
        private static IImageManager _passportManager;
        private static IPersonEditorService _personEditorService;
        private static IPaymentService _paymentService;
        private static IGateService _gateService;
        private static IService _serviceCharge;

        public SubscriptionFactory(IRepository da, IGateService gateService, IService serviceCharge, IImageManager passportManager, IPersonEditorService personEditorService, IPaymentService paymentService)
        {
            if (da == null)
            {
                throw new ArgumentNullException("da");
            }
            if (passportManager == null)
            {
                throw new ArgumentNullException("passportManager");
            }
            if (gateService == null)
            {
                throw new ArgumentNullException("gateService");
            }
            if (personEditorService == null)
            {
                throw new ArgumentNullException("personEditorService");
            }
            if (paymentService == null)
            {
                throw new ArgumentNullException("paymentService");
            }
            if (serviceCharge == null)
            {
                throw new ArgumentNullException("serviceCharge");
            }

            _da = da;
            _gateService = gateService;
            _paymentService = paymentService;
            _passportManager = passportManager;
            _personEditorService = personEditorService;
            _serviceCharge = serviceCharge;
        }

        public ISubscription Create(Subscription subscription)
        {
            try
            {
                switch (subscription)
                {
                    case Subscription.SubmitTeacher:
                        {
                            return new TeacherSubscriptionService(_da, _gateService, _passportManager, _serviceCharge, _paymentService);
                        }
                    case Subscription.EditTeacher:
                        {
                            return new TeacherSubscriptionEditorService(_da, _passportManager, _personEditorService, _paymentService);
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
