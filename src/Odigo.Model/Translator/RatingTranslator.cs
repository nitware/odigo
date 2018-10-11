using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Odigo.Entities;
using Odigo.Model.Model;

namespace Odigo.Model.Translator
{
    public class RatingTranslator : BaseTranslator<Rating, RATING>
    {
        public override Rating TranslateToModel(RATING entity)
        {
            try
            {
                Rating model = null;
                if (entity != null)
                {
                    model = new Rating();
                    model.Id = entity.Rating_Id;
                    model.Name = entity.Rating_Name;
                    model.Description = entity.Rating_Description;
                }

                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override RATING TranslateToEntity(Rating model)
        {
            try
            {
                RATING entity = null;
                if (model != null)
                {
                    entity = new RATING();
                    entity.Rating_Id = model.Id;
                    entity.Rating_Name = model.Name;
                    entity.Rating_Description = model.Description;
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
