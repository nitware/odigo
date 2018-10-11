using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Odigo.Entities;
using Odigo.Model.Model;

namespace Odigo.Model.Translator
{
    public class TeacherVerificationStatusTranslator : BaseTranslator<TeacherVerificationStatus, TEACHER_VERIFICATION_STATUS>
    {
        public override TeacherVerificationStatus TranslateToModel(TEACHER_VERIFICATION_STATUS entity)
        {
            try
            {
                TeacherVerificationStatus model = null;
                if (entity != null)
                {
                    model = new TeacherVerificationStatus();
                    model.Id = entity.Verification_Status_Id;
                    model.Name = entity.Verification_Status_Name;
                    model.Description = entity.Verification_Status_Description;
                }

                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override TEACHER_VERIFICATION_STATUS TranslateToEntity(TeacherVerificationStatus model)
        {
            try
            {
                TEACHER_VERIFICATION_STATUS entity = null;
                if (model != null)
                {
                    entity = new TEACHER_VERIFICATION_STATUS();
                    entity.Verification_Status_Id = model.Id;
                    entity.Verification_Status_Name = model.Name;
                    entity.Verification_Status_Description = model.Description;
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
