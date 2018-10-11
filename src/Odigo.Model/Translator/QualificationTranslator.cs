using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Odigo.Entities;
using Odigo.Model.Model;

namespace Odigo.Model.Translator
{
    public class QualificationTranslator : BaseTranslator<Qualification, QUALIFICATION>
    {
        private SchoolTypeTranslator _schoolTypeTranslator;
        private QualificationCategoryTranslator _qualificationCategoryTranslator;

        public QualificationTranslator()
        {
            _schoolTypeTranslator = new SchoolTypeTranslator();
            _qualificationCategoryTranslator = new QualificationCategoryTranslator();
        }

        public override Qualification TranslateToModel(QUALIFICATION entity)
        {
            try
            {
                Qualification model = null;
                if (entity != null)
                {
                    model = new Qualification();
                    model.Id = entity.Qualification_Id;
                    model.Name = entity.Qualification_Name;
                    model.Description = entity.Qualification_Description;
                    model.SchoolType = _schoolTypeTranslator.Translate(entity.SCHOOL_TYPE);
                    model.Category = _qualificationCategoryTranslator.Translate(entity.QUALIFICATION_CATEGORY);
                }

                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override QUALIFICATION TranslateToEntity(Qualification model)
        {
            try
            {
                QUALIFICATION entity = null;
                if (model != null)
                {
                    entity = new QUALIFICATION();
                    entity.Qualification_Id = model.Id;
                    entity.Qualification_Name = model.Name;
                    entity.Qualification_Description = model.Description;
                    entity.School_Type_Id = model.SchoolType.Id;
                    entity.Qualification_Category_Id = model.Category.Id;
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
