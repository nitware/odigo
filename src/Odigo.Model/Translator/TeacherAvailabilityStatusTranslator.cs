using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Odigo.Entities;
using Odigo.Model.Model;

namespace Odigo.Model.Translator
{
    public class TeacherAvailabilityStatusTranslator : BaseTranslator<TeacherAvailabilityStatus, TEACHER_AVAILABILITY_STATUS>
    {
        public override TeacherAvailabilityStatus TranslateToModel(TEACHER_AVAILABILITY_STATUS entity)
        {
            try
            {
                TeacherAvailabilityStatus model = null;
                if (entity != null)
                {
                    model = new TeacherAvailabilityStatus();
                    model.Id = entity.Availability_Status_Id;
                    model.Name = entity.Availability_Status_Name;
                    model.Description = entity.Availability_Status_Description;
                }

                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override TEACHER_AVAILABILITY_STATUS TranslateToEntity(TeacherAvailabilityStatus model)
        {
            try
            {
                TEACHER_AVAILABILITY_STATUS entity = null;
                if (model != null)
                {
                    entity = new TEACHER_AVAILABILITY_STATUS();
                    entity.Availability_Status_Id = model.Id;
                    entity.Availability_Status_Name = model.Name;
                    entity.Availability_Status_Description = model.Description;
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
