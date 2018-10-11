using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Odigo.Entities;
using Odigo.Model.Model;

namespace Odigo.Model.Translator
{
    public class LgaTranslator : BaseTranslator<Lga, LGA>
    {
        private StateTranslator _stateTranslator;

        public LgaTranslator()
        {
            _stateTranslator = new StateTranslator();
        }
        
        public override Lga TranslateToModel(LGA entity)
        {
            try
            {
                Lga lga = null;
                if (entity != null)
                {
                    lga = new Lga();
                    lga.Id = entity.Lga_Id;
                    lga.Code = entity.Lga_Code;
                    lga.Name = entity.Lga_Name;
                    lga.State = _stateTranslator.TranslateToModel(entity.STATE);
                }

                return lga;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override LGA TranslateToEntity(Lga lga)
        {
            try
            {
                LGA entity = null;
                if (lga != null)
                {
                    entity = new LGA();
                    entity.Lga_Id = lga.Id;
                    entity.Lga_Code = lga.Code;
                    entity.Lga_Name = lga.Name;
                    entity.State_Id = lga.State.Id;
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
