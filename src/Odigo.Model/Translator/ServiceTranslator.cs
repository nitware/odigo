using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Odigo.Entities;
using Odigo.Model.Model;

namespace Odigo.Model.Translator
{
    public class ServiceTranslator : BaseTranslator<Service, SERVICE>
    {
        public override Service TranslateToModel(SERVICE entity)
        {
            try
            {
                Service model = null;
                if (entity != null)
                {
                    model = new Service();
                    model.Id = entity.Service_Id;
                    model.Name = entity.Service_Name;
                    model.Description = entity.Service_Description;
                }

                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override SERVICE TranslateToEntity(Service model)
        {
            try
            {
                SERVICE entity = null;
                if (model != null)
                {
                    entity = new SERVICE();
                    entity.Service_Id = model.Id;
                    entity.Service_Name = model.Name;
                    entity.Service_Description = model.Description;
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
