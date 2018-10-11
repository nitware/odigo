using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Odigo.Entities;
using Odigo.Model.Model;

namespace Odigo.Model.Translator
{
    public class SchoolTypeTranslator : BaseTranslator<SchoolType, SCHOOL_TYPE>
    {
        public override SchoolType TranslateToModel(SCHOOL_TYPE entity)
        {
            try
            {
                SchoolType model = null;
                if (entity != null)
                {
                    model = new SchoolType();
                    model.Id = entity.School_Type_Id;
                    model.Name = entity.School_Type_Name;
                    model.Description = entity.School_Type_Description;
                }

                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override SCHOOL_TYPE TranslateToEntity(SchoolType model)
        {
            try
            {
                SCHOOL_TYPE entity = null;
                if (model != null)
                {
                    entity = new SCHOOL_TYPE();
                    entity.School_Type_Id = model.Id;
                    entity.School_Type_Name = model.Name;
                    entity.School_Type_Description = model.Description;
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
