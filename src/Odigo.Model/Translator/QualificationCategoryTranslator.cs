using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Odigo.Entities;
using Odigo.Model.Model;

namespace Odigo.Model.Translator
{
    public class QualificationCategoryTranslator : BaseTranslator<QualificationCategory, QUALIFICATION_CATEGORY>
    {
        //private QualificationTranslator _qualificationTranslator;

        //public QualificationCategoryTranslator()
        //{
        //    _qualificationTranslator = new QualificationTranslator();
        //}

        public override QualificationCategory TranslateToModel(QUALIFICATION_CATEGORY entity)
        {
            try
            {
                QualificationCategory model = null;
                if (entity != null)
                {
                    model = new QualificationCategory();
                    model.Id = entity.Qualification_Category_Id;
                    model.Name = entity.Qualification_Category_Name;
                    model.Description = entity.Qualification_Category_Description;

                    //if (entity.QUALIFICATION != null && entity.QUALIFICATION.Count > 0)
                    //{
                    //    model.Qualifications = _qualificationTranslator.Translate(entity.QUALIFICATION.ToList());
                    //}
                }

                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override QUALIFICATION_CATEGORY TranslateToEntity(QualificationCategory model)
        {
            try
            {
                QUALIFICATION_CATEGORY entity = null;
                if (model != null)
                {
                    entity = new QUALIFICATION_CATEGORY();
                    entity.Qualification_Category_Id = model.Id;
                    entity.Qualification_Category_Name = model.Name;
                    entity.Qualification_Category_Description = model.Description;
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
