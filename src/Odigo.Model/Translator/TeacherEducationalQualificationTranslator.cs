using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Odigo.Entities;
using Odigo.Model.Model;

namespace Odigo.Model.Translator
{
    public class TeacherEducationalQualificationTranslator : BaseTranslator<TeacherEducationalQualification, TEACHER_EDUCATIONAL_QUALIFICATION>
    {
        private PersonTranslator _personTranslator;
        private QualificationTranslator _qualificationTranslator;
        private SchoolTypeTranslator _schoolTypeTranslator;

        public TeacherEducationalQualificationTranslator()
        {
            _personTranslator = new PersonTranslator();
            _qualificationTranslator = new QualificationTranslator();
            _schoolTypeTranslator = new SchoolTypeTranslator();
        }

        public override TeacherEducationalQualification TranslateToModel(TEACHER_EDUCATIONAL_QUALIFICATION entity)
        {
            try
            {
                TeacherEducationalQualification model = null;
                if (entity != null)
                {
                    model = new TeacherEducationalQualification();
                    model.Id = entity.Teacher_Educational_Qualification_Id;
                    model.Person = _personTranslator.Translate(entity.TEACHER.PERSON);
                    model.School = entity.School;
                    model.SchoolType = _schoolTypeTranslator.Translate(entity.SCHOOL_TYPE);
                    model.YearOfAdmission = entity.Year_Of_Admission;
                    model.YearOfGraduation = entity.Year_Of_Graduation;
                    model.Qualification = _qualificationTranslator.Translate(entity.QUALIFICATION);
                }

                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override TEACHER_EDUCATIONAL_QUALIFICATION TranslateToEntity(TeacherEducationalQualification model)
        {
            try
            {
                TEACHER_EDUCATIONAL_QUALIFICATION entity = null;
                if (model != null)
                {
                    entity = new TEACHER_EDUCATIONAL_QUALIFICATION();
                    entity.Teacher_Educational_Qualification_Id = model.Id;
                    entity.Person_Id = model.Person.Id;
                    entity.School = model.School;
                    entity.School_Type_Id = model.SchoolType.Id;
                    entity.Year_Of_Admission = model.YearOfAdmission;
                    entity.Year_Of_Graduation = model.YearOfGraduation;
                    entity.Qualification_Id = model.Qualification.Id;
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
