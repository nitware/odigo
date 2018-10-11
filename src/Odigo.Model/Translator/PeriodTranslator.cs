using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Odigo.Entities;
using Odigo.Model.Model;

namespace Odigo.Model.Translator
{
    public class PeriodTranslator : BaseTranslator<Period, PERIOD>
    {
        public override Period TranslateToModel(PERIOD entity)
        {
            try
            {
                Period model = null;
                if (entity != null)
                {
                    model = new Period();
                    model.Id = entity.Period_Id;
                    model.Name = entity.Period_Name;
                }

                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override PERIOD TranslateToEntity(Period model)
        {
            try
            {
                PERIOD entity = null;
                if (model != null)
                {
                    entity = new PERIOD();
                    entity.Period_Id = model.Id;
                    entity.Period_Name = model.Name;
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
