using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Odigo.Entities;
using Odigo.Model.Model;

namespace Odigo.Model.Translator
{
    public class WeekDayTranslator : BaseTranslator<WeekDay, WEEK_DAY>
    {
        public override WeekDay TranslateToModel(WEEK_DAY entity)
        {
            try
            {
                WeekDay model = null;
                if (entity != null)
                {
                    model = new WeekDay();
                    model.Id = entity.Week_Day_Id;
                    model.Name = entity.Week_Day_Name;
                    model.Abbreviation = entity.Week_Day_Abbreviation;
                }

                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override WEEK_DAY TranslateToEntity(WeekDay model)
        {
            try
            {
                WEEK_DAY entity = null;
                if (model != null)
                {
                    entity = new WEEK_DAY();
                    entity.Week_Day_Id = model.Id;
                    entity.Week_Day_Name = model.Name;
                    entity.Week_Day_Abbreviation = model.Abbreviation;
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
