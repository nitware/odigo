using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Odigo.Entities;
using Odigo.Model.Model;

namespace Odigo.Model.Translator
{
    public class EmployerDesiredTimeTranslator : BaseTranslator<EmployerDesiredTime, EMPLOYER_DESIRED_TIME>
    {
        private PeriodTranslator _periodTranslator;
        private WeekDayTranslator _weekDayTranslator;
        private EmployerStudentCategoryTranslator _employerStudentCategoryTranslator;

        public EmployerDesiredTimeTranslator()
        {
            _periodTranslator = new PeriodTranslator();
            _weekDayTranslator = new WeekDayTranslator();
            _employerStudentCategoryTranslator = new EmployerStudentCategoryTranslator();
        }

        public override EmployerDesiredTime TranslateToModel(EMPLOYER_DESIRED_TIME entity)
        {
            try
            {
                EmployerDesiredTime model = null;
                if (entity != null)
                {
                    model = new EmployerDesiredTime();
                    model.Id = entity.Employer_Student_Category_Id;
                    model.EmployerStudentCategory = _employerStudentCategoryTranslator.Translate(entity.EMPLOYER_STUDENT_CATEGORY);
                    model.WeekDay = _weekDayTranslator.Translate(entity.WEEK_DAY);
                    model.Period = _periodTranslator.Translate(entity.PERIOD);
                }

                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override EMPLOYER_DESIRED_TIME TranslateToEntity(EmployerDesiredTime model)
        {
            try
            {
                EMPLOYER_DESIRED_TIME entity = null;
                if (model != null)
                {
                    entity = new EMPLOYER_DESIRED_TIME();
                    entity.Employer_Student_Category_Id = model.Id;
                    entity.Employer_Student_Category_Id = model.EmployerStudentCategory.Id;
                    entity.Week_Day_Id = model.WeekDay.Id;
                    entity.Period_Id = model.Period.Id;
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
