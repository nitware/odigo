using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Odigo.Business.Interfaces;
using Odigo.Model.Model;
using Odigo.Utility.Interfaces;
using System.Transactions;
using Odigo.Entities;

namespace Odigo.Business
{
    public class TeacherSubscriptionService : BaseTeacherSubscriptionService
    {
        private readonly IService _serviceCharge;
        private readonly IGateService _gateService;
        
        public TeacherSubscriptionService(IRepository da, IGateService gateService, IImageManager passportManager, IService serviceCharge, IPaymentService paymentService) : base(da, passportManager, paymentService)
        {
            if (gateService == null)
            {
                throw new ArgumentNullException("gateService");
            }
            if (serviceCharge == null)
            {
                throw new ArgumentNullException("serviceCharge");
            }

            _gateService = gateService;
            _serviceCharge = serviceCharge;
        }

        public override Teacher Save(Teacher teacher)
        {
            try
            {
                PaymentSlip slip = null;
                Teacher newTeacher = null;

                using (TransactionScope transaction = new TransactionScope())
                {
                    byte[] passwordHash = _gateService.CreatePasswordHash(teacher.LoginDetail.RawPassword);
                    teacher.LoginDetail.Password = passwordHash;

                    newTeacher = _da.Create(teacher);
                    if (newTeacher == null || newTeacher.Person.Id <= 0)
                    {
                        throw new Exception("Teacher subscription operation failed! " + TRY_AGAIN);
                    }

                    teacher.Person.Id = newTeacher.Person.Id;
                    string passportFilePath = SaveHelper(teacher);
                    newTeacher.ImageFileUrl = passportFilePath;
                                        

                    teacher.Payments[0].Person = teacher.Person;
                    teacher.Payments[0].ServiceCharge = _serviceCharge.GetChargesBy(teacher.Person.Type);

                    slip = _paymentService.Pay(teacher.Payments[0]);
                    if (slip == null || slip.Payment == null || slip.Payment.Id <= 0)
                    {
                        throw new Exception("Payment entry creation failed!");
                    }

                    newTeacher.PaymentSlip = slip;
                    transaction.Complete();
                }

                return newTeacher;
            }
            catch (Exception)
            {
                throw;
            }
        }

        
        





    }

}
