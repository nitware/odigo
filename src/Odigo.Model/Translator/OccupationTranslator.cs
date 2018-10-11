using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Odigo.Entities;
using Odigo.Model.Model;

namespace Odigo.Model.Translator
{
    public class OccupationTranslator : BaseTranslator<Occupation, OCCUPATION>
    {
        public override Occupation TranslateToModel(OCCUPATION entity)
        {
            try
            {
                Occupation model = null;
                if (entity != null)
                {
                    model = new Occupation();
                    model.Id = entity.Occupation_Id;
                    model.Name = entity.Occupation_Name;
                }

                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override OCCUPATION TranslateToEntity(Occupation model)
        {
            try
            {
                OCCUPATION entity = null;
                if (model != null)
                {
                    entity = new OCCUPATION();
                    entity.Occupation_Id = model.Id;
                    entity.Occupation_Name = model.Name;
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
