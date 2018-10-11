using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Odigo.Model.Model;
using System.Transactions;
using Odigo.Business.Interfaces;
using Odigo.Entities;

namespace Odigo.Business
{
    public class EmployerRegistrationEditorService : BaseEmployerRegistrationService
    {
        private readonly IPersonEditorService _personEditorService;
        
        public EmployerRegistrationEditorService(IRepository da, IPersonEditorService personEditorService, IPaymentService paymentService) : base(da, paymentService)
        {
            if (personEditorService == null)
            {
                throw new Exception("personEditorService");
            }
            
            _personEditorService = personEditorService;
        }
        
        public override Employer Save(Employer employer)
        {
            try
            {
                using (TransactionScope transaction = new TransactionScope())
                {
                    ModifyEmployer(employer);
                    _personEditorService.Modify(employer.Person);
                    DeleteStudentCategory(employer);
                    
                    bool saved = base.SaveStudentCategory(employer.StudentCategories);
                    if (saved == false)
                    {
                        throw new Exception("Student Category save operation failed!");
                    }

                    employer.PaymentSlip = _paymentService.GetPaymentSlipBy(employer.Person);


                    //ServiceCharge serviceCharge = null;
                    //List<Payment> payments = _da.GetModelsBy<Payment, PAYMENT>(p => p.Person_Id == employer.Person.Id && p.Service_Charge_Id == serviceCharge.Id);
                    //if (payments != null || payments.Count > 0)
                    //{
                    //    employer.PaymentSlip = new PaymentSlip();
                    //    employer.PaymentSlip.Payment = payments[0];
                    //}


                    transaction.Complete();
                }

                return employer;
            }
            catch(Exception)
            {
                throw;
            }
        }
        
        private void ModifyEmployer(Employer employer)
        {
            try
            {
                EMPLOYER entity = _da.GetSingleBy<EMPLOYER>(p => p.Person_Id == employer.Person.Id);
                if (entity != null)
                {
                    entity.Employer_Name = employer.Name;
                    entity.Website = employer.Website;

                    if (employer.Sex != null && employer.Sex.Id > 0)
                    {
                        entity.Sex_Id = employer.Sex.Id;
                    }

                    _da.Save();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void DeleteStudentCategory(Employer employer)
        {
            try
            {
                List<EMPLOYER_STUDENT_CATEGORY> studentCategoryEntities = _da.FindAllBy<EMPLOYER_STUDENT_CATEGORY>(p => p.Person_Id == employer.Person.Id);
                if (studentCategoryEntities != null && studentCategoryEntities.Count > 0)
                {
                    foreach (EMPLOYER_STUDENT_CATEGORY studentCategoryEntity in studentCategoryEntities)
                    {
                        _da.Delete<EMPLOYER_DESIRED_TIME>(p => p.Employer_Student_Category_Id == studentCategoryEntity.Employer_Student_Category_Id);
                    }

                    _da.Delete<EMPLOYER_STUDENT_CATEGORY>(p => p.Person_Id == employer.Person.Id);
                }

            }
            catch (Exception)
            {
                throw;
            }
        }





    }

}
