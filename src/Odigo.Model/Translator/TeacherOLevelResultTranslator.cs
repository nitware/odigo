using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Odigo.Model.Model;
using Odigo.Entities;

namespace Odigo.Model.Translator
{
    public class TeacherOLevelResultTranslator : BaseTranslator<TeacherOLevelResult, TEACHER_O_LEVEL_RESULT>
    {
        private PersonTranslator personTranslator;
        private PersonTypeTranslator personTypeTranslator;
        private OLevelExamSittingTranslator oLevelExamSittingTranslator;
        private OLevelTypeTranslator oLevelTypeTranslator;

        //private TeacherOLevelResultDetailTranslator _teacherOLevelResultDetailTranslator;

        public TeacherOLevelResultTranslator()
        {
            personTranslator = new PersonTranslator();
            personTypeTranslator = new PersonTypeTranslator();
            oLevelExamSittingTranslator = new OLevelExamSittingTranslator();
            oLevelTypeTranslator = new OLevelTypeTranslator();

            //_teacherOLevelResultDetailTranslator = new Translator.TeacherOLevelResultDetailTranslator();
        }

        public override TeacherOLevelResult TranslateToModel(TEACHER_O_LEVEL_RESULT entity)
        {
            try
            {
                TeacherOLevelResult oLevelResult = null;
                if (entity != null)
                {
                    oLevelResult = new TeacherOLevelResult();
                    oLevelResult.Id = entity.Teacher_O_Level_Result_Id;
                    oLevelResult.Person = personTranslator.Translate(entity.TEACHER.PERSON);
                    oLevelResult.ExamNumber = entity.Exam_Number;
                    oLevelResult.ExamYear = entity.Exam_Year;
                    oLevelResult.Sitting = oLevelExamSittingTranslator.Translate(entity.O_LEVEL_EXAM_SITTING);
                    oLevelResult.Type = oLevelTypeTranslator.Translate(entity.O_LEVEL_TYPE);
                }

                return oLevelResult;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override TEACHER_O_LEVEL_RESULT TranslateToEntity(TeacherOLevelResult oLevelResult)
        {
            try
            {
                TEACHER_O_LEVEL_RESULT entity = null;
                if (oLevelResult != null)
                {
                    entity = new TEACHER_O_LEVEL_RESULT();
                    entity.Teacher_O_Level_Result_Id = oLevelResult.Id;
                    entity.Person_Id = oLevelResult.Person.Id;
                    entity.Exam_Number = oLevelResult.ExamNumber;
                    entity.Exam_Year = oLevelResult.ExamYear;
                    entity.O_Level_Exam_Sitting_Id = oLevelResult.Sitting.Id;
                    entity.O_Level_Type_Id = oLevelResult.Type.Id;

                    //entity.TEACHER_O_LEVEL_RESULT_DETAIL = _teacherOLevelResultDetailTranslator.Translate(oLevelResult.OLevelResultDetails);
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
