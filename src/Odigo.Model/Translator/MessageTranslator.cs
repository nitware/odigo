using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Odigo.Entities;
using Odigo.Model.Model;

namespace Odigo.Model.Translator
{
    public class MessageTranslator : BaseTranslator<Message, MESSAGE>
    {
        private MessageTypeTranslator _messageTypeTranslator;

        public MessageTranslator()
        {
            _messageTypeTranslator = new MessageTypeTranslator();
        }

        public override Message TranslateToModel(MESSAGE entity)
        {
            try
            {
                Message model = null;
                if (entity != null)
                {
                    model = new Message();
                    model.Id = entity.Message_Id;
                    model.Type = _messageTypeTranslator.Translate(entity.MESSAGE_TYPE);
                    model.Text = entity.Message_Text;
                }

                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override MESSAGE TranslateToEntity(Message model)
        {
            try
            {
                MESSAGE entity = null;
                if (model != null)
                {
                    entity = new MESSAGE();
                    entity.Message_Id = model.Id;
                    entity.Message_Type_Id = model.Type.Id;
                    entity.Message_Text = model.Text;
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
