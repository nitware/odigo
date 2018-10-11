using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Odigo.Entities;
using Odigo.Model.Model;

namespace Odigo.Model.Translator
{
    public class ServiceChargeTranslator : BaseTranslator<ServiceCharge, SERVICE_CHARGE>
    {
        private ServiceTranslator _serviceTranslator;

        public ServiceChargeTranslator()
        {
            _serviceTranslator = new ServiceTranslator();
        }

        public override ServiceCharge TranslateToModel(SERVICE_CHARGE entity)
        {
            try
            {
                ServiceCharge model = null;
                if (entity != null)
                {
                    model = new ServiceCharge();
                    model.Id = entity.Service_Charge_Id;
                    model.Service = _serviceTranslator.Translate(entity.SERVICE);
                    model.Amount = entity.Amount;
                    model.DateEntered = entity.Date_Entered;
                }

                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override SERVICE_CHARGE TranslateToEntity(ServiceCharge model)
        {
            try
            {
                SERVICE_CHARGE entity = null;
                if (model != null)
                {
                    entity = new SERVICE_CHARGE();
                    entity.Service_Charge_Id = model.Id;
                    entity.Service_Id = model.Service.Id;
                    entity.Amount = model.Amount;
                    entity.Date_Entered = model.DateEntered;
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
