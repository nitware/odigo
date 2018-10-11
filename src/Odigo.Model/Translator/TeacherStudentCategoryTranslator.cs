using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Odigo.Entities;
using Odigo.Model.Model;

namespace Odigo.Model.Translator
{
    public class TeacherStudentCategoryTranslator : BaseTranslator<TeacherStudentCategory, TEACHER_STUDENT_CATEGORY>
    {
        private PersonTranslator _personTranslator;
        private StudentCategoryTranslator _studentCategoryTranslator;

        public TeacherStudentCategoryTranslator()
        {
            _personTranslator = new PersonTranslator();
            _studentCategoryTranslator = new StudentCategoryTranslator();
        }

        public override TeacherStudentCategory TranslateToModel(TEACHER_STUDENT_CATEGORY entity)
        {
            try
            {
                TeacherStudentCategory model = null;
                if (entity != null)
                {
                    model = new TeacherStudentCategory();
                    model.Id = entity.Teacher_Student_Category_Id;
                    model.Person = _personTranslator.Translate(entity.TEACHER.PERSON);
                    model.StudentCategory = _studentCategoryTranslator.Translate(entity.STUDENT_CATEGORY);
                }

                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override TEACHER_STUDENT_CATEGORY TranslateToEntity(TeacherStudentCategory model)
        {
            try
            {
                TEACHER_STUDENT_CATEGORY entity = null;
                if (model != null)
                {
                    entity = new TEACHER_STUDENT_CATEGORY();
                    entity.Teacher_Student_Category_Id= model.Id;
                    entity.Person_Id = model.Person.Id;
                    entity.Student_Category_Id = model.StudentCategory.Id;
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
