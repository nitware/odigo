using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Odigo.Entities;
using Odigo.Model.Model;

namespace Odigo.Model.Translator
{
    public class RequestForEmploymentCostImplicationTranslator : BaseTranslator<RequestForEmploymentCostImplication, REQUEST_FOR_EMPLOYMENT_COST_IMPLICATION>
    {
        private RequestTranslator _requestTranslator;
        private EmployerStudentCategoryTranslator _employerStudentCategoryTranslator;

        public RequestForEmploymentCostImplicationTranslator()
        {
            _requestTranslator = new RequestTranslator();
            _employerStudentCategoryTranslator = new EmployerStudentCategoryTranslator();
        }

        public override RequestForEmploymentCostImplication TranslateToModel(REQUEST_FOR_EMPLOYMENT_COST_IMPLICATION entity)
        {
            try
            {
                RequestForEmploymentCostImplication model = null;
                if (entity != null)
                {
                    model = new RequestForEmploymentCostImplication();
                    model.Id = entity.Request_For_Employment_Cost_Implication_Id;
                    model.Request = _requestTranslator.Translate(entity.REQUEST);
                    model.EmployerStudentCategory = _employerStudentCategoryTranslator.Translate(entity.EMPLOYER_STUDENT_CATEGORY);
                    model.MonthlyPay = entity.MonthlyPay;
                }

                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override REQUEST_FOR_EMPLOYMENT_COST_IMPLICATION TranslateToEntity(RequestForEmploymentCostImplication model)
        {
            try
            {
                REQUEST_FOR_EMPLOYMENT_COST_IMPLICATION entity = null;
                if (model != null)
                {
                    entity = new REQUEST_FOR_EMPLOYMENT_COST_IMPLICATION();
                    entity.Request_For_Employment_Cost_Implication_Id = model.Id;
                    entity.Request_Id = model.Request.Id;
                    entity.Employer_Student_Category_Id = model.EmployerStudentCategory.Id;
                    entity.MonthlyPay = model.MonthlyPay;
                }

                return entity;
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
