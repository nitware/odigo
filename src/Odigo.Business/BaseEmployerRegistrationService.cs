using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Odigo.Business.Interfaces;
using Odigo.Model.Model;

namespace Odigo.Business
{
    public abstract class BaseEmployerRegistrationService : IRegistration
    {
        protected readonly IRepository _da;
        protected readonly IPaymentService _paymentService;

        protected const string TRY_AGAIN = "Please try again. But contact your system administrator after three unsuccessful trials.";

        public BaseEmployerRegistrationService(IRepository da, IPaymentService paymentService)
        {
            if (da == null)
            {
                throw new ArgumentNullException("da");
            }
            if (paymentService == null)
            {
                throw new ArgumentNullException("paymentService");
            }

            _da = da;
            _paymentService = paymentService;
        }

        public abstract Employer Save(Employer employer);
        
        protected bool SaveStudentCategory(List<EmployerStudentCategory> employerStudentCategories)
        {
            try
            {
                if (employerStudentCategories == null || employerStudentCategories.Count <= 0)
                {
                    throw new ArgumentNullException("employerStudentCategories");
                }

                int rowsAdded = 0;
                bool saved = false;
                int desiredTimes = 0;
                foreach (EmployerStudentCategory employerStudentCategory in employerStudentCategories)
                {
                    EmployerStudentCategory newEmployerStudentCategory = _da.Create(employerStudentCategory);
                    if (newEmployerStudentCategory == null || newEmployerStudentCategory.Id <= 0)
                    {
                        throw new Exception("Employer Student Category save operation failed! " + TRY_AGAIN);
                    }

                    desiredTimes += employerStudentCategory.DesiredTimes.Count;
                    foreach (EmployerDesiredTime employerDesiredTime in employerStudentCategory.DesiredTimes)
                    {
                        employerDesiredTime.EmployerStudentCategory = newEmployerStudentCategory;
                    }

                    rowsAdded += _da.Create(employerStudentCategory.DesiredTimes);
                    if (rowsAdded <= 0 || rowsAdded != desiredTimes)
                    {
                        throw new Exception("Employer Desired Time save operation failed! " + TRY_AGAIN);
                    }
                }

                if (rowsAdded > 0 && desiredTimes == rowsAdded)
                {
                    saved = true;
                }

                return saved;
            }
            catch (Exception)
            {
                throw;
            }
        }







    }
}
