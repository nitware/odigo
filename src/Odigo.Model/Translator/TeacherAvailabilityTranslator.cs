using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Odigo.Entities;
using Odigo.Model.Model;

namespace Odigo.Model.Translator
{
    public class TeacherAvailabilityTranslator : BaseTranslator<TeacherAvailability, TEACHER_AVAILABILITY>
    {
        private PersonTranslator _personTranslator;
        private WeekDayTranslator _weekDayTranslator;
        private PeriodTranslator _periodTranslator;

        public TeacherAvailabilityTranslator()
        {
            _personTranslator = new PersonTranslator();
            _weekDayTranslator = new WeekDayTranslator();
            _periodTranslator = new PeriodTranslator();
        }

        public override TeacherAvailability TranslateToModel(TEACHER_AVAILABILITY entity)
        {
            try
            {
                TeacherAvailability model = null;
                if (entity != null)
                {
                    model = new TeacherAvailability();
                    model.Id = entity.Teacher_Availability_Id;
                    model.Person = _personTranslator.Translate(entity.TEACHER.PERSON);
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

        public override TEACHER_AVAILABILITY TranslateToEntity(TeacherAvailability model)
        {
            try
            {
                TEACHER_AVAILABILITY entity = null;
                if (model != null)
                {
                    entity = new TEACHER_AVAILABILITY();
                    entity.Teacher_Availability_Id = model.Id;
                    entity.Person_Id = model.Person.Id;
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
