using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Odigo.Business.Interfaces;
using Odigo.Model.Model;
using System.Transactions;

namespace Odigo.Business
{
    public class EmployerRegistrationService : BaseEmployerRegistrationService
    {
        private readonly IService _serviceCharge;
        private readonly IGateService _gateService;

        public EmployerRegistrationService(IRepository da, IGateService gateService, IService serviceCharge, IPaymentService paymentService) : base(da, paymentService)
        {
            if (gateService == null)
            {
                throw new ArgumentNullException("gateService");
            }
          
            _gateService = gateService;
            _serviceCharge = serviceCharge;
        }
    
        public override Employer Save(Employer employer)
        {
            try
            {
                PaymentSlip slip = null;
                Employer newEmployer = null;

                using (TransactionScope transaction = new TransactionScope())
                {
                    byte[] passwordHash = _gateService.CreatePasswordHash(employer.LoginDetail.RawPassword);
                    employer.LoginDetail.Password = passwordHash;

                    newEmployer = _da.Create(employer);
                    if (newEmployer == null || newEmployer.Person.Id <= 0)
                    {
                        throw new Exception("Employer registration failed! " + TRY_AGAIN);
                    }

                    employer.Person.Id = newEmployer.Person.Id;
                    foreach(EmployerStudentCategory employerStudentCategory in employer.StudentCategories)
                    {
                        employerStudentCategory.Person = employer.Person;
                    }

                    bool saved = base.SaveStudentCategory(employer.StudentCategories);
                    if (saved == false)
                    {
                        throw new Exception("Student Category save operation failed!");
                    }


                    employer.Payments[0].ServiceCharge = _serviceCharge.GetChargesBy(employer.Person.Type);
                    employer.Payments[0].Person = employer.Person;
                    slip = _paymentService.Pay(employer.Payments[0]);
                    if (slip == null || slip.Payment == null || slip.Payment.Id <= 0)
                    {
                        throw new Exception("Payment entry creation failed!");
                    }
                    
                    newEmployer.PaymentSlip = slip;
                    transaction.Complete();
                }

                return newEmployer;
            }
            catch(Exception)
            {
                throw;
            }
        }

       







    }

}
