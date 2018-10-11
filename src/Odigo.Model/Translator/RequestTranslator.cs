using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Odigo.Entities;
using Odigo.Model.Model;

namespace Odigo.Model.Translator
{
    public class RequestTranslator : BaseTranslator<Request, REQUEST>
    {
        private PersonTranslator _personTranslator;
        private MessageTranslator _messageTranslator;
        private ServiceChargeTranslator _serviceChargeTranslator;
        private RequestStatusTranslator _requestStatusTranslator;

        public RequestTranslator()
        {
            _personTranslator = new PersonTranslator();
            _messageTranslator = new MessageTranslator();
            _serviceChargeTranslator = new ServiceChargeTranslator();
            _requestStatusTranslator = new RequestStatusTranslator();
        }

        public override Request TranslateToModel(REQUEST entity)
        {
            try
            {
                Request model = null;
                if (entity != null)
                {
                    model = new Request();
                    model.Id = entity.Request_Id;
                    model.FromPerson = _personTranslator.Translate(entity.PERSON);
                    model.ToPerson = _personTranslator.Translate(entity.PERSON1);
                    model.RequestMessage = _messageTranslator.Translate(entity.MESSAGE);
                    model.ReplyMessage = _messageTranslator.Translate(entity.MESSAGE1);
                    model.ServiceCharge = _serviceChargeTranslator.Translate(entity.SERVICE_CHARGE);
                    model.Status = _requestStatusTranslator.Translate(entity.REQUEST_STATUS);
                    model.Date = entity.Request_Date;
                    model.ReplyDate = entity.Reply_Date;
                }

                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override REQUEST TranslateToEntity(Request model)
        {
            try
            {
                REQUEST entity = null;
                if (model != null)
                {
                    entity = new REQUEST();
                    entity.Request_Id = model.Id;
                    entity.From_Person_Id = model.FromPerson.Id;
                    entity.To_Person_Id = model.ToPerson.Id;
                    entity.Request_Message_Id = model.RequestMessage.Id;

                    if (model.ReplyMessage != null)
                    {
                        entity.Reply_Message_Id = model.ReplyMessage.Id;
                    }

                    entity.Service_Charge_Id = model.ServiceCharge.Id;
                    entity.Request_Status_Id = model.Status.Id;
                    entity.Reply_Date = model.ReplyDate;
                    entity.Request_Date = model.Date;
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
