using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Odigo.Entities;
using Odigo.Model.Model;

namespace Odigo.Model.Translator
{
    public class EmployerStudentCategoryTranslator : BaseTranslator<EmployerStudentCategory, EMPLOYER_STUDENT_CATEGORY>
    {
        private PersonTranslator _personTranslator;
        private StudentCategoryTranslator _studentCategoryTranslator;
        private TeacherTypeTranslator _teacherTypeTranslator;

        public EmployerStudentCategoryTranslator()
        {
            _personTranslator = new PersonTranslator();
            _teacherTypeTranslator = new TeacherTypeTranslator();
            _studentCategoryTranslator = new StudentCategoryTranslator();
        }

        public override EmployerStudentCategory TranslateToModel(EMPLOYER_STUDENT_CATEGORY entity)
        {
            try
            {
                EmployerStudentCategory model = null;
                if (entity != null)
                {
                    model = new EmployerStudentCategory();
                    model.Id = entity.Employer_Student_Category_Id;
                    model.Person = _personTranslator.Translate(entity.EMPLOYER.PERSON);
                    model.StudentCategory = _studentCategoryTranslator.Translate(entity.STUDENT_CATEGORY);
                    model.TeacherType = _teacherTypeTranslator.Translate(entity.TEACHER_TYPE);
                    model.NoOfStudent = entity.No_Of_Student;
                }

                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override EMPLOYER_STUDENT_CATEGORY TranslateToEntity(EmployerStudentCategory model)
        {
            try
            {
                EMPLOYER_STUDENT_CATEGORY entity = null;
                if (model != null)
                {
                    entity = new EMPLOYER_STUDENT_CATEGORY();
                    entity.Employer_Student_Category_Id = model.Id;
                    entity.Person_Id = model.Person.Id;
                    entity.Student_Category_Id = model.StudentCategory.Id;
                    entity.Teacher_Type_Id = model.TeacherType.Id;
                    entity.No_Of_Student = model.NoOfStudent;
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
