using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Odigo.Entities;
using Odigo.Model.Model;

namespace Odigo.Model.Translator
{
    public class TeacherAwardTranslator : BaseTranslator<TeacherAward, TEACHER_AWARD>
    {
        private PersonTranslator _personTranslator;

        public TeacherAwardTranslator()
        {
            _personTranslator = new PersonTranslator();
        }

        public override TeacherAward TranslateToModel(TEACHER_AWARD entity)
        {
            try
            {
                TeacherAward model = null;
                if (entity != null)
                {
                    model = new TeacherAward();
                    model.Id = entity.Teacher_Award_Id;
                    model.Person = _personTranslator.Translate(entity.TEACHER.PERSON);
                    model.AwardBody = entity.Award_Body;
                    model.AwardName = entity.Award_Name;
                    model.YearAwarded = entity.Year_Awarded;
                }

                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override TEACHER_AWARD TranslateToEntity(TeacherAward model)
        {
            try
            {
                TEACHER_AWARD entity = null;
                if (model != null)
                {
                    entity = new TEACHER_AWARD();
                    entity.Teacher_Award_Id = model.Id;
                    entity.Person_Id = model.Person.Id;
                    entity.Award_Body = model.AwardBody;
                    entity.Award_Name = model.AwardName;
                    entity.Year_Awarded = model.YearAwarded;
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
