using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Odigo.Entities;
using Odigo.Model.Model;

namespace Odigo.Model.Translator
{
    public class TeacherEmploymentHistoryTranslator : BaseTranslator<TeacherEmploymentHistory, TEACHER_EMPLOYMENT_HISTORY>
    {
        private PersonTranslator _personTranslator;

        public TeacherEmploymentHistoryTranslator()
        {
            _personTranslator = new PersonTranslator();
        }

        public override TeacherEmploymentHistory TranslateToModel(TEACHER_EMPLOYMENT_HISTORY entity)
        {
            try
            {
                TeacherEmploymentHistory model = null;
                if (entity != null)
                {
                    model = new TeacherEmploymentHistory();
                    model.Id = entity.Teacher_Employment_History_Id;
                    model.Person = _personTranslator.Translate(entity.TEACHER.PERSON);
                    model.Employer = entity.Employer;
                    model.LastPositionHeld = entity.Last_Position_Held;
                    model.StartYear = entity.Start_Year;
                    model.EndYear = entity.End_Year;
                }

                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override TEACHER_EMPLOYMENT_HISTORY TranslateToEntity(TeacherEmploymentHistory model)
        {
            try
            {
                TEACHER_EMPLOYMENT_HISTORY entity = null;
                if (model != null)
                {
                    entity = new TEACHER_EMPLOYMENT_HISTORY();
                    entity.Teacher_Employment_History_Id = model.Id;
                    entity.Person_Id = model.Person.Id;
                    entity.Employer = model.Employer;
                    entity.Last_Position_Held = model.LastPositionHeld;
                    entity.Start_Year = model.StartYear;
                    entity.End_Year = model.EndYear;
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
