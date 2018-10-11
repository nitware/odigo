using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Odigo.Entities;
using Odigo.Model.Model;

namespace Odigo.Model.Translator
{
    public class RequestStatusTranslator : BaseTranslator<RequestStatus, REQUEST_STATUS>
    {
        public override RequestStatus TranslateToModel(REQUEST_STATUS entity)
        {
            try
            {
                RequestStatus model = null;
                if (entity != null)
                {
                    model = new RequestStatus();
                    model.Id = entity.Request_Status_Id;
                    model.Name = entity.Request_Status_Name;
                    model.Description = entity.Request_Status_Description;
                   
                }

                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override REQUEST_STATUS TranslateToEntity(RequestStatus model)
        {
            try
            {
                REQUEST_STATUS entity = null;
                if (model != null)
                {
                    entity = new REQUEST_STATUS();
                    entity.Request_Status_Id = model.Id;
                    entity.Request_Status_Name = model.Name;
                    entity.Request_Status_Description = model.Description;
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
