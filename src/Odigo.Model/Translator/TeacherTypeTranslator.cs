using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Odigo.Entities;
using Odigo.Model.Model;

namespace Odigo.Model.Translator
{
    public class TeacherTypeTranslator : BaseTranslator<TeacherType, TEACHER_TYPE>
    {
        public override TeacherType TranslateToModel(TEACHER_TYPE entity)
        {
            try
            {
                TeacherType model = null;
                if (entity != null)
                {
                    model = new TeacherType();
                    model.Id = entity.Teacher_Type_Id;
                    model.Name = entity.Teacher_Type_Name;
                    model.Description = entity.Teacher_Type_Description;
                }

                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override TEACHER_TYPE TranslateToEntity(TeacherType model)
        {
            try
            {
                TEACHER_TYPE entity = null;
                if (model != null)
                {
                    entity = new TEACHER_TYPE();
                    entity.Teacher_Type_Id = model.Id;
                    entity.Teacher_Type_Name = model.Name;
                    entity.Teacher_Type_Description = model.Description;
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
