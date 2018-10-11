using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Odigo.Model.Model;
using Odigo.Entities;

namespace Odigo.Model.Translator
{
    public class TeacherOLevelResultDetailTranslator : BaseTranslator<TeacherOLevelResultDetail, TEACHER_O_LEVEL_RESULT_DETAIL>
    {
        private OLevelGradeTranslator oLevelGradeTranslator;
        private OLevelSubjectTranslator oLevelSubjectTranslator;
        private TeacherOLevelResultTranslator oLevelResultTranslator;
        
        public TeacherOLevelResultDetailTranslator()
        {
            oLevelResultTranslator = new TeacherOLevelResultTranslator();
            oLevelSubjectTranslator = new OLevelSubjectTranslator();
            oLevelGradeTranslator = new OLevelGradeTranslator();
        }

        public override TeacherOLevelResultDetail TranslateToModel(TEACHER_O_LEVEL_RESULT_DETAIL entity)
        {
            try
            {
                TeacherOLevelResultDetail oLevelResultDetail = null;
                if (entity != null)
                {
                    oLevelResultDetail = new TeacherOLevelResultDetail();
                    oLevelResultDetail.Id = entity.Teacher_O_Level_Result_Id;
                    oLevelResultDetail.Result = oLevelResultTranslator.Translate(entity.TEACHER_O_LEVEL_RESULT);
                    oLevelResultDetail.Subject = oLevelSubjectTranslator.Translate(entity.O_LEVEL_SUBJECT);
                    oLevelResultDetail.Grade = oLevelGradeTranslator.Translate(entity.O_LEVEL_GRADE);
                }

                return oLevelResultDetail;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override TEACHER_O_LEVEL_RESULT_DETAIL TranslateToEntity(TeacherOLevelResultDetail oLevelResultDetail)
        {
            try
            {
                TEACHER_O_LEVEL_RESULT_DETAIL entity = null;
                if (oLevelResultDetail != null)
                {
                    entity = new TEACHER_O_LEVEL_RESULT_DETAIL();
                    entity.Teacher_O_Level_Result_Detail_Id = oLevelResultDetail.Id;
                    entity.Teacher_O_Level_Result_Id = oLevelResultDetail.Result.Id;
                    entity.O_Level_Subject_Id= oLevelResultDetail.Subject.Id;
                    entity.O_Level_Grade_Id = oLevelResultDetail.Grade.Id;
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
