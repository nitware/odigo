using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Odigo.Entities;
using Odigo.Model.Model;

namespace Odigo.Model.Translator
{
    public class TeachingCostTranslator : BaseTranslator<TeachingCost, TEACHING_COST>
    {
        private StudentCategoryTranslator _studentCategoryTranslator;
        private QualificationCategoryTranslator _qualificationCategoryTranslator;
        //private QualificationTranslator _qualificationTranslator;

        public TeachingCostTranslator()
        {
            _studentCategoryTranslator = new StudentCategoryTranslator();
            _qualificationCategoryTranslator = new QualificationCategoryTranslator();
            //_qualificationTranslator = new QualificationTranslator();
        }

        public override TeachingCost TranslateToModel(TEACHING_COST entity)
        {
            try
            {
                TeachingCost model = null;
                if (entity != null)
                {
                    model = new TeachingCost();
                    model.Id = entity.Teaching_Cost_Id;
                    model.StudentCategory = _studentCategoryTranslator.Translate(entity.STUDENT_CATEGORY);
                    model.QualificationCategory = _qualificationCategoryTranslator.Translate(entity.QUALIFICATION_CATEGORY);
                    model.NoOfPeriod = entity.No_Of_Period;
                    model.Amount = entity.Amount;
                    model.DateEntered = entity.Date_Entered;

                    //if (entity.QUALIFICATION_CATEGORY != null && entity.QUALIFICATION_CATEGORY.QUALIFICATION != null && entity.QUALIFICATION_CATEGORY.QUALIFICATION.Count > 0)
                    //{
                    //    model.Qualifications = _qualificationTranslator.Translate(entity.QUALIFICATION_CATEGORY.QUALIFICATION.ToList());
                    //}
                }

                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override TEACHING_COST TranslateToEntity(TeachingCost model)
        {
            try
            {
                TEACHING_COST entity = null;
                if (model != null)
                {
                    entity = new TEACHING_COST();
                    entity.Teaching_Cost_Id = model.Id;
                    entity.Student_Category_Id = model.StudentCategory.Id;
                    entity.Qualification_Category_Id = model.QualificationCategory.Id;
                    entity.No_Of_Period = model.NoOfPeriod;
                    entity.Amount = model.Amount;
                    entity.Date_Entered = model.DateEntered;
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
