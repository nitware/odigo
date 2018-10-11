using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Odigo.Business.Interfaces;
using Odigo.Model.Model;

namespace Odigo.Business
{
    public abstract class BaseTeacherSubscriptionService : ISubscription
    {
        protected readonly IRepository _da;
        protected readonly IPaymentService _paymentService;
        protected readonly IImageManager _passportManager;
        protected const string TRY_AGAIN = "Please try again. But contact your system administrator after three unsuccessful trials.";
        
        public BaseTeacherSubscriptionService(IRepository da, IImageManager passportManager, IPaymentService paymentService)
        {
            if (da == null)
            {
                throw new ArgumentNullException("da");
            }
            if (passportManager == null)
            {
                throw new ArgumentNullException("passportManager");
            }
            if (paymentService == null)
            {
                throw new ArgumentNullException("paymentService");
            }

            _da = da;
            _paymentService = paymentService;
            _passportManager = passportManager;
        }

        public abstract Teacher Save(Teacher teacher);

        private bool SaveOLevelResult(List<TeacherOLevelResult> oLevelResults)
        {
            try
            {
                if (oLevelResults == null || oLevelResults.Count <= 0)
                {
                    throw new Exception("No O-Level result set for teacher! Please set O-Level result.");
                }

                int rowsAdded = 0;
                bool saved = false;
                int oLevelResultDetailCount = 0;

                foreach (TeacherOLevelResult oLevelResult in oLevelResults)
                {
                    TeacherOLevelResult newTeacherOLevelResult = _da.Create(oLevelResult);
                    if (newTeacherOLevelResult == null || newTeacherOLevelResult.Id <= 0)
                    {
                        throw new Exception("Teacher O-Level Result save operation failed! " + TRY_AGAIN);
                    }

                    oLevelResultDetailCount += oLevelResult.OLevelResultDetails.Count;
                    foreach (TeacherOLevelResultDetail oLevelResultDetails in oLevelResult.OLevelResultDetails)
                    {
                        oLevelResultDetails.Result = newTeacherOLevelResult;
                    }

                    rowsAdded += _da.Create(oLevelResult.OLevelResultDetails);
                    if (rowsAdded <= 0)
                    {
                        throw new Exception("Teacher O-Level Result Detail save operation failed! " + TRY_AGAIN);
                    }
                }

                if (rowsAdded > 0 && oLevelResultDetailCount == rowsAdded)
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

        protected string SaveHelper(Teacher teacher)
        {
            try
            {
                bool saved = SaveOLevelResult(teacher.OLevelResults);
                if (saved == false)
                {
                    throw new Exception("Teacher O-Level Result save operation failed! " + TRY_AGAIN);
                }

                string passportFilePath = _passportManager.Store(teacher);
                if (string.IsNullOrWhiteSpace(passportFilePath))
                {
                    throw new Exception("Passport save operation failed! " + TRY_AGAIN);
                }

                return passportFilePath;
            }
            catch (Exception)
            {
                throw;
            }
        }





    }
}
